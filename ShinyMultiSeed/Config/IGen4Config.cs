namespace ShinyMultiSeed.Config
{
    public interface IGen4Config
    {
        /// <summary>
        /// ゲームがHGSSかどうかを取得/設定します。
        /// </summary>
        bool IsHgss { get; }

        /// <summary>
        /// エンカウントタイプの内部Indexを取得/設定します。
        /// </summary>
        int EncountType { get; }

        /// <summary>
        /// フィルターの色違いフラグを取得/設定します。
        /// </summary>
        bool IsShiny { get; }

        /// <summary>
        /// フィルターの表IDを取得/設定します。
        /// </summary>
        uint Tid { get; }

        /// <summary>
        /// フィルターの表IDを取得/設定します。
        /// </summary>
        uint Sid { get; }

        /// <summary>
        /// フィルターのA個体値フラグを取得/設定します。
        /// </summary>
        bool FiltersAtkIV { get; }

        /// <summary>
        /// フィルターのA個体値下限を取得/設定します。
        /// </summary>
        uint AtkIVMin { get; }

        /// <summary>
        /// フィルターのA個体値上限を取得/設定します。
        /// </summary>
        uint AtkIVMax { get; }

        /// <summary>
        /// フィルターのS個体値フラグを取得/設定します。
        /// </summary>
        bool FiltersSpdIV { get; }

        /// <summary>
        /// フィルターのS個体値下限を取得/設定します。
        /// </summary>
        uint SpdIVMin { get; }

        /// <summary>
        /// フィルターのS個体値上限を取得/設定します。
        /// </summary>
        uint SpdIVMax { get; }

        /// <summary>
        /// フィルターのシンクロ使用フラグを取得/設定します。
        /// </summary>
        bool UsesSynchro { get; }

        /// <summary>
        /// 許容フレームの下限を取得/設定します。
        /// </summary>
        uint FrameMin { get; }

        /// <summary>
        /// 許容フレームの上限を取得/設定します。
        /// </summary>
        uint FrameMax { get; }

        /// <summary>
        /// 性格値生成位置の下限を取得/設定します。
        /// </summary>
        uint PositionMin { get; }

        /// <summary>
        /// 性格値生成位置の上限を取得/設定します。
        /// </summary>
        uint PositionMax { get; }
    }
}
