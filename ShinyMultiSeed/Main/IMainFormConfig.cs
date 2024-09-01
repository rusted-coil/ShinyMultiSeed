namespace ShinyMultiSeed.Main
{
	public interface IMainFormConfig
	{
		/// <summary>
		/// スレッド数が変更された時に発行されるストリームを取得します。
		/// </summary>
		IObservable<int> ThreadCountChanged { get; }
	}
}
