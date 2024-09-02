namespace ShinyMultiSeed.Config
{
    public interface IModifiableGeneralConfig : IGeneralConfig
    {
        /// <summary>
        /// 並列計算に使用するスレッド数を取得/設定します。
        /// </summary>
        new int ThreadCount { get; set; }
    }
}
