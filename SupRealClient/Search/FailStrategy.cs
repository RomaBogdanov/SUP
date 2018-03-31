namespace SupRealClient.Search
{
	/// <summary>
	/// Сравнение по умолчанию
	/// </summary>
	class FailStrategy : ICompareStrategy
	{
		public bool Execute(string value1, string value2)
		{
			return false;
		}
	}
}
