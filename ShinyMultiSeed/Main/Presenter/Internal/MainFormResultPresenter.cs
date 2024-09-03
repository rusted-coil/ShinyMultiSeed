using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using PKHeX.Core;
using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Text;

namespace ShinyMultiSeed.Main.Presenter.Internal
{
    internal sealed class MainFormResultPresenter : IMainFormResultPresenter
    {
        const string c_OutputFilePath = "result.txt";

        IResultViewModel? m_CurrentViewModel;

        readonly IMainFormResultView m_View;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormResultPresenter(IMainFormResultView view)
        {
            m_View = view;
            m_Disposables.Add(m_View.OutputResultButton.Clicked.Subscribe(_ => OutputResult()));
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        public void ShowResult(IResultViewModel viewModel)
        {
            m_CurrentViewModel = viewModel;
            m_View.OverViewText = viewModel.OverviewText;
            m_View.SetResultColumns(viewModel.Columns);
            m_View.SetResultRows(viewModel.Rows);
        }

        void OutputResult()
        {
            if (m_CurrentViewModel != null)
            {
                using (var sw = new StreamWriter(c_OutputFilePath))
                {
                    bool showsWildSlot = false;
                    bool usesSynchro = false;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < m_CurrentViewModel.Columns.Count; ++i)
                    {
                        var column = m_CurrentViewModel.Columns[i];
                        if (column.Id == "WildSlot")
                        {
                            showsWildSlot = true;
                        }
                        else if (column.Id == "SynchroNature")
                        {
                            usesSynchro = true;
                        }

                        if (i > 0)
                        {
                            sb.Append(",");
                        }
                        sb.Append(column.DisplayText);                        
                    }
                    sw.WriteLine(sb.ToString());

                    foreach (var row in m_CurrentViewModel.Rows)
                    {
                        var viewModel = row as Gen4ResultRowViewModel;
                        Debug.Assert(viewModel != null);

                        sb.Clear();
                        sb.Append($"{viewModel.InitialSeed},{viewModel.StartPosition},");
                        if (showsWildSlot)
                        {
                            sb.Append($"{viewModel.WildSlot},");
                        }
                        if (usesSynchro)
                        {
                            sb.Append($"{viewModel.SynchroNature},");
                        }
                        sb.Append($"{viewModel.Pid},{viewModel.Nature},{viewModel.HpIV},{viewModel.AtkIV},{viewModel.DefIV},{viewModel.SpAtkIV},{viewModel.SpDefIV},{viewModel.SpdIV}");
                        sw.WriteLine(sb.ToString());
                    }
                }
                var startInfo = new ProcessStartInfo()
                {
                    FileName = c_OutputFilePath,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                };
                Process.Start(startInfo);
            }
        }
    }
}
