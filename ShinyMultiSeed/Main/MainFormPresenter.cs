using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using System.Reactive.Disposables;

namespace ShinyMultiSeed.Main
{
    internal sealed class MainFormPresenter : IDisposable
    {
        readonly MainForm m_MainForm;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormPresenter()
        { 
            m_MainForm = new MainForm();
        }

        public void Run()
        {
//            Test();
            Application.Run(m_MainForm);
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        /*
        void Test()
        {
            using (var sw = new StreamWriter("output.txt"))
            {
                uint tsv = (24485 ^ 59064) & 0xfff8;
                var rng = RngFactory.CreateLcgRng(0);
                var tempRng = RngFactory.CreateLcgRng(0);
                var reverseRng = RngFactory.CreateReverseLcgRng(0);
                var individual = new Individual();
                var initialSeedCandidates = new List<uint>();
                for (uint upper = 0; upper <= 0xff; ++upper)
                {
                    for (uint hour = 0; hour <= 23; ++hour)
                    {
                        var frameSet = new HashSet<uint>();
                        for (uint frame = 900; frame <= 1500; ++frame)
                        {
                            uint initialSeed = upper << 24 | hour << 16 | frame;
                            rng.Seed = initialSeed;

                            uint pid1, pid2;
                            pid1 = rng.Next(); // r[0]
                            for (int i = 1; i <= 150; ++i)
                            {
                                pid2 = rng.Next(); // r[i]

                                var psv = (pid1 ^ pid2) & 0xfff8;
                                if (tsv == psv) // r[i-1]が色違い性格値生成位置
                                {
                                    tempRng.Seed = rng.Seed;
                                    // 個体値チェック
                                    if (((tempRng.Next() >> 5) & 0b11111) <= 1 // A0
                                        && ((tempRng.Next()) & 0b11111) <= 1) // S0
                                    {
                                        uint nature = (pid2 << 16 | pid1) % 25;

                                        reverseRng.Seed = rng.Seed; // rngのSeedを与えた逆RNGは、次にr[i-1]を返す
                                        reverseRng.Next();

                                        // r[i-2]でシンクロ判定or性格ロール、r[i-3]とr[i-2]で一つ前の性格値生成が行われる
                                        // 性格値生成で同じ性格が出る前に、シンクロ判定か性格ロールに成功したらOK
                                        bool isOk = false;
                                        int startPosition = 0;
                                        for (int a = i - 3; a >= 0; a -= 2)
                                        {
                                            uint rand = reverseRng.Next();
                                            if (rand % 2 == 0) // シンクロ成功
                                            {
                                                // OK確定
                                                isOk = true;
                                                startPosition = a + 1;
                                                break;
                                            }
                                            else if (rand % 25 == nature) // 性格ロール成功
                                            {
                                                // OK確定
                                                isOk = true;
                                                startPosition = a + 1;
                                                break;
                                            }
                                            else
                                            {
                                                uint targetPid = (rand << 16 | reverseRng.Next());
                                                if (targetPid % 25 == nature) // 同じ性格が出てしまった
                                                {
                                                    // NG確定
                                                    break;
                                                }
                                            }
                                        }

                                        if (isOk)
                                        {
                                            frameSet.Add(frame);
                                            if (frameSet.Contains(frame - 2))
                                            {
                                                sw.WriteLine($"{initialSeed - 2:X8},{initialSeed:X8}");
                                            }
                                            break;
                                        }
                                    }
                                }

                                pid1 = pid2;
                            }
                        }
                    }
                }
            }
            var startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "output.txt",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            System.Diagnostics.Process.Start(startInfo);
        }
        */
    }
}
