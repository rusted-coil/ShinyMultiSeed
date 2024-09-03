using Gen4RngLib.Rng;
using Gen4RngLib.Unown;
using System.Diagnostics;

namespace ShinyMultiSeed.Calculator.Strategy.Internal
{
    internal sealed class Gen4SeedCheckStrategy : ISeedCheckStrategy<uint, IGen4SeedCheckResult>
    {
		// 高速化のため、スレッドごとにRNGオブジェクトを使い回す
		readonly ThreadLocal<ILcgRng> m_MainRng;
		readonly ThreadLocal<ILcgRng> m_TempRng;
		readonly ThreadLocal<ILcgRng> m_ReverseRng;

		class Result : IGen4SeedCheckResult
		{
			public bool IsPassed { get; init; }
			public uint StartPosition { get; init; }
			public int SynchroNature { get; init; }
		}

		// 高速化のため、マルチスレッド内の処理から参照するパラメータはreadonlyなフィールドに値をコピーする
		readonly bool m_isHgss;
		readonly uint m_EncountOffset;
		readonly bool m_DeterminesNature;
		readonly uint m_PositionMin;
		readonly uint m_PositionMax;
		readonly bool m_isShiny;
		readonly uint m_Tsv;
		readonly bool m_FiltersAtkIV;
		readonly uint m_AtkIVMin;
		readonly uint m_AtkIVMax;
		readonly bool m_FiltersSpdIV;
		readonly uint m_SpdIVMin;
		readonly uint m_SpdIVMax;
		readonly bool m_UsesSynchro;
		readonly bool m_isUnownRadio;

		public Gen4SeedCheckStrategy(Gen4SeedCheckStrategyArgs args, Func<ILcgRng> createLcgRng, Func<ILcgRng> createReverseLcgRng)
		{
			m_isHgss = args.IsHgss;
			m_EncountOffset = args.EncountOffset;
			m_DeterminesNature = args.DeterminesNature;
			m_PositionMin = args.PositionMin;
			m_PositionMax = args.PositionMax;
			m_isShiny = args.IsShiny;
			m_Tsv = args.Tsv;
			m_FiltersAtkIV = args.FiltersAtkIV;
			m_AtkIVMin = args.AtkIVMin;
			m_AtkIVMax = args.AtkIVMax;
			m_FiltersSpdIV = args.FiltersSpdIV;
			m_SpdIVMin = args.SpdIVMin;
			m_SpdIVMax = args.SpdIVMax;
			m_UsesSynchro = args.UsesSynchro;
			m_isUnownRadio = args.IsUnownRadio;

			m_MainRng = new ThreadLocal<ILcgRng>(createLcgRng);
			m_TempRng = new ThreadLocal<ILcgRng>(createLcgRng);
			m_ReverseRng = new ThreadLocal<ILcgRng>(createReverseLcgRng);
		}

