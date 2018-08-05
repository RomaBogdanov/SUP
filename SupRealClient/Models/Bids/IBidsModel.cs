using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Models
{
	public interface IBidsModel
	{
		/// <summary>
		/// Срабатывает при обновлении данных.
		/// </summary>
		event Action OnRefresh;

		ObservableCollection<Order> SingleOrdersSet { get; set; }
		ObservableCollection<Order> TemporaryOrdersSet { get; set; }
		ObservableCollection<Order> VirtueOrdersSet { get; set; }
		ObservableCollection<Order> OrdersSet { get; set; }
		Order CurrentSingleOrder { get; set; }
		Order CurrentTemporaryOrder { get; set; }
		Order CurrentVirtueOrder { get; set; }
		Order CurrentOrder { get; set; }
		OrderType OrderType { get; set; }
		bool IsCanAddRows { get; set; }
		Visibility IsAddUpdVisib { get; set; }
		OrderElement SelectedElement { get; set; }
		string RecOperator { get; set; }
		string NewRecOperator { get; set; }

		void AddPerson();


		/// <summary>
		/// Обработка нажатия кнопки "Начало"
		/// </summary>
		void Begin();

		void Cancel();
		void Delay();
		void Edit();
		void End();
		void Further();
		void New();
		void Next();

		/// <summary>
		/// Обрабатывает нажатие кнопки Ок.
		/// </summary>
		void Ok();

		void Prev();
		void Reload();
		void Search();
		void Agreer();

		void Signer();

		//void SignerTemp();
		void UpdatePerson();
		void DeletePerson();
	}
}