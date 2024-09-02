namespace ShinyMultiSeed.Main
{
	public interface IMainFormConfigView
	{
		/// <summary>
		/// スレッド数ボタンがクリックされた時に発行されるストリームを取得します。
		/// <para> * スレッド数ボタンのインデックスを通知します。</para>
		/// </summary>
		IObservable<int> ThreadCountButtonClicked { get; }

		/// <summary>
		/// 現在選択中のスレッド数のインデックス(0開始の連番)を設定します。
		/// </summary>
		void SetThreadCountIndex(int threadCountIndex);
	}
}
