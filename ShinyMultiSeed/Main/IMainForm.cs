namespace ShinyMultiSeed.Main
{
	public interface IMainForm
	{
		/// <summary>
		/// MainFormのコンフィグを扱う部分を取得します。
		/// </summary>
		IMainFormConfig MainFormConfig { get; }

		/// <summary>
		/// MainFormの第4世代を扱う部分を取得します。
		/// </summary>
		IMainFormGen4 MainFormGen4 { get; }
	}
}
