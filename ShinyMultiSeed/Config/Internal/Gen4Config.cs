namespace ShinyMultiSeed.Config.Internal
{
    /// <summary>
    /// 第4世代の条件設定のフォームデータに対応するコンフィグです。
    /// </summary>
    public sealed class Gen4Config : IModifiableGen4Config
    {
        public bool IsHgss { get; set; } = true;
        public int EncountType { get; set; } = 0;
        public bool IsShiny { get; set; } = true;
        public uint Tid { get; set; } = 0;
        public uint Sid { get; set; } = 0;
        public bool FiltersAtkIV { get; set; } = false;
        public uint AtkIVMin { get; set; } = 0;
        public uint AtkIVMax { get; set; } = 1;
        public bool FiltersSpdIV { get; set; } = false;
        public uint SpdIVMin { get; set; } = 0;
        public uint SpdIVMax { get; set; } = 1;
        public bool UsesSynchro { get; set; } = false;
        public uint FrameMin { get; set; } = 900;
        public uint FrameMax { get; set; } = 1500;
        public uint PositionMin { get; set; } = 0;
        public uint PositionMax { get; set; } = 150;
        public uint MultiSeedCount { get; set; } = 2;
    }
}
