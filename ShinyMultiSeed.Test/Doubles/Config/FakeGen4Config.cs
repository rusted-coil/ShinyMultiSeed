using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Test.Doubles.Config
{
    internal class FakeGen4Config : IModifiableGen4Config
    {
        public bool IsHgss { get; set; }
        public int EncountType { get; set; }
        public bool IsShiny { get; set; }
        public uint Tid { get; set; }
        public uint Sid { get; set; }
        public bool FiltersAtkIV { get; set; }
        public uint AtkIVMin { get; set; }
        public uint AtkIVMax { get; set; }
        public bool FiltersSpdIV { get; set; }
        public uint SpdIVMin { get; set; }
        public uint SpdIVMax { get; set; }
        public bool UsesSynchro { get; set; }
        public uint FrameMin { get; set; }
        public uint FrameMax { get; set; }
        public uint PositionMin { get; set; }
        public uint PositionMax { get; set; }
    }
}
