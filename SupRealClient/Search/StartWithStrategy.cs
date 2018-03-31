namespace SupRealClient.Search
{
	/// <summary>
	/// Сравнение на начало строки
	/// </summary>
	class StartWithStrategy : RegisterStrategy
	{
		public StartWithStrategy(bool register) : base(register, (string value1, string value2) => value1.StartsWith(value2))
		{
		}
	}
}
