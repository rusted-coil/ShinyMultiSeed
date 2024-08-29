using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Main
{
    internal static class ConfigConverter
    {
        public static Gen4SeedCalculatorArgs ConvertToGen4SeedCalculatorArgs(ConfigData config, Gen4Config gen4Config)
        {
            return new Gen4SeedCalculatorArgs
            {
                IsHgss = gen4Config.IsHgss,
                FrameMin = gen4Config.FrameMin,
                FrameMax = gen4Config.FrameMax,
                PositionMin = gen4Config.PositionMin,
                PositionMax = gen4Config.PositionMax,
                EncountOffset = 0, // TODO
                DeterminesNature = true, // TODO
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
    }
}
