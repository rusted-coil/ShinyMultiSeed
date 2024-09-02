using ShinyMultiSeed.Config;
using ShinyMultiSeed.Config.Internal;
using System.Reactive.Disposables;
using System.Text;
using System.Diagnostics;
using ShinyMultiSeed.Calculator.Strategy;
using ShinyMultiSeed.Calculator;

namespace ShinyMultiSeed.Main.Internal
{
    /// <summary>
    /// MainFormの第4世代に関わる部分のPresenterです。
    /// </summary>
    internal sealed class MainFormGen4Presenter : IDisposable
    {
        public enum EncountType
        {
            Legendary = 0,
            Roamer,
            Wild,
            Unown,
        }

        readonly IMainFormGen4View m_View;
        readonly IGeneralConfig m_GeneralConfig;
        readonly IModifiableGen4Config m_Config;
        readonly Func<bool> m_SerializeGen4Config;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormGen4Presenter(IMainFormGen4View view, IGeneralConfig generalConfig, IModifiableGen4Config config, Func<bool> serializeGen4Config)
        {
            m_View = view;
            m_GeneralConfig = generalConfig;
            m_Config = config;
            m_SerializeGen4Config = serializeGen4Config;

            m_Disposables.Add(view.IsHgssCheckedChanged.Subscribe(SetSelectableEncountTypes));
            m_Disposables.Add(view.CalculateButton.Clicked.Subscribe(_ => Calculate()));

            InitializeView();
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
        }

        // フォームに設定を反映
        void ReflectConfigToView()
        { 
            m_View.IsHgssChecked = m_Config.IsHgss;
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

            var args = ConfigConverter.ConvertToGen4SeedCheckStrategyArgs(m_GeneralConfig, m_Config);

            // 前処理
            m_View.SetIsCalculating(true);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await ExecuteCalculationAsync(args, m_GeneralConfig.ThreadCount);

            // 後処理
            stopwatch.Stop();
            m_View.SetIsCalculating(false);

//            OutputResult(args, result, stopwatch.Elapsed.TotalSeconds);
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

            if (sb.Length > 0)
            {
                errorMessage = sb.ToString();
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 計算を実行して結果を出力
        async Task<IEnumerable<ISeedCalculatorResult<uint>>> ExecuteCalculationAsync(Gen4SeedCheckStrategyArgs args, int threadCount)
        {
            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);
            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(strategy, m_Config.FrameMin, m_Config.FrameMax, 2);
            return await Task.Run(() => calculator.Calculate(threadCount));
        }
    }
}
