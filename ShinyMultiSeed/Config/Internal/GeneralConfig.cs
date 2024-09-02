namespace ShinyMultiSeed.Config.Internal
{
    internal sealed class GeneralConfig : IModifiableGeneralConfig
    {
        public int ThreadCount { get; set; } = 16;
    }
}
