using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;

namespace SupRealClient.ViewModels
{
	public class BidsViewModel : ViewModelBase
	{
		private IBidsModel bidsModel;

		private bool _isEnabled = false;

		/// <summary>
		/// Доступность вкладок TabControl.
		/// </summary>
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				_isEnabled = value;
				OnPropertyChanged(nameof(IsEnabled));
			}
		}

		public IBidsModel BidsModel
		{
			get { return bidsModel; }
			set
			{
				bidsModel = value;
				OnPropertyChanged();
				OrdersSet = bidsModel.OrdersSet;
				TemporaryOrdersSet = bidsModel.TemporaryOrdersSet;
				SingleOrdersSet = bidsModel.SingleOrdersSet;
				VirtueOrdersSet = bidsModel.VirtueOrdersSet;
				CurrentOrder = bidsModel.CurrentOrder;
				
				IsCanAddRows = bidsModel.IsCanAddRows;
				AddUpdVisib = bidsModel.IsAddUpdVisib;
				bidsModel.OrderType = CurrentOrderType;
				bidsModel.OnRefresh += BidsModel_OnRefresh;
			}
		}

		private void BidsModel_OnRefresh()
		{
			CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
			CurrentTemporaryOrder.OrderElements = BidsModel.CurrentTemporaryOrder.OrderElements;

			CurrentSingleOrder = BidsModel.CurrentSingleOrder;
			CurrentSingleOrder.OrderElements = BidsModel.CurrentSingleOrder.OrderElements;

			CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
			CurrentVirtueOrder.OrderElements = BidsModel.CurrentVirtueOrder.OrderElements;

			//UpdateVisitor = BidsModel.UpdateVisitor;
		}

		public OrderElement UpdateVisitor
		{
			get { return BidsModel?.UpdateVisitor; }
			set
			{
				BidsModel.UpdateVisitor = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Order> SingleOrdersSet
		{
			get { return BidsModel?.SingleOrdersSet; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.SingleOrdersSet = value;
					OnPropertyChanged();
				}
			}
		}

		public Order CurrentSingleOrder
		{
			get { return BidsModel?.CurrentSingleOrder; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.CurrentSingleOrder = value;
					OnPropertyChanged();
				}
			}
		}

		public ObservableCollection<Order> TemporaryOrdersSet
		{
			get { return BidsModel?.TemporaryOrdersSet; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.TemporaryOrdersSet = value;
					OnPropertyChanged();
				}
			}
		}

		public Order CurrentTemporaryOrder
		{
			get { return BidsModel?.CurrentTemporaryOrder; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.CurrentTemporaryOrder = value;
					OnPropertyChanged();
				}
			}
		}

		public ObservableCollection<Order> VirtueOrdersSet
		{
			get { return BidsModel?.VirtueOrdersSet; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.VirtueOrdersSet = value;
					OnPropertyChanged();
				}
			}
		}

		public Order CurrentVirtueOrder
		{
			get { return BidsModel?.CurrentVirtueOrder; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.CurrentVirtueOrder = value;
					OnPropertyChanged();
				}
			}
		}

		public ObservableCollection<Order> OrdersSet
		{
			get { return BidsModel?.OrdersSet; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.OrdersSet = value;
					OnPropertyChanged();
				}
			}
		}

		public Order CurrentOrder
		{
			get { return BidsModel?.CurrentOrder; }
			set
			{
				if (BidsModel != null)
				{
					BidsModel.CurrentOrder = value;
					OnPropertyChanged();
				}
			}
		}

		private bool isTempOrder;

		public bool IsTempOrder
		{
			get { return isTempOrder; }
			set
			{
				isTempOrder = value;
				if (isTempOrder)
				{
					BidsModel.OrderType = OrderType.Temp;
				}

				OnPropertyChanged();
			}
		}

		private bool isSingleOrder;


		public bool IsSingleOrder
		{
			get { return isSingleOrder; }
			set
			{
				isSingleOrder = value;
				if (isSingleOrder)
				{
					BidsModel.OrderType = OrderType.Single;
				}

				OnPropertyChanged();
			}
		}

		private bool isVirtueOrder;


		public bool IsVirtueOrder
		{
			get { return isVirtueOrder; }
			set
			{
				isVirtueOrder = value;
				if (IsVirtueOrder)
				{
					BidsModel.OrderType = OrderType.Virtue;
				}

				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Свойство для определения текущей открытой вкладки.
		/// </summary>
		private OrderType CurrentOrderType
		{
			get
			{
				if (IsTempOrder)
				{
					return OrderType.Temp;
				}
				else if (IsSingleOrder)
				{
					return OrderType.Single;
				}
				else
				{
					if (IsVirtueOrder)
					{
						return OrderType.Virtue;
					}
				}

				return OrderType.Single;
			}
		}

		public bool IsCanAddRows
		{
			get
			{
				if (BidsModel == null)
				{
					return false;
				}

				return BidsModel.IsCanAddRows;
			}
			set
			{
				BidsModel.IsCanAddRows = value;
				OnPropertyChanged();
			}
		}

		public Visibility AddUpdVisib
		{
			get { return BidsModel.IsAddUpdVisib; }
			set
			{
				BidsModel.IsAddUpdVisib = value;
				OnPropertyChanged();
			}
		}

		public ICommand BeginCommand { get; set; }
		public ICommand PrevCommand { get; set; }
		public ICommand NextCommand { get; set; }
		public ICommand EndCommand { get; set; }
		public ICommand SearchCommand { get; set; }
		public ICommand DelayCommand { get; set; }
		public ICommand NewCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand OkCommand { get; set; }
		public ICommand CancelCommand { get; set; }
		public ICommand FurtherCommand { get; set; }
		public ICommand ReloadCommand { get; set; }

		public ICommand AddPersonCommand { get; set; } // добавление человека в заявку
		public ICommand UpdatePersonCommand { get; set; } // редактирование человека в заявке
		public ICommand DeletePersonCommand { get; set; } // удаление человека из заявки

		public ICommand SignerCommand { get; set; } // посписывающий заявку

		//public ICommand SignerTempCommand { get; set; } // посписывающий временную
		public ICommand AgreerCommand { get; set; } // согласующий

		private bool _textEnable = false;

		/// <summary>
		/// Доступность редактирования полей.
		/// </summary>
		public bool TextEnable
		{
			get { return _textEnable; }
			set
			{
				_textEnable = value;
				OnPropertyChanged(nameof(TextEnable));
			}
		}

		private bool _acceptButtonEnable = false;

		/// <summary>
		/// Доступность кнопок Применить и Отмена.
		/// </summary>
		public bool AcceptButtonEnable
		{
			get { return _acceptButtonEnable; }
			set
			{
				_acceptButtonEnable = value;
				NavigateButtonEnable = !value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Доступность кнопок нижнего ряда, кроме кнопок Применить, Отмена.
		/// </summary>
		public bool NavigateButtonEnable
		{
			get { return !_acceptButtonEnable; }
			set { OnPropertyChanged(); }
		}

		/// <summary>
		/// Настройки положения и размера окна.
		/// </summary>
		public ChildWinSet WinSet { get; set; }

		public BidsViewModel()
		{
			// Задать размеры и положение формы.
			WinSet = new ChildWinSet() {Left = 0};
			WinSet.Left = WinSet.Width;

			// Инициализация команд.
			BeginCommand = new RelayCommand(arg => Begin());
			PrevCommand = new RelayCommand(arg => Prev());
			NextCommand = new RelayCommand(arg => Next());
			EndCommand = new RelayCommand(arg => End());
			SearchCommand = new RelayCommand(arg => Search());
			DelayCommand = new RelayCommand(arg => Delay());
			NewCommand = new RelayCommand(arg => New());
			EditCommand = new RelayCommand(arg => Edit());
			OkCommand = new RelayCommand(arg => Ok());
			CancelCommand = new RelayCommand(arg => Cancel());
			FurtherCommand = new RelayCommand(arg => Further());
			ReloadCommand = new RelayCommand(arg => Reload());

			AddPersonCommand = new RelayCommand(arg => AddPerson());
			UpdatePersonCommand = new RelayCommand(arg => UpdatePerson());
			DeletePersonCommand = new RelayCommand(arg => DeletePerson());

			SignerCommand = new RelayCommand(arg => Signer());
			//SignerTempCommand = new RelayCommand(arg => SignerTemp());
			AgreerCommand = new RelayCommand(arg => Agreer());

			TextEnable = false; // При открытии окна поля недоступны.
			AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
			IsEnabled = true;
		}

		private void Agreer()
		{
			BidsModel.Agreer();
		}

		private void Signer()
		{
			BidsModel.Signer();
		}
		/*
		private void SignerTemp()
		{
		    BidsModel.SignerTemp();
		}*/

		/// <summary>
		/// Добавляет человека в заявку.
		/// </summary>
		private void AddPerson()
		{
			BidsModel.AddPerson();
		}

		/// <summary>
		/// Обновляет информацию о человеке в заявке.
		/// </summary>
		private void UpdatePerson()
		{
			BidsModel.UpdatePerson();
			UpdateVisitor = BidsModel.UpdateVisitor;
		}

		/// <summary>
		/// Удаляет информацию о человеке в заявке.
		/// </summary>
		private void DeletePerson()
		{
			BidsModel.DeletePerson();
		}

		private void Begin()
		{
			BidsModel.Begin();

			SetCurrentSelectedOrderTrue();
		}

		private void End()
		{
			BidsModel.End();

			SetCurrentSelectedOrderTrue();
		}

		private void Next()
		{
			BidsModel.Next();

			SetCurrentSelectedOrderTrue();
		}

		private void Prev()
		{
			BidsModel.Prev();

			SetCurrentSelectedOrderTrue();
		}

		private void Search()
		{
			BidsModel.Search();
		}

		private void Delay()
		{
			BidsModel.Delay();
		}

		/// <summary>
		/// Создание новой заявки
		/// </summary>
		private void New()
		{
			BidsModel = new NewBidsModel(CurrentOrderType);
			
			TextEnable = true; // При открытии окна поля недоступны.
			AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
			IsEnabled = false;
		}
		
		/// <summary>
		/// Редактирование заявки.
		/// </summary>
		private void Edit()
		{
			BidsModel = new EditBidsModel(CurrentSingleOrder,
				CurrentTemporaryOrder, CurrentVirtueOrder, CurrentOrder);

			SetCurrentSelectedEdit();

			TextEnable = true; // При открытии окна поля недоступны.
			AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
			IsEnabled = false;
		}

		private void Ok()
		{
			BidsModel.Ok();
			BidsModel = new BidsModel();

			SetCurrentSelectedOk();

			TextEnable = false;
			AcceptButtonEnable = false;
			IsEnabled = true;
		}

		private void SetCurrentSelectedOk()
		{
			switch (CurrentOrderType)
			{
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.TemporaryOrdersSet.FirstOrDefault(x=>x.Id == CurrentSelectedOrder.Id);
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.SingleOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.VirtueOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetCurrentSelectedEdit()
		{
			switch (CurrentOrderType)
			{
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
					CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder;
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.CurrentSingleOrder;
					CurrentSelectedOrder = BidsModel.CurrentSingleOrder;
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
					CurrentSelectedOrder = BidsModel.CurrentVirtueOrder;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetCurrentSelectedOrderTrue()
		{
			switch (CurrentOrderType)
			{
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
					CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder; // Запомнить временную заявку перед добавлением новой.
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.CurrentSingleOrder;
					CurrentSelectedOrder = BidsModel.CurrentSingleOrder; // Запомнить разовую заявку перед добавлением новой.
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
					CurrentSelectedOrder = BidsModel.CurrentVirtueOrder; // Запомнить заявку на основании перед добавлением новой.
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		private void SetCurrentSelectedOrder(bool changeOrder = false)
		{
			switch (CurrentOrderType)
			{
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder; 
					if (changeOrder == true)
					{
						CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder; // Запомнить временную заявку перед добавлением новой.
					}
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.CurrentSingleOrder;
					if (changeOrder == true)
					{
						CurrentSelectedOrder = BidsModel.CurrentSingleOrder; // Запомнить разовую заявку перед добавлением новой.
					}
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
					if (changeOrder == true)
					{
						CurrentSelectedOrder = BidsModel.CurrentVirtueOrder; // Запомнить заявку на основании перед добавлением новой.
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void FindAndChangeOrder()
		{
			switch (CurrentOrderType)
			{
				case OrderType.Single:
					if (FindOrder(SingleOrdersSet, CurrentSelectedOrder.Id) != null)
					{
						CurrentSingleOrder = SingleOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					}
					CurrentSelectedOrder = CurrentSingleOrder;
					break;
				case OrderType.Temp:
					if (FindOrder(TemporaryOrdersSet, CurrentSelectedOrder.Id) != null)
					{
						CurrentTemporaryOrder = TemporaryOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					}
					CurrentSelectedOrder = CurrentSingleOrder;
					break;
				case OrderType.Virtue:
					if (FindOrder(VirtueOrdersSet, CurrentSelectedOrder.Id) != null)
					{
						CurrentVirtueOrder = VirtueOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					}
					CurrentSelectedOrder = CurrentSingleOrder;
					break;
			}
		}

		/// <summary>
		/// Выбранная заявка перед добавлением новой. Может быть тип "Временная", "Разовая", "На основании"
		/// </summary>
		private Order CurrentSelectedOrder { get; set; }

		private void Cancel()
		{
			BidsModel = new BidsModel();

			SetCurrentSelectedOk();

			TextEnable = false; // При открытии окна поля недоступны.
			AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
			IsEnabled = true;
		}

		private Order FindOrder(ObservableCollection<Order> orderSet, int id)
		{
			return orderSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
		}

		private void Further()
		{
			BidsModel.Further();
		}

		private void Reload()
		{
			BidsModel.Reload();
		}
	}
}