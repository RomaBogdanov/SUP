namespace SupRealClient.Search
{
	/// <summary>
	/// Стратегия сравнения
	/// </summary>
	interface ICompareStrategy
	{
		bool Execute(string value1, string value2);
	}
}
