namespace ShinyMultiSeed.Main.Presenter.Internal
{
    internal sealed class Gen4ResultRowViewModel
    {
        public string? InitialSeed { get; init; }
        public uint StartPosition { get; init; }
        public uint WildSlot { get; init; }
        public string? SynchroNature { get; init; }
        public string? Pid { get; init; }
        public string? Nature { get; init; }
        public uint HpIV { get; init; }
        public uint AtkIV { get; init; }
        public uint DefIV { get; init; }
        public uint SpAtkIV { get; init; }
        public uint SpDefIV { get; init; }
        public uint SpdIV { get; init; }
    }
}
