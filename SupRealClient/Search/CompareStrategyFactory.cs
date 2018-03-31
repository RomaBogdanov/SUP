namespace SupRealClient.Search
{
	/// <summary>
	/// Фабрика стратегий сравнения
	/// </summary>
	static class CompareStrategyFactory
	{
		public static ICompareStrategy Create(bool register, bool equal, bool startWith, bool contains)
		{
			if (equal)
			{
				return new EqualStrategy(register);
			}
			if (startWith)
			{
				return new StartWithStrategy(register);
			}
			if (contains)
			{
				return new ContainsStrategy(register);
			}

			return new FailStrategy();
		}
	}
}
