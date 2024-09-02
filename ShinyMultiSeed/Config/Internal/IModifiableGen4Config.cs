namespace ShinyMultiSeed.Config.Internal
{
    internal interface IModifiableGen4Config : IGen4Config
    {
        /// <summary>
        /// ゲームがHGSSかどうかを取得/設定します。
        /// </summary>
        new bool IsHgss { get; set; }

        /// <summary>
        /// エンカウントタイプの内部Indexを取得/設定します。
        /// </summary>
        new int EncountType { get; set; }

        /// <summary>
        /// フィルターの色違いフラグを取得/設定します。
        /// </summary>
        new bool IsShiny { get; set; }

        /// <summary>
        /// フィルターの表IDを取得/設定します。
        /// </summary>
        new uint Tid { get; set; }

        /// <summary>
        /// フィルターの表IDを取得/設定します。
        /// </summary>
        new uint Sid { get; set; }

        /// <summary>
        /// フィルターのA個体値フラグを取得/設定します。
        /// </summary>
        new bool FiltersAtkIV { get; set; }

        /// <summary>
        /// フィルターのA個体値下限を取得/設定します。
        /// </summary>
        new uint AtkIVMin { get; set; }

        /// <summary>
        /// フィルターのA個体値上限を取得/設定します。
        /// </summary>
        new uint AtkIVMax { get; set; }

        /// <summary>
        /// フィルターのS個体値フラグを取得/設定します。
        /// </summary>
        new bool FiltersSpdIV { get; set; }

        /// <summary>
        /// フィルターのS個体値下限を取得/設定します。
        /// </summary>
        new uint SpdIVMin { get; set; }

        /// <summary>
        /// フィルターのS個体値上限を取得/設定します。
        /// </summary>
        new uint SpdIVMax { get; set; }

        /// <summary>
        /// フィルターのシンクロ使用フラグを取得/設定します。
        /// </summary>
        new bool UsesSynchro { get; set; }

        /// <summary>
        /// 許容フレームの下限を取得/設定します。
        /// </summary>
        new uint FrameMin { get; set; }

        /// <summary>
        /// 許容フレームの上限を取得/設定します。
        /// </summary>
        new uint FrameMax { get; set; }

        /// <summary>
        /// 性格値生成位置の下限を取得/設定します。
        /// </summary>
        new uint PositionMin { get; set; }

        /// <summary>
        /// 性格値生成位置の上限を取得/設定します。
        /// </summary>
        new uint PositionMax { get; set; }
    }
}
