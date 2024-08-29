using Gen4RngLib.Rng;
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

			// TODO 目当てのポジションまで消費するのは高速化したい
			for (int i = 0; i < m_PositionMin + m_EncountOffset; ++i)
			{
				mainRng.Next();
			}

			uint pidLower = mainRng.Next(); // r[PositionMin]
			uint pidUpper = 0;
			for (uint i = m_PositionMin; i <= m_PositionMax; ++i, pidLower = pidUpper)
			{
				pidUpper = mainRng.Next(); // r[i+1]
										   // このpidは位置iから生成したもの

				// 色違い判定
				if (m_isShiny && m_Tsv != ((pidLower ^ pidUpper) & 0xfff8))
				{
					continue;
				}

				// 個体値判定
				if (m_FiltersAtkIV || m_FiltersSpdIV)
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
				}

				// この性格値が本当に生成できるかチェック
				if (m_DeterminesNature)
				{
					uint nature = (pidUpper << 16 | pidLower) % 25;

					reverseRng.Seed = mainRng.Seed; // mainRngのSeedを与えた逆RNGは、次にr[i]を返す
					reverseRng.Next(); // r[i]

					// r[i-1]でシンクロ判定or性格ロール、r[i-2]とr[i-1]で一つ前の性格値生成が行われる
					// 性格値生成で同じ性格が出る前に、シンクロ判定か性格ロールに成功したらOK
					for (int a = (int)i - 2; a >= m_EncountOffset; a -= 2)
					{
						uint rand = reverseRng.Next(); // r[a+1]
						if ((m_isHgss && rand % 25 == nature)
							|| (!m_isHgss && rand / 0xa3e == nature))// 素の性格ロール成功
						{
							isOk = true;
							startPosition = (uint)(a + 1);
							synchroNature = -1;
						}
						else if (m_UsesSynchro
							&& ((m_isHgss && rand % 2 == 0) || (!m_isHgss && (rand & 0x8000) == 0))) // シンクロ成功
						{
							isOk = true;
							startPosition = (uint)(a + 1);
							synchroNature = (int)nature;
						}

						uint targetPid = (rand << 16 | reverseRng.Next()); // r[a]が生成する性格値
						if (targetPid % 25 == nature) // 同じ性格が出てしまった
						{
							// 探索おわり
							break;
						}
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
