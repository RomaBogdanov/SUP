using SupRealClient.EnumerationClasses;

namespace SupRealClient.Models.Helpers
{
	/// <summary>
	/// Класс общих статических функций для работы с классом карт
	/// </summary>
	public static class CardsHelper
	{
		public static string CardStateToSting(CardState state)
		{
			switch (state)
			{
				case CardState.Unknown:
					return "Неизвестно";
				case CardState.Active:
					return "Активен";
				case CardState.Inactive:
					return "Неактивен";
				case CardState.Issued:
					return "Выдан";
				case CardState.Lost:
					return "Утерян";
				case CardState.Returnded:
					return "Возвращен";
				default:
					return "Неизвестно";
			}
		}
	}
}