		public IGen4SeedCheckResult Check(uint initialSeed)
		{
			var mainRng = m_MainRng.Value;
			var tempRng = m_TempRng.Value;
			var reverseRng = m_ReverseRng.Value;
			Debug.Assert(mainRng != null);
			Debug.Assert(tempRng != null);
			Debug.Assert(reverseRng != null);
			mainRng.Seed = initialSeed;

			bool isOk = false;
			uint startPosition = 0;
			int synchroNature = -1;

            // mainRngから最初に出てくるのはr[0]
            // 検索したいのはr[PositionMin + EncountOffset]以降に生成される個体

            mainRng.Advance(m_PositionMin + m_EncountOffset);

			// 範囲拡大の事前処理
			tempRng.Seed = mainRng.Seed; // tempRngはr[PositionMin + EncountOffset]から生成
            tempRng.Advance(m_PositionMax - m_PositionMin + 1);
			// tempRngはr[PositionMax + EncountOffset + 1]から生成

			// 消費数PositionMaxからエンカウント処理を開始すると、個体生成はPositionMax + EncountOffsetから
			// 性格決定が有効の時、性格値が生成されるのはPositionMax + EncountOffset + 1以降となる
			// ここで、PositionMax + EncountOffset + 1 以降で、偶奇ごとにそれぞれの性格が少なくとも1回出る消費数を求めれば、それ以降は絶対に使われない
			uint oddNatureFlag = 0;
			uint evenNatureFlag = 0;
			uint pidLower = tempRng.Next();
			uint pidUpper = 0;
			uint correctedMax = m_PositionMax;
			if (m_DeterminesNature)
			{
                for (uint i = m_PositionMax + m_EncountOffset + 1; ; ++i)
                {
                    pidUpper = tempRng.Next();
                    int nature = (int)((pidUpper << 16 | pidLower) % 25);
                    if (i % 2 == 0)
                    {
                        evenNatureFlag |= 1u << nature;
                    }
                    else
                    {
                        oddNatureFlag |= 1u << nature;
                    }

                    if (oddNatureFlag == 0x1ffffff && evenNatureFlag == 0x1ffffff)
                    {
                        correctedMax = i;
                        break;
                    }
                }
            }

            pidLower = mainRng.Next(); // r[PositionMin + EncountOffset]
            pidUpper = 0;
            for (uint i = m_PositionMin; i <= correctedMax; ++i, pidLower = pidUpper)
            {
                pidUpper = mainRng.Next(); // r[i + EncountOffset + 1]
                                           // このpidは位置[i + EncountOffset]から生成したもの

                // 色違い判定
                if (m_isShiny && m_Tsv != ((pidLower ^ pidUpper) & 0xfff8))
                {
                    continue;
                }

                // 個体値判定とアンノーン判定
                if (m_FiltersAtkIV || m_FiltersSpdIV || m_isUnownRadio)
                {
                    tempRng.Seed = mainRng.Seed;
                    var atk = ((tempRng.Next() >> 5) & 0b11111);
                    if (m_FiltersAtkIV && (atk < m_AtkIVMin || atk > m_AtkIVMax))
                    {
                        continue;
                    }
                    var spd = (tempRng.Next() & 0b11111);
                    if (m_FiltersSpdIV && (spd < m_SpdIVMin || spd > m_SpdIVMax))
                    {
                        continue;
                    }

                    if (m_isUnownRadio)
                    {
                        if (!m_isHgss)
                        {
                            continue;
                        }
                        tempRng.Next();
                        if (!tempRng.CheckRadioEffect())
                        {
                            continue;
                        }
                    }
                }

                // r[i + EncountOffset]から性格値を生成するものが条件を満たした

                // この性格値が本当に生成できるかチェック
                if (m_DeterminesNature)
                {
                    uint nature = (pidUpper << 16 | pidLower) % 25;

                    reverseRng.Seed = mainRng.Seed; // mainRngのSeedを与えた逆RNGは、次にr[i + EncountOffset]を返す
                    reverseRng.Next(); // r[i + EncountOffset]

                    // r[i + EncountOffset - 1]でシンクロ判定or性格ロール、r[i + EncountOffset - 2]とr[i + EncountOffset -1]で一つ前の性格値生成が行われる
                    // 性格値生成で同じ性格が出る前に、シンクロ判定か性格ロールに成功したらOK
                    for (int a = (int)i - 2; a >= (int)m_PositionMin - 1; a -= 2)
                    {
                        uint rand = reverseRng.Next(); // r[a + EncountOffset + 1]
						if (a <= (int)m_PositionMax - 1)
						{
                            if ((m_isHgss && rand % 25 == nature)
                                || (!m_isHgss && rand / 0xa3e == nature))// 素の性格ロール成功
                            {
                                isOk = true;
                                startPosition = (uint)(a + 1); // r[a + EncountOffset + 1]から性格決定を行えばOKなので、r[a + 1]からエンカウント処理を行う位置を返す
                                synchroNature = -1;
                            }
                            else if (m_UsesSynchro
                                && ((m_isHgss && rand % 2 == 0) || (!m_isHgss && (rand & 0x8000) == 0))) // シンクロ成功
                            {
                                isOk = true;
                                startPosition = (uint)(a + 1);
                                synchroNature = (int)nature;
                            }
                        }

                        uint targetPid = (rand << 16 | reverseRng.Next()); // r[a + EncountOffset]が生成する性格値
                        if (targetPid % 25 == nature) // 同じ性格が出てしまった
                        {
                            // 探索おわり
                            break;
                        }
                    }
                    if (isOk)
                    {
                        break;
                    }
                }
                else
                {
                    // 性格決定処理を挟まない場合は、iから生成で確定
                    isOk = true;
                    startPosition = i;
                    break;
                }
            }

            return new Result { IsPassed = isOk, StartPosition = startPosition, SynchroNature = synchroNature };
        }
	}
}
