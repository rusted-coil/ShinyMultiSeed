namespace ShinyMultiSeed.Calculator.Strategy
{
    public class Gen4SeedCheckStrategyArgs
    {
        // HGSSかどうか
        public bool IsHgss { get; init; }

        // エンカウント処理に消費する数
        public uint EncountOffset { get; init; }

        // 性格決定処理を行うかどうか
        public bool DeterminesNature { get; init; }

        // 検索する性格値生成開始位置の範囲
        public uint PositionMin { get; init; }
        public uint PositionMax { get; init; }

        // 色違い判定
        public bool IsShiny { get; init; }
        public uint Tsv { get; init; }

        // 個体値判定
        public bool FiltersAtkIV { get; init; }
        public uint AtkIVMin { get; init; }
        public uint AtkIVMax { get; init; }
        public bool FiltersSpdIV { get; init; }
        public uint SpdIVMin { get; init; }
        public uint SpdIVMax { get; init; }

        // シンクロを使用するかどうか
        public bool UsesSynchro { get; init; }

        // アンノーンラジオチェック
        public bool IsUnownRadio { get; init; }
    }
}
