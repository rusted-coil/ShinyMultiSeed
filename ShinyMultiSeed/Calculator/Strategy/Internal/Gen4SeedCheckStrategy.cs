using Gen4RngLib.Rng;

namespace ShinyMultiSeed.Calculator.Strategy.Internal
{
    internal sealed class Gen4SeedCheckStrategy : ISeedCheckStrategy<uint, IGen4SeedCheckResult>
    {
        // 高速化のため、スレッドごとにRNGオブジェクトを使い回す
        readonly ThreadLocal<ILcgRng> m_MainRng;
        readonly ThreadLocal<ILcgRng> m_TempRng;
        readonly ThreadLocal<ILcgRng> m_ReverseRng;

        readonly ThreadLocal<int[]> m_NatureFlags;

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
            m_NatureFlags = new ThreadLocal<int[]>(() => new int[50]);
        }

        public IGen4SeedCheckResult Check(uint initialSeed)
        {
            var mainRng = m_MainRng.Value;
            var tempRng = m_TempRng.Value;
            var reverseRng = m_ReverseRng.Value;
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

            uint pidLower = mainRng.Next(); // r[PositionMin + EncountOffset]
            uint pidUpper = mainRng.Next();
            uint ivs1 = mainRng.Next();
            uint ivs2;

            for (int i = 0; i < 50; ++i)
            {
                m_NatureFlags.Value[i] = -1;
            }

            for (uint i = m_PositionMin + m_EncountOffset; i <= m_PositionMax + m_EncountOffset; ++i, pidLower = pidUpper, pidUpper = ivs1, ivs1 = ivs2)
            { 
                ivs2 = mainRng.Next();

                // この時点でのpidとivsはr[i]から生成されるもの

                uint nature = (pidUpper << 16 | pidLower) % 25;

                if (m_NatureFlags.Value[nature * 2 + i % 2] >= 0) // 生成できる性格だった
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
                        startPosition = (uint)m_NatureFlags.Value[nature * 2 + i % 2];
                        break;
                    }
                    else
                    {
                        // 適合していなかったらこの個体の性格フラグを✕に
                        m_NatureFlags.Value[nature * 2 + i % 2] = -1;
                    }
                }

                // フラグ更新
                if (pidLower % 2 == 0) // シンクロに成功したら全てのフラグを◯に
                {
                    for (int n = 0; n < 25; ++n)
                    {
                        m_NatureFlags.Value[n * 2 + (1 - i % 2)] = (int)i;
                    }
                }
                else
                {
                    // 通常の性格ロールフラグだけを◯に
                    m_NatureFlags.Value[(pidLower % 25) * 2 + (1 - i % 2)] = (int)i;
                }
            }

            return new Result { IsPassed = isOk, StartPosition = startPosition, SynchroNature = synchroNature };
        }
    }
}
