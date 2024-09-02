namespace ShinyMultiSeed.Config
{
    public interface IGeneralConfig
    {
        /// <summary>
        /// 並列計算に使用するスレッド数を取得します。
        /// </summary>
        int ThreadCount { get; }
    }
}
