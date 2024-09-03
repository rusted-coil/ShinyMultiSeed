using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using PKHeXUtilLib.Nature;
using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Calculator.Provider;
using ShinyMultiSeed.Calculator.Strategy;
using ShinyMultiSeed.Config;
using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Text;

namespace ShinyMultiSeed.Main.Presenter.Internal
{
    /// <summary>
    /// MainFormの第4世代に関わる部分のPresenterです。
    /// </summary>
    internal sealed class MainFormGen4Presenter : IMainFormGen4Presenter
    {
        public enum EncountType
        {
            Legendary = 0,
            Roamer,
            Wild,
            Unown,
        }

        readonly IGeneralConfig m_GeneralConfig;
        readonly IModifiableGen4Config m_Config;
        readonly IGen4SeedCalculatorProvider m_CalculatorProvider;
        readonly IMainFormGen4View m_View;
        readonly IMainFormResultPresenter m_ResultPresenter;
        readonly Func<bool> m_SerializeGen4Config;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormGen4Presenter(
            IGeneralConfig generalConfig,
            IModifiableGen4Config config,
            IGen4SeedCalculatorProvider calculatorProvider,
            IMainFormGen4View view,
            IMainFormResultPresenter resultPresenter,
            Func<bool> serializeGen4Config)
        {
            m_GeneralConfig = generalConfig;
            m_Config = config;
            m_CalculatorProvider = calculatorProvider;
            m_View = view;
            m_ResultPresenter = resultPresenter;
            m_SerializeGen4Config = serializeGen4Config;

            InitializeView();

            m_Disposables.Add(view.IsHgssCheckedChanged.Subscribe(SetSelectableEncountTypes));
            m_Disposables.Add(view.CalculateButton.Clicked.Subscribe(_ => Calculate()));
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        void InitializeView()
        {
            ReflectConfigToView();
        }

        void SetSelectableEncountTypes(bool isHgss)
        {
            int old = m_View.EncountType;
            var list = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>((int)EncountType.Legendary, "固定シンボル(シンクロ可)"),
                new KeyValuePair < int, string >((int)EncountType.Roamer, "徘徊"),
                new KeyValuePair < int, string >((int)EncountType.Wild, "野生"),
            };
            if (isHgss)
            {
                list.Add(new KeyValuePair<int, string>((int)EncountType.Unown, "アンノーン(ラジオ有り)"));
            }
            m_View.SetSelectableEncountTypes(list);
            m_View.EncountType = old;
        }

        // フォームに設定を反映
        void ReflectConfigToView()
        {
            m_View.IsHgssChecked = m_Config.IsHgss;
            SetSelectableEncountTypes(m_Config.IsHgss);
            m_View.EncountType = m_Config.EncountType;
            m_View.IsShinyChecked = m_Config.IsShiny;
            m_View.TidText = m_Config.Tid.ToString();
            m_View.SidText = m_Config.Sid.ToString();
            m_View.FiltersAtkIVChecked = m_Config.FiltersAtkIV;
            m_View.AtkIVMinValue = m_Config.AtkIVMin;
            m_View.AtkIVMaxValue = m_Config.AtkIVMax;
            m_View.FiltersSpdIVChecked = m_Config.FiltersSpdIV;
            m_View.SpdIVMinValue = m_Config.SpdIVMin;
            m_View.SpdIVMaxValue = m_Config.SpdIVMax;
            m_View.UsesSynchroChecked = m_Config.UsesSynchro;
            m_View.FrameMinText = m_Config.FrameMin.ToString();
            m_View.FrameMaxText = m_Config.FrameMax.ToString();
            m_View.PositionMinText = m_Config.PositionMin.ToString();
            m_View.PositionMaxText = m_Config.PositionMax.ToString();
            m_View.MultiSeedCount = m_Config.MultiSeedCount;
        }

        void ShowError(string message) => MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

        async void Calculate()
        {
            if (!TryReflectViewToConfig(out string reflectConfigError))
            {
                ShowError(reflectConfigError);
                return;
            }

            m_SerializeGen4Config();

            var calculator = m_CalculatorProvider.CreateGen4SeedCalculator(m_GeneralConfig, m_Config);

            // 前処理
            m_View.SetIsCalculating(true);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await Task.Run(() => calculator.Calculate(m_GeneralConfig.ThreadCount));

            // 後処理
            stopwatch.Stop();
            m_View.SetIsCalculating(false);

            OutputResult(result, stopwatch.Elapsed.TotalSeconds);
        }

        private void ValidateAndSetUInt(string text, Action<uint> setProperty, StringBuilder sb, string fieldName)
        {
            if (uint.TryParse(text, out uint result))
            {
                setProperty(result);
            }
            else
            {
                sb.AppendLine($"{fieldName}の形式が不正です。: {text}");
            }
        }
        private void ValidateAndSetUInt(decimal value, Action<uint> setProperty, StringBuilder sb, string fieldName)
        {
            if (value >= uint.MinValue && value <= uint.MaxValue)
            {
                setProperty((uint)value);
            }
            else
            {
                sb.AppendLine($"{fieldName}の形式が不正です。: {value.ToString()}");
            }
        }

