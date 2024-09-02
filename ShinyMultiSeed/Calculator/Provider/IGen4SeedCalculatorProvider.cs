using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Calculator.Provider
{
    /// <summary>
    /// 第4世代のSeedCalculatorを作成して返す能力を持つインターフェースです。
    /// </summary>
    public interface IGen4SeedCalculatorProvider
    {
        /// <summary>
        /// 第4世代の初期seedカリキュレータを作成します。
        /// </summary>
        /// <param name="generalConfig">全体のコンフィグ</param>
        /// <param name="gen4Config">第4世代の設定</param>
        /// <returns>作成したseedカリキュレータ</returns>
        ISeedCalculator<uint> CreateGen4SeedCalculator(IGeneralConfig generalConfig, IGen4Config gen4Config);
    }
}
