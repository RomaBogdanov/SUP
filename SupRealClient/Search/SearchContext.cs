namespace SupRealClient.Search
{
	/// <summary>
	/// Контекст поиска с использованием различных стратегий
	/// </summary>
	class SearchContext
	{
		private ICompareStrategy strategy;

		public void SetStrategy(ICompareStrategy _strategy)
		{
			strategy = _strategy;
		}

		public bool Execute(object value1, string value2)
		{
            string str = value1 != null ? value1.ToString() : "";
			return strategy.Execute(str ?? "", value2 ?? "");
		}
	}
}
