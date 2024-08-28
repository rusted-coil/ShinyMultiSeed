﻿using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator;
using System.Diagnostics;
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
            m_Disposables.Add(m_MainForm.CalculateButton.Clicked.Subscribe(_ => Calculate()));
        }

        public void Run()
        {
            Application.Run(m_MainForm);
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        void Calculate()
        {
            var args = m_MainForm.GetGen4SeedCalculatorArgs();
            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(args);

            var stopwatch = new Stopwatch(); // 処理時間測定用のストップウォッチ
            stopwatch.Start();

            calculator.CalculateAll(16);

            stopwatch.Stop();

            var sortedResults = calculator.Results.OrderBy(result => result.InitialSeed).ToList();
            using (var sw = new StreamWriter("output.txt"))
            {
                var tempRng = RngFactory.CreateLcgRng(0);
                Individual individual = new Individual();
                foreach (var result in sortedResults)
                {
                    var rng = RngFactory.CreateLcgRng(result.InitialSeed);
                    uint nature = 0;
                    for (int i = 0; i < result.StartPosition + args.EncountOffset; ++i)
                    {
                        rng.Next();
                    }
                    if (args.DeterminesNature)
                    {
                        nature = rng.DetermineNature(Gen4RngLib.GameVersion.HGSS, result.SynchroNature);
                    }
                    else
                    {
                        rng.GenerateIndividual(-1, individual);
                        nature = individual.GetNature();
                    }
                    sw.WriteLine($"{result.InitialSeed:X8},{result.StartPosition},{nature}");
                }
            }
            var startInfo = new ProcessStartInfo()
            {
                FileName = "output.txt",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            Process.Start(startInfo);

            MessageBox.Show($"処理時間: {stopwatch.Elapsed.TotalSeconds:F2} 秒");
        }
    }
}
