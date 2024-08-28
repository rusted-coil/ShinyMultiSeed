using Gen4RngLib.Rng;
using System.Collections.Concurrent;

namespace ShinyMultiSeed.Calculator.Internal
{
    internal sealed class Gen4SeedCalculator : ISeedCalculator<uint>
    {
        ConcurrentBag<(uint InitialSeed, uint StartPosition)> m_Results = new ConcurrentBag<(uint, uint)>();
        public IEnumerable<(uint InitialSeed, uint StartPosition)> Results => m_Results;

        // 高速化のため、マルチスレッド内の処理から参照するパラメータはreadonlyなフィールドに値をコピーする
        readonly uint m_FrameMin;
        readonly uint m_FrameMax;
        readonly uint m_PositionMin;
        readonly uint m_PositionMax;
        readonly uint m_EncountOffset;
        readonly bool m_DeterminesNature;
        readonly bool m_isShiny;
        readonly uint m_Tsv;
        readonly bool m_FiltersAtkIV;
        readonly uint m_AtkIVMin;
        readonly uint m_AtkIVMax;
        readonly bool m_FiltersSpdIV;
        readonly uint m_SpdIVMin;
        readonly uint m_SpdIVMax;

        public Gen4SeedCalculator(Gen4SeedCalculatorArgs args)
        {
            m_FrameMin = args.FrameMin;
            m_FrameMax = args.FrameMax;
            m_PositionMin = args.PositionMin;
            m_PositionMax = args.PositionMax;
            m_EncountOffset = args.EncountOffset;
            m_DeterminesNature = args.DeterminesNature;
            m_isShiny = args.IsShiny;
            m_Tsv = args.Tsv;
            m_FiltersAtkIV = args.FiltersAtkIV;
            m_AtkIVMin = args.AtkIVMin;
            m_AtkIVMax = args.AtkIVMax;
            m_FiltersSpdIV = args.FiltersSpdIV;
            m_SpdIVMin = args.SpdIVMin;
            m_SpdIVMax = args.SpdIVMax;
        }

        public void Clear()
        {
            m_Results.Clear();
        }

        public void CalculateAll(int threadCount)
        {
            m_Results.Clear();
            if (threadCount == 1)
            {
                CalculatePart(0, 1);
            }
            else
            {
                Parallel.For(0, threadCount, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, threadIndex => CalculatePart((uint)threadIndex, (uint)threadCount));
            }
        }

        public void CalculatePart(uint partIndex, uint partCount)
        {
            // インスタンス使いまわし用に共通のものを用意しておく
            var mainRng = RngFactory.CreateLcgRng(0);
            var tempRng = RngFactory.CreateLcgRng(0);
            var reverseRng = RngFactory.CreateReverseLcgRng(0);

            (uint Seed, uint Position)? evenCandidate = null;
            (uint Seed, uint Position)? oddCandidate = null;
            for (uint upper = partIndex; upper <= 0xff; upper += partCount)
            {
                for (uint hour = 0; hour <= 23; ++hour)
                {
                    evenCandidate = null;
                    oddCandidate = null;
                    for(uint frame = m_FrameMin; frame <= m_FrameMax; ++frame)
                    {
                        uint initialSeed = upper << 24 | hour << 16 | frame;
                        mainRng.Seed = initialSeed;

                        bool isOk = false;
                        uint startPosition = 0;

                        // mainRngから最初に出てくるのはr[0]
                        // 検索したいのはr[PositionMin]以降に生成される性格値

                        // TODO 目当てのポジションまで消費するのは高速化したい
                        for (int i = 0; i < m_PositionMin; ++i)
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
                                    if (rand % 2 == 0) // シンクロ成功
                                    {
                                        // OK確定
                                        isOk = true;
                                        startPosition = (uint)(a + 1);
                                    }
                                    else if (rand % 25 == nature) // 性格ロール成功
                                    {
                                        // OK確定
                                        isOk = true;
                                        startPosition = (uint)(a + 1);
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

                        if (isOk) // この初期seedのstartPositionで目的の個体が出る
                        {
                            startPosition -= m_EncountOffset;
                            if (frame % 2 == 0)
                            {
                                if (evenCandidate == null)
                                {
                                    evenCandidate = (initialSeed, startPosition);
                                }
                                else
                                {
                                    m_Results.Add((evenCandidate.Value.Seed, evenCandidate.Value.Position));
                                    m_Results.Add((initialSeed, startPosition));
                                    evenCandidate = (initialSeed, startPosition);
                                }
                            }
                            else
                            {
                                if (oddCandidate == null)
                                {
                                    oddCandidate = (initialSeed, startPosition);
                                }
                                else
                                {
                                    m_Results.Add((oddCandidate.Value.Seed, oddCandidate.Value.Position));
                                    m_Results.Add((initialSeed, startPosition));
                                    oddCandidate = (initialSeed, startPosition);
                                }
                            }
                        }
                        else // この初期seedでは出ない
                        {
                            if (frame % 2 == 0)
                            {
                                evenCandidate = null;
                            }
                            else
                            {
                                oddCandidate = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
