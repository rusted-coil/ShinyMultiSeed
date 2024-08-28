using Gen4RngLib.Rng;
using System.Collections.Concurrent;

namespace ShinyMultiSeed.Calculator.Internal
{
    internal sealed class Gen4SeedCalculator : ISeedCalculator<uint>
    {
        class Result : ISeedCalculatorResult<uint>
        {
            public uint InitialSeed { get; set; }
            public uint StartPosition { get; set; }
            public int SynchroNature { get; set; }

            public Result() { }
            public Result(ISeedCalculatorResult<uint> source)
            {
                InitialSeed = source.InitialSeed;
                StartPosition = source.StartPosition;
                SynchroNature = source.SynchroNature;
            }
        }

        ConcurrentBag<ISeedCalculatorResult<uint>> m_Results = new ConcurrentBag<ISeedCalculatorResult<uint>>();
        public IEnumerable<ISeedCalculatorResult<uint>> Results => m_Results;

        // 高速化のため、マルチスレッド内の処理から参照するパラメータはreadonlyなフィールドに値をコピーする
        readonly bool m_isHgss;
        readonly uint m_EncountOffset;
        readonly bool m_DeterminesNature;
        readonly uint m_FrameMin;
        readonly uint m_FrameMax;
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

        public Gen4SeedCalculator(Gen4SeedCalculatorArgs args)
        {
            m_isHgss = args.IsHgss;
            m_EncountOffset = args.EncountOffset;
            m_DeterminesNature = args.DeterminesNature;
            m_FrameMin = args.FrameMin;
            m_FrameMax = args.FrameMax;
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

            int evenCandidateCount = 0;
            Result[] evenCandidates = [new Result()];
            int oddCandidateCount = 0;
            Result[] oddCandidates = [new Result()];

            for (uint upper = partIndex; upper <= 0xff; upper += partCount)
            {
                for (uint hour = 0; hour <= 23; ++hour)
                {
                    evenCandidateCount = 0;
                    oddCandidateCount = 0;
                    for(uint frame = m_FrameMin; frame <= m_FrameMax; ++frame)
                    {
                        uint initialSeed = upper << 24 | hour << 16 | frame;
                        mainRng.Seed = initialSeed;

                        bool isOk = false;
                        uint startPosition = 0;
                        int synchroNature = -1;

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

                        if (isOk) // この初期seedのstartPositionで目的の個体が出る
                        {
                            startPosition -= m_EncountOffset;
                            if (frame % 2 == 0)
                            {
                                if (evenCandidateCount == evenCandidates.Length)
                                {
                                    for (int i = 0; i < evenCandidates.Length; ++i)
                                    {
                                        m_Results.Add(new Result(evenCandidates[i]));
                                    }
                                    m_Results.Add(new Result
                                    {
                                        InitialSeed = initialSeed,
                                        StartPosition = startPosition,
                                        SynchroNature = synchroNature,
                                    });
                                    evenCandidateCount = 0;
                                }
                                else
                                {
                                    evenCandidates[evenCandidateCount].InitialSeed = initialSeed;
                                    evenCandidates[evenCandidateCount].StartPosition = startPosition;
                                    evenCandidates[evenCandidateCount].SynchroNature = synchroNature;
                                    evenCandidateCount++;
                                }
                            }
                            else
                            {
                                if (oddCandidateCount == oddCandidates.Length)
                                {
                                    for (int i = 0; i < oddCandidates.Length; ++i)
                                    {
                                        m_Results.Add(new Result(oddCandidates[i]));
                                    }
                                    m_Results.Add(new Result
                                    {
                                        InitialSeed = initialSeed,
                                        StartPosition = startPosition,
                                        SynchroNature = synchroNature,
                                    });
                                    oddCandidateCount = 0;
                                }
                                else
                                {
                                    oddCandidates[oddCandidateCount].InitialSeed = initialSeed;
                                    oddCandidates[oddCandidateCount].StartPosition = startPosition;
                                    oddCandidates[oddCandidateCount].SynchroNature = synchroNature;
                                    oddCandidateCount++;
                                }
                            }
                        }
                        else // この初期seedでは出ない
                        {
                            if (frame % 2 == 0)
                            {
                                evenCandidateCount = 0;
                            }
                            else
                            {
                                oddCandidateCount = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
