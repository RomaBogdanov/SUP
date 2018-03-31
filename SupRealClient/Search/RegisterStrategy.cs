namespace SupRealClient.Search
{
	/// <summary>
	/// Сравнение с/без учета регистра
	/// </summary>
	abstract class RegisterStrategy : ICompareStrategy
	{
		public delegate bool Compare(string value1, string value2);

		bool register;
		Compare action;

		public RegisterStrategy(bool _register, Compare _action)
		{
			register = _register;
			action = _action;
		}

		public bool Execute(string value1, string value2)
		{
			return action(register ? value1 : value1.ToUpper(), register ? value2 : value2.ToUpper());
		}
	}
}
