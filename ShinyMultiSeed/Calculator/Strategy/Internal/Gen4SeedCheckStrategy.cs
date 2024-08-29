using Gen4RngLib.Rng;
using System.Diagnostics;

namespace ShinyMultiSeed.Calculator.Strategy.Internal
{
    internal sealed class Gen4SeedCheckStrategy : ISeedCheckStrategy<uint, IGen4SeedCheckResult>
    {
        // 高速化のため、スレッドごとにオブジェクトを使い回す
        readonly ThreadLocal<ILcgRng> m_Rng;
        readonly ThreadLocal<(int Flag, int Synchro)[]> m_NatureFlags;

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

        public Gen4SeedCheckStrategy(Gen4SeedCheckStrategyArgs args, Func<ILcgRng> createLcgRng)
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

            m_Rng = new ThreadLocal<ILcgRng>(createLcgRng);
            m_NatureFlags = new ThreadLocal<(int Flag, int Synchro)[]>(() => new (int Flag, int Synchro)[50]);
        }

        public IGen4SeedCheckResult Check(uint initialSeed)
        {
            var rng = m_Rng.Value;
            var natureFlags = m_NatureFlags.Value;
            Debug.Assert(rng != null);
            Debug.Assert(natureFlags != null);
			rng.Seed = initialSeed;

            bool isOk = false;
            uint startPosition = 0;
            int synchroNature = -1;

            // mainRngから最初に出てくるのはr[0]
            // 検索したいのはr[PositionMin + EncountOffset]以降に生成される個体

            // TODO 目当てのポジションまで消費するのは高速化したい
            for (int i = 0; i < m_PositionMin + m_EncountOffset; ++i)
            {
				rng.Next();
            }

            uint pidLower = rng.Next(); // r[PositionMin + EncountOffset]
            uint pidUpper = rng.Next();
            uint ivs1 = rng.Next();
            uint ivs2;

            for (int i = 0; i < 50; ++i)
            {
				natureFlags[i].Flag = -1;
            }

            for (uint i = m_PositionMin + m_EncountOffset; i <= m_PositionMax + m_EncountOffset; ++i, pidLower = pidUpper, pidUpper = ivs1, ivs1 = ivs2)
            { 
                ivs2 = rng.Next();

                // この時点でのpidとivsはr[i]から生成されるもの

                uint nature = (pidUpper << 16 | pidLower) % 25;

                if (!m_DeterminesNature || natureFlags[nature + (i % 2) * 25].Flag >= 0) // 生成できる性格だった
                {
                    bool isPass = true;

                    // 色違い判定
                    if (m_isShiny && m_Tsv != ((pidLower ^ pidUpper) & 0xfff8))
                    {
                        isPass = false;
                    }

                    // 個体値判定
                    if (isPass && (m_FiltersAtkIV || m_FiltersSpdIV))
                    {
                        var atk = ((ivs1 >> 5) & 0b11111);
                        if (m_FiltersAtkIV && (atk < m_AtkIVMin || atk > m_AtkIVMax))
                        {
                            isPass = false;
                        }
                        var spd = (ivs2 & 0b11111);
                        if (m_FiltersSpdIV && (spd < m_SpdIVMin || spd > m_SpdIVMax))
                        {
                            isPass = false;
                        }
                    }

                    if (isPass)
                    {
                        // 適合個体だったらNatureFlagにかかれている消費数を出力して終了
                        isOk = true;
                        startPosition = (uint)natureFlags[nature + (i % 2) * 25].Flag;
                        synchroNature = natureFlags[nature + (i % 2) * 25].Synchro;
						break;
                    }
                    else
                    {
						// 適合していなかったらこの個体の性格フラグを✕に
						natureFlags[nature + (i % 2) * 25].Flag = -1;
                    }
                }

                // フラグ更新
                if (m_DeterminesNature)
                {
					if (m_UsesSynchro &&
                        ((m_isHgss && pidLower % 2 == 0) || (!m_isHgss && (pidLower & 0x8000) == 0))) // シンクロに成功したら全てのフラグを◯に（上書きはしない）
					{
						uint offset = (1 - i % 2) * 25;
						for (int n = 0; n < 25; ++n)
						{
							if (natureFlags[n + offset].Flag < 0)
							{
								natureFlags[n + offset] = ((int)i, n);
							}
						}
					}
					else
					{
                        // 通常の性格ロールフラグだけを◯に（上書きはしない）
                        uint n = (m_isHgss ? pidLower % 25 : pidLower / 0xa3e);                        
						if (natureFlags[n + (1 - i % 2) * 25].Flag < 0)
						{
							natureFlags[n + (1 - i % 2) * 25] = ((int)i, -1);
						}
					}
				}
			}

            return new Result { IsPassed = isOk, StartPosition = startPosition, SynchroNature = synchroNature };
        }
    }
}