        // フォームから設定を取得
        bool TryReflectViewToConfig(out string errorMessage)
        {
            StringBuilder sb = new StringBuilder();

            m_Config.IsHgss = m_View.IsHgssChecked;
            m_Config.EncountType = m_View.EncountType;
            m_Config.IsShiny = m_View.IsShinyChecked;
            ValidateAndSetUInt(m_View.TidText, value => m_Config.Tid = value, sb, "表ID");
            ValidateAndSetUInt(m_View.SidText, value => m_Config.Sid = value, sb, "裏ID");
            m_Config.FiltersAtkIV = m_View.FiltersAtkIVChecked;
            ValidateAndSetUInt(m_View.AtkIVMinValue, value => m_Config.AtkIVMin = value, sb, "A個体値Min");
            ValidateAndSetUInt(m_View.AtkIVMaxValue, value => m_Config.AtkIVMax = value, sb, "A個体値Max");
            m_Config.FiltersSpdIV = m_View.FiltersSpdIVChecked;
            ValidateAndSetUInt(m_View.SpdIVMinValue, value => m_Config.SpdIVMin = value, sb, "S個体値Min");
            ValidateAndSetUInt(m_View.SpdIVMaxValue, value => m_Config.SpdIVMax = value, sb, "S個体値Max");
            m_Config.UsesSynchro = m_View.UsesSynchroChecked;
            ValidateAndSetUInt(m_View.FrameMinText, value => m_Config.FrameMin = value, sb, "待機フレームMin");
            ValidateAndSetUInt(m_View.FrameMaxText, value => m_Config.FrameMax = value, sb, "待機フレームMax");
            ValidateAndSetUInt(m_View.PositionMinText, value => m_Config.PositionMin = value, sb, "性格値決定消費数Min");
            ValidateAndSetUInt(m_View.PositionMaxText, value => m_Config.PositionMax = value, sb, "性格値決定消費数Max");
            ValidateAndSetUInt(m_View.MultiSeedCount, value => m_Config.MultiSeedCount = value, sb, "多面待ち候補数");

            if (sb.Length > 0)
            {
                errorMessage = sb.ToString();
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 結果を出力
        void OutputResult(IEnumerable<ISeedCalculatorResult<uint>> results, double elapsedSeconds)
        {
            var rng = RngFactory.CreateLcgRng(0);
            var individual = new Individual();
            var args = ConfigConverter.ConvertToGen4SeedCheckStrategyArgs(m_GeneralConfig, m_Config);

            var columns = CreateResultViewModelColumns(m_Config);

            var rows = results
                .OrderBy(result => result.InitialSeed)
                .Select(result => 
                {
                    // 個体情報を得るために実際に生成してみる
                    rng.Seed = result.InitialSeed;
                    uint lastRand = rng.Advance(result.StartPosition + args.EncountOffset);

                    int wildSlot = (int)(args.IsHgss ? lastRand % 100 : lastRand / 0x290);

                    int nature = -1;
                    if (args.DeterminesNature)
                    {
                        nature = (int)rng.DetermineNature(args.IsHgss ? Gen4RngLib.GameVersion.HGSS : Gen4RngLib.GameVersion.DPt, result.SynchroNature);
                    }
                    rng.GenerateIndividual(nature, individual);

                    return new Gen4ResultRowViewModel
                    {
                        InitialSeed = result.InitialSeed.ToString("X8"),
                        StartPosition = result.StartPosition,
                        WildSlot = StrategyConst.WildEncountLookUp[wildSlot],
                        SynchroNature = result.SynchroNature < 0 ? "-" : NatureUtil.GetNatureName(result.SynchroNature, "ja"),
                        Pid = individual.PID.ToString("X8"),
                        Nature = NatureUtil.GetNatureName((int)individual.GetNature(), "ja"),
                        HpIV = individual.HpIV,
                        AtkIV = individual.AtkIV,
                        DefIV = individual.DefIV,
                        SpAtkIV = individual.SpAtkIV,
                        SpDefIV = individual.SpDefIV,
                        SpdIV = individual.SpdIV,
                    };
                }).ToArray();
            var resultViewModel = ResultViewModelFactory.Create(
                $"計算結果: 候補{rows.Count()}個 (処理時間: {elapsedSeconds:F2} 秒)",
                columns, rows);
            m_ResultPresenter.ShowResult(resultViewModel);
        }

        IReadOnlyList<IResultColumnViewModel> CreateResultViewModelColumns(IGen4Config gen4Config)
        {
            var list = new List<IResultColumnViewModel> {
                ResultViewModelFactory.CreateColumn("InitialSeed", "初期seed", 85),
                ResultViewModelFactory.CreateColumn("StartPosition", "消費数", 50)
            };
            if (gen4Config.EncountType == (int)EncountType.Wild)
            {
                list.Add(ResultViewModelFactory.CreateColumn("WildSlot", "野生", 40));
            }
            if (gen4Config.UsesSynchro)
            {
                list.Add(ResultViewModelFactory.CreateColumn("SynchroNature", "シンクロ", 80));
            }
            list.Add(ResultViewModelFactory.CreateColumn("Pid", "性格値", 85));
            list.Add(ResultViewModelFactory.CreateColumn("Nature", "性格", 80));
            list.Add(ResultViewModelFactory.CreateColumn("HpIV", "H", 40));
            list.Add(ResultViewModelFactory.CreateColumn("AtkIV", "A", 40));
            list.Add(ResultViewModelFactory.CreateColumn("DefIV", "B", 40));
            list.Add(ResultViewModelFactory.CreateColumn("SpAtkIV", "C", 40));
            list.Add(ResultViewModelFactory.CreateColumn("SpDefIV", "D", 40));
            list.Add(ResultViewModelFactory.CreateColumn("SpdIV", "S", 40));

            return list;
        }
    }
}
