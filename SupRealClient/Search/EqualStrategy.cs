namespace SupRealClient.Search
{
	/// <summary>
	/// Сравнение на равенство строк
	/// </summary>
	class EqualStrategy : RegisterStrategy
	{
		public EqualStrategy(bool register) : base(register, (string value1, string value2) => value1.Equals(value2))
		{
		}
	}
}
