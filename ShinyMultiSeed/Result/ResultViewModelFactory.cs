namespace ShinyMultiSeed.Result
{
    public static class ResultViewModelFactory
    {
        public static IResultColumnViewModel CreateColumn(string id, string displayText, int? width = null)
        { 
            return new Internal.ResultColumn { 
                Id = id,
                DisplayText = displayText,
                Width = width,
            };
        }

        public static IReadOnlyList<IResultColumnViewModel> CreateColumns(params (string Id, string DisplayText)[] values)
        {
            return values.Select(x => new Internal.ResultColumn { Id = x.Id, DisplayText = x.DisplayText, }).ToArray();
        }

        public static IResultViewModel Create(string overviewText, IReadOnlyList<IResultColumnViewModel> columns, IReadOnlyList<object> rows)
        {
            return new Internal.ResultViewModel
            { 
                OverviewText = overviewText,
                Columns = columns,
                Rows = rows,
            };
        }
    }
}
