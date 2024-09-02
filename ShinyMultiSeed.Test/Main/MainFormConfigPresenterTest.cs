using ShinyMultiSeed.Main.Presenter;
using ShinyMultiSeed.Test.Doubles.Config;
using ShinyMultiSeed.Test.Doubles.MainForm;

namespace ShinyMultiSeed.Test.Main
{
    [TestClass]
    public class MainFormConfigPresenterTest
    {
        /// <summary>
        /// ConfigPresenterに与えたGeneralConfigが適切に更新されるかをテストします。
        /// </summary>
        [TestMethod]
        public void ConfigModification_NormalCase()
        {
            // MockのGeneralConfigを与えてPresenterを初期化し、StubのViewを与えてPresenterを操作する。
            // 最後にGeneralConfigをチェックして終了。

            var mockGeneralConfig = new FakeGeneralConfig { ThreadCount = 1 };
            var stubView = new FakeMainFormConfigView();
            var stubSerializer = () => true; // Serializerについては今回は何もしない

            using (var presenter = MainFormPresenterFactory.CreateConfigPresenter(mockGeneralConfig, stubView, stubSerializer))
            {
                for (int i = 0; i < 5; ++i)
                {
                    stubView.FireThreadCountButtonClicked(i);
                    Assert.AreEqual(mockGeneralConfig.ThreadCount, Math.Pow(2, i));
                }
            }                
        }

        /// <summary>
        /// ConfigPresenterに与えたViewが適切に更新されるかをテストします。
        /// </summary>
        [TestMethod]
        public void ViewModification_NormalCase()
        {
            // StubのGeneralConfigを与えてPresenterを初期化し、MockのViewを与えてPresenterを操作する。
            // 最後にViewをチェックして終了。

            var stubGeneralConfig = new FakeGeneralConfig();
            var mockView = new FakeMainFormConfigView();
            var stubSerializer = () => true; // Serializerについては今回は何もしない

            using (var presenter = MainFormPresenterFactory.CreateConfigPresenter(stubGeneralConfig, mockView, stubSerializer))
            {
                for (int i = 0; i < 5; ++i)
                {
                    mockView.FireThreadCountButtonClicked(i);
                    Assert.AreEqual(mockView.ThreadCountIndex, i);
                }
            }
        }


        /// <summary>
        /// ConfigPresenterに与えたSerializerが適切に呼ばれるかをテストします。
        /// </summary>
        [TestMethod]
        public void SerializerExecution_NormalCase()
        {
            // StubのGeneralConfigを与えてPresenterを初期化し、StubのViewを与えてPresenterを操作する。
            // 最後にViewをアサートして終了。

            var stubGeneralConfig = new FakeGeneralConfig();
            var stubView = new FakeMainFormConfigView();
            var serializedFlag = false;
            var mockSerializer = () => serializedFlag = true;

            using (var presenter = MainFormPresenterFactory.CreateConfigPresenter(stubGeneralConfig, stubView, mockSerializer))
            {
                stubView.FireThreadCountButtonClicked(0);
                Assert.AreEqual(serializedFlag, true);
            }
        }
    }
}
