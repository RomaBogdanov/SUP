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
            value1 = value1.Replace('Ё', 'Е');
            value1 = value1.Replace('ё', 'е');
            value2 = value2.Replace('Ё', 'Е');
            value2 = value2.Replace('ё', 'е');

            return action(register ? value1 : value1.ToUpper(),
                register ? value2 : value2.ToUpper());
		}
	}
}
