using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator.Strategy;
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

        readonly ISeedCheckStrategy<uint, IGen4SeedCheckResult> m_Strategy;
        readonly uint m_FrameMin;
        readonly uint m_FrameMax;
        readonly uint m_MultiSeedCount;

        public Gen4SeedCalculator(ISeedCheckStrategy<uint, IGen4SeedCheckResult> strategy, uint frameMin, uint frameMax, uint multiSeedCount)
        {
            m_Strategy = strategy;
            m_FrameMin = frameMin;
            m_FrameMax = frameMax;
            m_MultiSeedCount = multiSeedCount;
        }

        public IEnumerable<ISeedCalculatorResult<uint>> Calculate(int threadCount)
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
            return m_Results;
        }

        void CalculatePart(uint partIndex, uint partCount)
        {
            int evenCandidateCount = 0;
            int oddCandidateCount = 0;
            Result[] evenCandidates = new Result[m_MultiSeedCount - 1];
            Result[] oddCandidates = new Result[m_MultiSeedCount - 1];
            for (int i = 0; i < m_MultiSeedCount - 1; ++i)
            {
                evenCandidates[i] = new Result();
                oddCandidates[i] = new Result();
            }

            for (uint upper = partIndex; upper <= 0xff; upper += partCount)
            {
                for (uint hour = 0; hour <= 23; ++hour)
                {
                    evenCandidateCount = 0;
                    oddCandidateCount = 0;
                    for (uint frame = m_FrameMin; frame <= m_FrameMax; ++frame)
                    {
                        uint initialSeed = upper << 24 | hour << 16 | frame;
                        ref int candidateCount = ref (frame % 2 == 0 ? ref evenCandidateCount : ref oddCandidateCount);
                        Result[] candidates = frame % 2 == 0 ? evenCandidates : oddCandidates;

                        var result = m_Strategy.Check(initialSeed);
                        if (result.IsPassed) // この初期seedのstartPositionで目的の個体が出る
                        {
                            if (candidateCount == candidates.Length)
                            {
                                for (int i = 0; i < candidates.Length; ++i)
                                {
                                    m_Results.Add(new Result(candidates[i]));
                                }
                                m_Results.Add(new Result
                                {
                                    InitialSeed = initialSeed,
                                    StartPosition = result.StartPosition,
                                    SynchroNature = result.SynchroNature,
                                });
                                candidateCount = 0;
                            }
                            else
                            {
                                candidates[candidateCount].InitialSeed = initialSeed;
                                candidates[candidateCount].StartPosition = result.StartPosition;
                                candidates[candidateCount].SynchroNature = result.SynchroNature;
                                candidateCount++;
                            }
                        }
                        else // この初期seedでは出ない
                        {
                            candidateCount = 0;
                        }
                    }
                }
            }
        }
        /*

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
                Parallel.For(0, threadCount, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, threadIndex => CalculatePart2((uint)threadIndex, (uint)threadCount));
            }
        }




        public void CalculatePart2(uint partIndex, uint partCount)
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
                    for (uint frame = m_FrameMin; frame <= m_FrameMax; ++frame)
                    {
                        uint initialSeed = upper << 24 | hour << 16 | frame;

                        var result = m_Strategy.Check(initialSeed);
                        uint startPosition = result.StartPosition;
                        if (result.IsPassed) // この初期seedのstartPositionで目的の個体が出る
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
                                        SynchroNature = result.SynchroNature,
                                    });
                                    evenCandidateCount = 0;
                                }
                                else
                                {
                                    evenCandidates[evenCandidateCount].InitialSeed = initialSeed;
                                    evenCandidates[evenCandidateCount].StartPosition = startPosition;
                                    evenCandidates[evenCandidateCount].SynchroNature = result.SynchroNature;
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
                                        SynchroNature = result.SynchroNature,
                                    });
                                    oddCandidateCount = 0;
                                }
                                else
                                {
                                    oddCandidates[oddCandidateCount].InitialSeed = initialSeed;
                                    oddCandidates[oddCandidateCount].StartPosition = startPosition;
                                    oddCandidates[oddCandidateCount].SynchroNature = result.SynchroNature;
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
        */
    }
}
