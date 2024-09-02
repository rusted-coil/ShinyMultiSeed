using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Test.Doubles.Config
{
    /// <summary>
    /// GeneralConfigのテスト用ダブルです。
    /// </summary>
    internal class FakeGeneralConfig : IModifiableGeneralConfig
    {
        public int ThreadCount { get; set; }
    }
}
