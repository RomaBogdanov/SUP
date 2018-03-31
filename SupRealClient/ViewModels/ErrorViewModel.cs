using System;

namespace SupRealClient.ViewModels
{
	/// <summary>
	/// ViewModel, обрабатывающий системные ошибки
	/// </summary>
	public class ErrorViewModel
	{
		Exception exeption;
		string description;

		public ErrorViewModel(Exception ex, string _description)
		{
			exeption = ex;
			description = _description;
		}

		public string Message { get { return exeption.Message; } }

		public string Description { get { return description; } }

		public string Info { get { return exeption.StackTrace; } }
	}
}
