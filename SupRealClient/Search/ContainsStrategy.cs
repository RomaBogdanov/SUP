namespace SupRealClient.Search
{
	/// <summary>
	/// Сравнение на вхождение в строку
	/// </summary>
	class ContainsStrategy : RegisterStrategy
	{
		public ContainsStrategy(bool register) : base(register, (string value1, string value2) => value1.Contains(value2))
		{
		}
	}
}
