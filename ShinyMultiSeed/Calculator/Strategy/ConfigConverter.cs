using ShinyMultiSeed.Config;
using ShinyMultiSeed.Main.Presenter.Internal;

namespace ShinyMultiSeed.Calculator.Strategy
{
    internal static class ConfigConverter
    {
        public static Gen4SeedCheckStrategyArgs ConvertToGen4SeedCheckStrategyArgs(IGeneralConfig config, IGen4Config gen4Config)
        {
            return new Gen4SeedCheckStrategyArgs
            {
                IsHgss = gen4Config.IsHgss,
                PositionMin = gen4Config.PositionMin,
                PositionMax = gen4Config.PositionMax,
                EncountOffset = GetEncountOffset((MainFormGen4Presenter.EncountType)gen4Config.EncountType),
                DeterminesNature = GetDeterminesNature((MainFormGen4Presenter.EncountType)gen4Config.EncountType),
                IsShiny = gen4Config.IsShiny,
                Tsv = (gen4Config.Tid ^ gen4Config.Sid) & 0xfff8,
                FiltersAtkIV = gen4Config.FiltersAtkIV,
                AtkIVMin = gen4Config.AtkIVMin,
                AtkIVMax = gen4Config.AtkIVMax,
                FiltersSpdIV = gen4Config.FiltersSpdIV,
                SpdIVMin = gen4Config.SpdIVMin,
                SpdIVMax = gen4Config.SpdIVMax,
                UsesSynchro = gen4Config.UsesSynchro,
                IsUnownRadio = gen4Config.EncountType == (int)MainFormGen4Presenter.EncountType.Unown,
            };
        }

        // EncounterOffset(性格決定前の消費数。野生のスロット決定処理など)を取得
        static uint GetEncountOffset(MainFormGen4Presenter.EncountType encountType) => encountType switch
        {
            MainFormGen4Presenter.EncountType.Legendary => 0,
            MainFormGen4Presenter.EncountType.Roamer => 0,
            MainFormGen4Presenter.EncountType.Wild => 1,
            MainFormGen4Presenter.EncountType.Unown => 1,
            _ => 0,
        };

        // 性格決定処理を行うかどうかを取得
        static bool GetDeterminesNature(MainFormGen4Presenter.EncountType encountType) => encountType switch
        {
            MainFormGen4Presenter.EncountType.Legendary => true,
            MainFormGen4Presenter.EncountType.Roamer => false,
            MainFormGen4Presenter.EncountType.Wild => true,
            MainFormGen4Presenter.EncountType.Unown => true,
            _ => true,
        };
    }
}
