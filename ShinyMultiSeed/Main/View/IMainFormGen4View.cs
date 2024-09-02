using FormRx.Button;

namespace ShinyMultiSeed.Main.View
{
    public interface IMainFormGen4View
    {
        /// <summary>
        /// HGSSのチェックボックスの状態を取得します。
        /// </summary>
        bool IsHgssChecked { get; set; }

        /// <summary>
        /// 選択中のエンカウント種別のIndexを取得します。
        /// </summary>
        int EncountType { get; set; }

        /// <summary>
        /// フィルターの色違いチェックボックスの状態を取得します。
        /// </summary>
        bool IsShinyChecked { get; set; }

        /// <summary>
        /// 表ID欄に入力されている文字列を取得します。
        /// </summary>
        string TidText { get; set; }

        /// <summary>
        /// 裏ID欄に入力されている文字列を取得します。
        /// </summary>
        string SidText { get; set; }

        /// <summary>
        /// フィルターのA個体値チェックボックスの状態を取得します。
        /// </summary>
        bool FiltersAtkIVChecked { get; set; }

        /// <summary>
        /// A個体値の下限に入力されている値を取得します。
        /// </summary>
        decimal AtkIVMinValue { get; set; }

        /// <summary>
        /// A個体値の上限に入力されている値を取得します。
        /// </summary>
        decimal AtkIVMaxValue { get; set; }

        /// <summary>
        /// フィルターのS個体値チェックボックスの状態を取得します。
        /// </summary>
        bool FiltersSpdIVChecked { get; set; }

        /// <summary>
        /// S個体値の下限に入力されている値を取得します。
        /// </summary>
        decimal SpdIVMinValue { get; set; }

        /// <summary>
        /// S個体値の上限に入力されている値を取得します。
        /// </summary>
        decimal SpdIVMaxValue { get; set; }

        /// <summary>
        /// フィルターのシンクロを使用するチェックボックスの状態を取得します。
        /// </summary>
        bool UsesSynchroChecked { get; set; }

        /// <summary>
        /// フレームの下限に入力されている文字列を取得します。
        /// </summary>
        string FrameMinText { get; set; }

        /// <summary>
        /// フレームの上限に入力されている文字列を取得します。
        /// </summary>
        string FrameMaxText { get; set; }

        /// <summary>
        /// 性格値生成位置の下限に入力されている文字列を取得します。
        /// </summary>
        string PositionMinText { get; set; }

        /// <summary>
        /// 性格値生成位置の上限に入力されている文字列を取得します。
        /// </summary>
        string PositionMaxText { get; set; }

        /// <summary>
        /// HGSSかどうかのチェック状態が変わった時に発行されるストリームを取得します。
        /// <para> * チェックされているかどうか。このイベントが発行された時点でIsHgssCheckedの値も同じ値をとります。</para>
        /// </summary>
        IObservable<bool> IsHgssCheckedChanged { get; }

        /// <summary>
        /// 計算ボタンを取得します。
        /// </summary>
        IButton CalculateButton { get; }

        /// <summary>
        /// 選択可能なエンカウント種別のIDと文字列の組を設定します。
        /// </summary>
        void SetSelectableEncountTypes(IReadOnlyList<KeyValuePair<int, string>> encountTypes);

        /// <summary>
        /// 現在計算中かどうかを設定します。
        /// </summary>
        void SetIsCalculating(bool isCalculating);
    }
}
