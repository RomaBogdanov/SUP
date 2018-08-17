using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SupRealClient.Models
{
	public abstract class BidsModelBase : IBidsModel
	{
		/// <summary>
		/// Данное поле необходимо для храниния информации о максимальном номере
		/// заявки, которая есть сейчас, чтобы создавать новую.
		/// </summary>
		protected static int maxOrderNumber;

		public ObservableCollection<Order> SingleOrdersSet { get; set; }
		public ObservableCollection<Order> TemporaryOrdersSet { get; set; }
		public ObservableCollection<Order> VirtueOrdersSet { get; set; }
		public ObservableCollection<Order> OrdersSet { get; set; }
		public Order CurrentSingleOrder { get; set; }
		public Order CurrentTemporaryOrder { get; set; }
		public Order CurrentVirtueOrder { get; set; }
		public Order CurrentOrder { get; set; }
		public Order GetCurrentSelectedOrder
		{
			get
			{
				switch (OrderType)
				{
					case OrderType.Single:
						return CurrentSingleOrder;
					case OrderType.Temp:
						return CurrentTemporaryOrder;
					case OrderType.Virtue:
						return CurrentVirtueOrder;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		public OrderType OrderType { get; set; }
		public bool IsCanAddRows { get; set; }
		public Visibility IsAddUpdVisib { get; set; }
		public OrderElement SelectedElement { get; set; }
		public string RecOperator { get; set; }
		public string NewRecOperator { get; set; }

		public virtual event Action OnRefresh;

		/// <summary>
		/// Обработка нажатия кнопки "Начало".
		/// </summary>
		public void Begin()
		{
			switch (OrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
				{
					if (TemporaryOrdersSet.Count > 0)
					{
						CurrentTemporaryOrder = TemporaryOrdersSet[0];
					}

					break;
				}
				case OrderType.Single:
				{
					if (SingleOrdersSet.Count > 0)
					{
						CurrentSingleOrder = SingleOrdersSet[0];
					}

					break;
				}
				case OrderType.Virtue:
				{
					if (VirtueOrdersSet.Count > 0)
					{
						CurrentVirtueOrder = VirtueOrdersSet[0];
					}

					break;
				}
				default:
					throw new Exception("Unexpected Case");
			}
		}

		/// <summary>
		/// Обработка нажатия кнопки "Предыдущий".
		/// </summary>
		public void Prev()
		{
			switch (OrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
				{
					if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
						    .IndexOf(CurrentTemporaryOrder) > 0)
					{
						CurrentTemporaryOrder = TemporaryOrdersSet[
							TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) - 1];
					}

					break;
				}
				case OrderType.Single:
				{
					if (SingleOrdersSet.Count > 0 &&
					    SingleOrdersSet.IndexOf(CurrentSingleOrder) > 0)
					{
						CurrentSingleOrder = SingleOrdersSet[
							SingleOrdersSet.IndexOf(CurrentSingleOrder) - 1];
					}

					break;
				}
				case OrderType.Virtue:
				{
					if (VirtueOrdersSet.Count > 0 &&
					    VirtueOrdersSet.IndexOf(CurrentVirtueOrder) > 0)
					{
						CurrentVirtueOrder = VirtueOrdersSet[
							VirtueOrdersSet.IndexOf(CurrentVirtueOrder) - 1];
					}

					break;
				}
			}
		}

		/// <summary>
		/// Обработка нажатия кнопки "Следующий".
		/// </summary>
		public void Next()
		{
			switch (OrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
				{
					if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
						    .IndexOf(CurrentTemporaryOrder) <
					    TemporaryOrdersSet.Count - 1)
					{
						CurrentTemporaryOrder = TemporaryOrdersSet[
							TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) + 1];
					}

					break;
				}
				case OrderType.Single:
				{
					if (SingleOrdersSet.Count > 0 &&
					    SingleOrdersSet.IndexOf(CurrentSingleOrder) <
					    SingleOrdersSet.Count - 1)
					{
						CurrentSingleOrder = SingleOrdersSet[
							SingleOrdersSet.IndexOf(CurrentSingleOrder) + 1];
					}

					break;
				}
				case OrderType.Virtue:
				{
					if (VirtueOrdersSet.Count > 0 &&
					    VirtueOrdersSet.IndexOf(CurrentVirtueOrder) <
					    VirtueOrdersSet.Count - 1)
					{
						CurrentVirtueOrder = VirtueOrdersSet[
							VirtueOrdersSet.IndexOf(CurrentVirtueOrder) + 1];
					}

					break;
				}
			}
		}

		/// <summary>
		/// Обработка нажатия кнопки "Конец".
		/// </summary>
		public void End()
		{
			switch (OrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
				{
					if (TemporaryOrdersSet.Count > 0)
					{
						CurrentTemporaryOrder = TemporaryOrdersSet[
							TemporaryOrdersSet.Count - 1];
					}

					break;
				}
				case OrderType.Single:
				{
					if (SingleOrdersSet.Count > 0)
					{
						CurrentSingleOrder = SingleOrdersSet[
							SingleOrdersSet.Count - 1];
					}

					break;
				}
				case OrderType.Virtue:
				{
					if (VirtueOrdersSet.Count > 0)
					{
						CurrentVirtueOrder = VirtueOrdersSet[
							VirtueOrdersSet.Count - 1];
					}

					break;
				}
			}
		}

		/// <summary>
		/// Обработка нажатия кнопки "Далее".
		/// </summary>
		public void Further()
		{
			//todo: обработать команду
		}

		/// <summary>
		/// Обработка нажатия кнопки "Отложить".
		/// </summary>
		public void Delay()
		{
			//todo: обработать команду
		}

		/// <summary>
		/// Обработка нажатия кнопки "Новый".
		/// </summary>
		public void New()
		{
			//todo: наверно, надо удалить, потому что обработка на уровне ViewModel
		}

		/// <summary>
		/// Обработка нажатия кнопки "Правка".
		/// </summary>
		public void Edit()
		{
			//todo: наверно, надо удалить, потому что обработка на уровне ViewModel
		}

		/// <summary>
		/// Обработка нажатия кнопки "Принять".
		/// </summary>
		public virtual void Ok()
		{
		}

		/// <summary>
		/// Обработка нажатия кнопки "Отмена".
		/// </summary>
		public void Cancel()
		{
			//todo: наверно, надо удалить, потому что обработка на уровне ViewModel
		}

		/// <summary>
		/// Обработка нажатия кнопки "Найти".
		/// </summary>
		public void Search()
		{
			ViewManager.Instance.OpenWindowModal("BidsListWindView", null);
			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Обработка нажатия кнопки "Обновить".
		/// </summary>
		public void Reload()
		{
			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Обработка нажатия кнопок добавления нового человека в заявку.
		/// </summary>
		public void AddPerson()
		{
			switch (OrderType)
			{
				case OrderType.Temp:
					AddPersonInTempOrder();
					break;
				case OrderType.Single:
					AddPersonInSingleOrder();
					break;
				case OrderType.Virtue:
					throw new Exception("Unexpected Case");
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Обработка нажатия кнопок редактирования нового человека в заявку.
		/// </summary>
		public void UpdatePerson()
		{
			if (SelectedElement == null)
			{
				return;
			}

			AddUpdateAbstrModel model = new UpdateBidModel(SelectedElement);
			int i = GetCurrentSelectedOrder.OrderElements.IndexOf(SelectedElement);
			object res = OpenWindow(model);
			if (res == null) return;

			GetCurrentSelectedOrder.OrderElements[i] = (res as OrderElement);
			SelectedElement = GetCurrentSelectedOrder.OrderElements[i];
			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Обработка нажатия кнопок удаления нового человека из заявки.
		/// </summary>
		public void DeletePerson()
		{
			if (SelectedElement == null)
			{
				return;
			}

			SelectedElement.IsDeleted = true;
			GetCurrentSelectedOrder.OrderElements.Remove(SelectedElement);
		}

		/// <summary>
		/// Обработка нажатия кнопки для добавления согласующего заявку.
		/// </summary>
		public void Agreer()
		{
			var model = new VisitorsListModel<Visitor>();
			model.IsRequiredCanAgree = true;
			var viewModel = new Base4ViewModel<Visitor>()
			{
				OkCaption = "OK",
				Model = model
			};
			var view = new VisitorsListWindView(viewModel);
			view.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			model.OnClose += view.Handling_OnClose;
			view.ShowDialog();
			VisitorsModelResult result = view.WindowResult as VisitorsModelResult;

			if (result == null) return;
			CurrentTemporaryOrder.AgreeId = result.Id;
			CurrentTemporaryOrder.Agree = result.Name;
			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Обработка нажатия кнопки "Правка".
		/// </summary>
		public void Signer()
		{
			var model = new VisitorsListModel<Visitor>();
			model.IsRequiredCanSign = true;
			var viewModel = new Base4ViewModel<Visitor>()
			{
				OkCaption = "OK",
				Model = model
			};
			var view = new VisitorsListWindView(viewModel);
			view.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			model.OnClose += view.Handling_OnClose;
			view.ShowDialog();
			VisitorsModelResult result = view.WindowResult as VisitorsModelResult;

			if (result == null) return;
			switch (OrderType)
			{
				case OrderType.Temp:
				{
					CurrentTemporaryOrder.SignedId = result.Id;
					CurrentTemporaryOrder.Signed = result.Name;
					break;
				}
				case OrderType.Single:
				{
					CurrentSingleOrder.SignedId = result.Id;
					CurrentSingleOrder.Signed = result.Name;
					break;
				}
				case OrderType.Virtue:
				{
					CurrentVirtueOrder.SignedId = result.Id;
					CurrentVirtueOrder.Signed = result.Name;
					break;
				}
				default:
					break;
			}

			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Процедура обрабатывающая добавление персоны в разовой заявке.
		/// </summary>
		protected void AddPersonInSingleOrder()
		{
			AddUpdateAbstrModel model = new AddSingleBidModel(true, CurrentSingleOrder.From, CurrentSingleOrder.From);
			object res = OpenWindow(model);
			if (res == null) return;
			if (CurrentSingleOrder.OrderElements == null)
				CurrentSingleOrder.OrderElements = new ObservableCollection<OrderElement>();
			CurrentSingleOrder.OrderElements.Add((OrderElement) res);
			OnRefresh?.Invoke();
		}

		/// <summary>
		/// Процедура обрабатывающая добавление персоны во временной заявке.
		/// </summary>
		protected void AddPersonInTempOrder()
		{
			AddUpdateAbstrModel model = new AddSingleBidModel(false, CurrentTemporaryOrder.From, CurrentTemporaryOrder.To);
			object res = OpenWindow(model);
			if (res == null) return;
			if (CurrentTemporaryOrder.OrderElements == null)
			{
				CurrentTemporaryOrder.OrderElements = new ObservableCollection<OrderElement>();
			}

			CurrentTemporaryOrder.OrderElements.Add((OrderElement) res);
			OnRefresh?.Invoke();
		}

		private object OpenWindow(AddUpdateAbstrModel model)
		{
			AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel(model)
			{
				Model = model
			};
			AddUpdateBidWindView view = new AddUpdateBidWindView();
			view.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			view.Title = "Добавить посетителя";
			view.DataContext = viewModel;
			model.OnClose += view.Handling_OnClose;
			view.ShowDialog();
			return view.WindowResult;
		}

	}
}