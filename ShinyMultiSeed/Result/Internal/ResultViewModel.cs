namespace ShinyMultiSeed.Result.Internal
{
    internal sealed class ResultViewModel : IResultViewModel
    {
        public string OverviewText { get; init; } = string.Empty;
        public IReadOnlyList<IResultColumnViewModel> Columns { get; init; } = Array.Empty<IResultColumnViewModel>();
        public IReadOnlyList<object> Rows { get; init; } = Array.Empty<object>();
    }

    internal sealed class ResultColumn : IResultColumnViewModel
    {
        public string Id { get; init; } = string.Empty;
        public string DisplayText { get; init; } = string.Empty;
        public int? Width { get; init; } = null;
    }
}
