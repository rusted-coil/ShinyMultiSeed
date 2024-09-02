namespace ShinyMultiSeed.Main
{
	public interface IMainFormView
	{
		/// <summary>
		/// MainFormのコンフィグを扱う部分を取得します。
		/// </summary>
		IMainFormConfigView MainFormConfig { get; }

		/// <summary>
		/// MainFormの第4世代を扱う部分を取得します。
		/// </summary>
		IMainFormGen4View MainFormGen4 { get; }
	}
}
