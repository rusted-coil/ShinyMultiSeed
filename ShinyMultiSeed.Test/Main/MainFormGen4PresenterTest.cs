using ShinyMultiSeed.Main.Presenter;
using ShinyMultiSeed.Test.Doubles.Calculator.Provider;
using ShinyMultiSeed.Test.Doubles.Config;
using ShinyMultiSeed.Test.Doubles.MainForm;

namespace ShinyMultiSeed.Test.Main
{
    [TestClass]
    public class MainFormGen4PresenterTest
    {
        // stubのViewに値をセット
        // stubのViewの計算ボタンを発火
        // ・変更がConfigに反映される
        // ・変更後の値でCalculatorProviderが呼ばれる
        // ・Serializerが呼ばれる
        // をそれぞれテストしたい
        static void SetStubViewTestParams(FakeMainFormGen4View stubView)
        {
            stubView.IsHgssChecked = true;
            stubView.EncountType = 1;
            stubView.IsShinyChecked = true;
            stubView.TidText = "12378";
            stubView.SidText = "45690";
            stubView.FiltersAtkIVChecked = true;
            stubView.AtkIVMinValue = 1;
            stubView.AtkIVMaxValue = 2;
            stubView.FiltersSpdIVChecked = true;
            stubView.SpdIVMinValue = 3;
            stubView.SpdIVMaxValue = 4;
            stubView.UsesSynchroChecked = true;
            stubView.FrameMinText = "56";
            stubView.FrameMaxText = "78";
            stubView.PositionMinText = "90";
            stubView.PositionMaxText = "123";
        }

        /// <summary>
        /// 設定した内容がGen4Configに適切に反映されるかをテストします。
        /// </summary>
        [TestMethod]
        public void ConfigModification_NormalCase()
        {
            var stubGeneralConfig = new FakeGeneralConfig();
            var mockGen4Config = new FakeGen4Config();
            var stubCalculatorProvider = new FakeGen4SeedCalculatorProvider();
            var stubView = new FakeMainFormGen4View();
            var stubResultPresenter = MainFormPresenterFactory.CreateResultPresenter(new FakeMainFormResultView());
            var stubSerializer = () => true;

            using (var presenter = MainFormPresenterFactory.CreateGen4Presenter(
                stubGeneralConfig,
                mockGen4Config,
                stubCalculatorProvider,
                stubView,
                stubResultPresenter,
                stubSerializer))
            {
                SetStubViewTestParams(stubView);

                stubView.FireCalculateButton();

                Assert.AreEqual(mockGen4Config.IsHgss, true);
                Assert.AreEqual(mockGen4Config.EncountType, 1);
                Assert.AreEqual(mockGen4Config.IsShiny, true);
                Assert.AreEqual(mockGen4Config.Tid, 12378u);
                Assert.AreEqual(mockGen4Config.Sid, 45690u);
                Assert.AreEqual(mockGen4Config.FiltersAtkIV, true);
                Assert.AreEqual(mockGen4Config.AtkIVMin, 1u);
                Assert.AreEqual(mockGen4Config.AtkIVMax, 2u);
                Assert.AreEqual(mockGen4Config.FiltersSpdIV, true);
                Assert.AreEqual(mockGen4Config.SpdIVMin, 3u);
                Assert.AreEqual(mockGen4Config.SpdIVMax, 4u);
                Assert.AreEqual(mockGen4Config.UsesSynchro, true);
                Assert.AreEqual(mockGen4Config.FrameMin, 56u);
                Assert.AreEqual(mockGen4Config.FrameMax, 78u);
                Assert.AreEqual(mockGen4Config.PositionMin, 90u);
                Assert.AreEqual(mockGen4Config.PositionMax, 123u);
            }
        }

        /// <summary>
        /// 設定した内容でCalculatorProviderが適切なConfigを渡されるかをテストします。
        /// </summary>
        [TestMethod]
        public void CalculatorProvider_NormalCase_ReceivesExpectConfig()
        {
            var stubGeneralConfig = new FakeGeneralConfig();
            var stubGen4Config = new FakeGen4Config();
            var mockCalculatorProvider = new FakeGen4SeedCalculatorProvider();
            var stubView = new FakeMainFormGen4View();
            var stubResultPresenter = MainFormPresenterFactory.CreateResultPresenter(new FakeMainFormResultView());
            var stubSerializer = () => true;

            using (var presenter = MainFormPresenterFactory.CreateGen4Presenter(
                stubGeneralConfig,
                stubGen4Config,
                mockCalculatorProvider,
                stubView,
                stubResultPresenter,
                stubSerializer))
            {
                SetStubViewTestParams(stubView);

                stubView.FireCalculateButton();

                Assert.IsNotNull(mockCalculatorProvider.GeneralConfig);
                Assert.IsNotNull(mockCalculatorProvider.Gen4Config);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.IsHgss, true);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.EncountType, 1);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.IsShiny, true);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.Tid, 12378u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.Sid, 45690u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.FiltersAtkIV, true);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.AtkIVMin, 1u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.AtkIVMax, 2u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.FiltersSpdIV, true);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.SpdIVMin, 3u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.SpdIVMax, 4u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.UsesSynchro, true);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.FrameMin, 56u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.FrameMax, 78u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.PositionMin, 90u);
                Assert.AreEqual(mockCalculatorProvider.Gen4Config.PositionMax, 123u);
            }
        }


        /// <summary>
        /// Serializerが適切に呼ばれるかをテストします。
        /// </summary>
        [TestMethod]
        public void SerializerExecution_NormalCase_ReceivesExpectConfig()
        {
            var stubGeneralConfig = new FakeGeneralConfig();
            var stubGen4Config = new FakeGen4Config();
            var stubCalculatorProvider = new FakeGen4SeedCalculatorProvider();
            var stubView = new FakeMainFormGen4View();
            var stubResultPresenter = MainFormPresenterFactory.CreateResultPresenter(new FakeMainFormResultView());
            var serializedFlag = false;
            var mockSerializer = () => serializedFlag = true;

            using (var presenter = MainFormPresenterFactory.CreateGen4Presenter(
                stubGeneralConfig,
                stubGen4Config,
                stubCalculatorProvider,
                stubView,
                stubResultPresenter,
                mockSerializer))
            {
                stubView.FireCalculateButton();

                Assert.AreEqual(serializedFlag, true);
            }
        }
    }
}
