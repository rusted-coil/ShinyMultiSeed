using ShinyMultiSeed.Calculator.Strategy;
using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Main
{
    internal static class ConfigConverter
    {
        public static Gen4SeedCheckStrategyArgs ConvertToGen4SeedCheckStrategyArgs(ConfigData config, Gen4Config gen4Config)
        {
            return new Gen4SeedCheckStrategyArgs
            {
                IsHgss = gen4Config.IsHgss,
                PositionMin = gen4Config.PositionMin,
                PositionMax = gen4Config.PositionMax,
                EncountOffset = GetEncountOffset((MainForm.EncountType)gen4Config.EncountType),
                DeterminesNature = GetDeterminesNature((MainForm.EncountType)gen4Config.EncountType),
                IsShiny = gen4Config.IsShiny,
                Tsv = (gen4Config.Tid ^ gen4Config.Sid) & 0xfff8,
                FiltersAtkIV = gen4Config.FiltersAtkIV,
                AtkIVMin = gen4Config.AtkIVMin,
                AtkIVMax = gen4Config.AtkIVMax,
                FiltersSpdIV = gen4Config.FiltersSpdIV,
                SpdIVMin = gen4Config.SpdIVMin,
                SpdIVMax = gen4Config.SpdIVMax,
                UsesSynchro = gen4Config.UsesSynchro,
            };
        }

        // EncounterOffset(性格決定前の消費数。野生のスロット決定処理など)を取得
        static uint GetEncountOffset(MainForm.EncountType encountType) => encountType switch
        {
            MainForm.EncountType.Legendary => 0,
            MainForm.EncountType.Roamer => 0,
            MainForm.EncountType.Wild => 1,
            MainForm.EncountType.Unown => 1,
            _ => 0,
        };

        // 性格決定処理を行うかどうかを取得
        static bool GetDeterminesNature(MainForm.EncountType encountType) => encountType switch
        {
            MainForm.EncountType.Legendary => true,
            MainForm.EncountType.Roamer => false,
            MainForm.EncountType.Wild => true,
            MainForm.EncountType.Unown => true,
            _ => true,
        };
    }
}
