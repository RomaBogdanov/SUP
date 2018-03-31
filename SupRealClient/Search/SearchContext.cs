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

		public bool Execute(string value1, string value2)
		{
			return strategy.Execute(value1 ?? "", value2 ?? "");
		}
	}
}
