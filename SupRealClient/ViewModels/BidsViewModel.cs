using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Models.Helpers;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace SupRealClient.ViewModels
{
	public class BidsViewModel : ViewModelBase
	{
		#region Properties

		// Открыта из выдачи пропусков по заявке по кнопке "на основании заявки" 
		private bool isToVirtue = false;

		private IBidsModel bidsModel;


		/// <summary>
		/// Выбранная заявка перед добавлением новой. Может быть тип "Временная", "Разовая", "На основании"
		/// </summary>
		private Order CurrentSelectedOrder { get; set; }

		private bool isEnabled = false;
		/// <summary>
		/// Доступность вкладок TabControl.
		/// </summary>
		public bool IsEnabled
		{
			get { return isEnabled; }
			set
			{
				isEnabled = value;
				OnPropertyChanged(nameof(IsEnabled));
			}
		}

		private int selectedIndex = 0;
		public int SelectedIndex
		{
			get { return selectedIndex; }
			set
			{
				selectedIndex = value;

				if (SelectedIndex == 0) CurrentOrderType = OrderType.Single;
				if (SelectedIndex == 1) CurrentOrderType = OrderType.Temp;
				if (SelectedIndex == 2) CurrentOrderType = OrderType.Virtue;

				OnPropertyChanged(nameof(SelectedIndex));
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

				
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		private Order selectedOrder;
		public Order SelectedOrder
		{
			get { return selectedOrder; }
			set
			{
				selectedOrder = value;
				OnPropertyChanged(nameof(SelectedOrder));


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}
		public OrderElement SelectedElement
		{
			get { return BidsModel?.SelectedElement; }
			set
			{
				BidsModel.SelectedElement = value;
				IsSelectedElementExist = IsSelectedElementExist;
				OnPropertyChanged();


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public bool IsSelectedElementExist
		{
			get { return SelectedElement != null; }
			private set { OnPropertyChanged(); }
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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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
				OnPropertyChanged(nameof(CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			}
		}

		public bool LoadedOriginalScan_CurrentSingleOrder => BidsModel?.CurrentSingleOrder?.ImageGuid != null && BidsModel?.CurrentSingleOrder?.ImageGuid != Guid.Empty;

		public bool LoadedOriginalScan_CurrentTemporaryOrder => BidsModel?.CurrentTemporaryOrder?.ImageGuid != null && BidsModel?.CurrentTemporaryOrder?.ImageGuid != Guid.Empty;

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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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
				OnPropertyChanged(nameof(CurrentTemporaryOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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
					OnPropertyChanged(nameof(CurrentOrder));
				}


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public bool IsNoneOrder
		{
			get { return CurrentOrderType == OrderType.None; }
			set
			{
				if (value)
				{
					EditButtonEnable = false;
					CurrentOrderType = OrderType.None;
				}
				else
				{
					EditButtonEnable = true;
				}

				OnPropertyChanged(nameof(IsNoneOrder));


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public bool IsTempOrder
		{
			get { return CurrentOrderType == OrderType.Temp; }
			set
			{
				if (value)
				{
					EditButtonEnable = true;
					CurrentOrderType = OrderType.Temp;
				}

				OnPropertyChanged(nameof(IsTempOrder));

				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public bool IsSingleOrder
		{
			get { return CurrentOrderType == OrderType.Single; }
			set
			{
				if (value)
				{
					EditButtonEnable = true;
					CurrentOrderType = OrderType.Single;
				}
				OnPropertyChanged(nameof(IsSingleOrder));

				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public bool IsVirtueOrder
		{
			get { return CurrentOrderType == OrderType.Virtue; }
			set
			{
				if (value)
				{
					EditButtonEnable = true;
					CurrentOrderType = OrderType.Virtue;
				}
				OnPropertyChanged(nameof(IsVirtueOrder));

				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		private OrderType currentOrderType = OrderType.None;
		/// <summary>
		/// Свойство для определения текущей открытой вкладки.
		/// </summary>
		private OrderType CurrentOrderType
		{
			get { return currentOrderType; }
			set
			{
				if (value != currentOrderType)
				{
					currentOrderType = value;
					if (BidsModel != null)
					{
						BidsModel.OrderType = currentOrderType;
					}

					OnPropertyChanged(nameof(IsSingleOrder));
					OnPropertyChanged(nameof(IsTempOrder));
					OnPropertyChanged(nameof(IsVirtueOrder));
					OnPropertyChanged(nameof(IsNoneOrder));

					OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
					OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
				}
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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public Visibility AddUpdVisib
		{
			get { return BidsModel.IsAddUpdVisib; }
			set
			{
				BidsModel.IsAddUpdVisib = value;
				OnPropertyChanged();


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		public ICommand DoubleClickCommand { get; set; }
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

		public ICommand ChooseVisitorForVirtueCommand { get; set; }
		public ICommand ChooseOrganizationForVirtueCommand { get; set; }
		public ICommand ChooseZonesForVirtueCommand { get; set; }

		public ICommand AddPersonCommand { get; set; } // добавление человека в заявку
		public ICommand UpdatePersonCommand { get; set; } // редактирование человека в заявке
		public ICommand DeletePersonCommand { get; set; } // удаление человека из заявки

		public ICommand SignerCommand { get; set; } // посписывающий заявку

		//public ICommand SignerTempCommand { get; set; } // посписывающий временную
		public ICommand AgreerCommand { get; set; } // согласующий

		public ICommand OpeningOriginalOrderCommand { get; set; } // Команда для открытия оригинала заявки
		public ICommand LoadingOriginalOrderCommand { get; set; } // Команда для открытия окна диалога для загрузки оригинала заявки
		public ICommand RemovingOriginalOrderScanCommand { get; set; } // Команда для удаления



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


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		private bool acceptButtonEnable = false;

		/// <summary>
		/// Доступность кнопок Применить и Отмена.
		/// </summary>
		public bool AcceptButtonEnable
		{
			get { return acceptButtonEnable; }
			set
			{
				acceptButtonEnable = value;
				OnPropertyChanged();


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		private bool navigateButtonEnable = false;

		/// <summary>
		/// Доступность кнопок нижнего ряда, кроме кнопок Применить, Отмена.
		/// </summary>
		public bool NavigateButtonEnable
		{
			get { return navigateButtonEnable; }
			set
			{
				navigateButtonEnable = value;
				OnPropertyChanged();


				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		private bool editButtonEnable = false;

		public bool EditButtonEnable
		{
			get
			{
				return editButtonEnable;
			}
			set
			{
				editButtonEnable = value;

				OnPropertyChanged();

				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
				OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
			}
		}

		/// <summary>
		/// Настройки положения и размера окна.
		/// </summary>
		public ChildWinSet WinSet { get; set; }

		#endregion

		public BidsViewModel()
		{
			// Задать размеры и положение формы.
			WinSet = new ChildWinSet() {Left = 0};
			WinSet.Left = WinSet.Width;

			// Инициализация команд.
			DoubleClickCommand = new RelayCommand( arg => DoubleClickElement(arg));
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

			ChooseVisitorForVirtueCommand = new RelayCommand(arg => ChooseVisitorForVirtue());
			ChooseOrganizationForVirtueCommand = new RelayCommand(arg => ChooseOrganizationForVirtue());
			ChooseZonesForVirtueCommand = new RelayCommand(arg => ChooseZonesForVirtue());

			AddPersonCommand = new RelayCommand(arg => AddPerson());
			UpdatePersonCommand = new RelayCommand(arg => UpdatePerson());
			DeletePersonCommand = new RelayCommand(arg => DeletePerson());

			SignerCommand = new RelayCommand(arg => Signer());
			AgreerCommand = new RelayCommand(arg => Agreer());

			OpeningOriginalOrderCommand = new RelayCommand(args => OpenOriginalOrderScan());
			LoadingOriginalOrderCommand = new RelayCommand(args => LoadOriginalOrderScan());
			RemovingOriginalOrderScanCommand = new RelayCommand(args => RemoveOriginalOrderScan());

			//			public ICommand LoadingRoiginalBidCommand { get; set; } // Команда для открытия окна диалога для загрузки оригинала заявки
			//public ICommand ClearCommand { get; set; } // Команда для удаления

			CurrentOrderType = OrderType.Single;

			TextEnable = false; // При открытии окна поля недоступны.
			AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
			NavigateButtonEnable = true;
			EditButtonEnable = true;
			IsEnabled = true;
		}

        public void SetToVirtue()
        {
            isToVirtue = true;
            New();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

        public void SetToOrder(Order order)
        {
            // TODO - здесь нужно установить текущим переданный Order
            //BidsModel.CurrentSingleOrder = order;
            //OnPropertyChanged(nameof(SelectedOrder));
        }

        private void BidsModel_OnRefresh()
		{
			CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
			CurrentTemporaryOrder.OrderElements = BidsModel.CurrentTemporaryOrder.OrderElements;

			CurrentSingleOrder = BidsModel.CurrentSingleOrder;
			CurrentSingleOrder.OrderElements = BidsModel.CurrentSingleOrder.OrderElements;

			CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
			CurrentVirtueOrder.OrderElements = BidsModel.CurrentVirtueOrder.OrderElements;


			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));

			//UpdateVisitor = BidsModel.UpdateVisitor;
		}

		private void Agreer()
		{
			BidsModel.Agreer();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void OpenOriginalOrderScan()
		{
			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Single:
					if (CurrentSingleOrder != null && CurrentSingleOrder.ImageGuid != null && CurrentSingleOrder.ImageGuid != Guid.Empty)
					{
						DocumentImageView documentImageView = new DocumentImageView();
						documentImageView.DocumentImageGuid = CurrentSingleOrder.ImageGuid;
						documentImageView.ShowDialog();
					}
					break;
				case OrderType.Temp:
					if (CurrentTemporaryOrder != null && CurrentTemporaryOrder.ImageGuid != null && CurrentTemporaryOrder.ImageGuid != Guid.Empty)
					{
						DocumentImageView documentImageView = new DocumentImageView();
						documentImageView.DocumentImageGuid = CurrentTemporaryOrder.ImageGuid;
						documentImageView.ShowDialog();
					}
					break;
				default: break;
			}



			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));

		}

		private void LoadOriginalOrderScan()
		{
			SupRealClient.Common.Data.ImageOpenFileDialog imageOpenFileDialog = new SupRealClient.Common.Data.ImageOpenFileDialog();
			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				string path = imageOpenFileDialog.FileName;

				switch (CurrentOrderType)
				{
					case OrderType.None:
						break;
					case OrderType.Single:
						CurrentSingleOrder.ImageGuid = ImagesHelper.LoadImage(path);
						break;
					case OrderType.Temp:
						CurrentTemporaryOrder.ImageGuid = ImagesHelper.LoadImage(path);
						break;
					default:break;
				}

				


				//DataRow imageRow = ImagesWrapper.
				//	CurrentTable().Table.NewRow();
				//imageRow["f_image_alias"] = alias;
				//imageRow["f_visitor_id"] = id;
				//imageRow["f_image_type"] = ImageType.Document;
				//imageRow["f_deleted"] = "N";
				//images.Add(new KeyValuePair<Guid, ImageType>(
				//	alias, ImageType.Document));


				//PhotoSource = ImagesHelper.GetImagePath(photoAlias);
				//OnModelPropertyChanged?.Invoke("PhotoSource");
			}



			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}


		private void RemoveOriginalOrderScan()
		{
			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Single:
					CurrentSingleOrder.ImageGuid = Guid.Empty;
					break;
				case OrderType.Temp:
					CurrentTemporaryOrder.ImageGuid = Guid.Empty;
					break;
				default: break;
			}

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Signer()
		{
			BidsModel.Signer();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
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

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		/// <summary>
		/// Обновляет информацию о человеке в заявке.
		/// </summary>
		private void UpdatePerson()
		{
			BidsModel.UpdatePerson();
			SelectedElement = BidsModel.SelectedElement;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		/// <summary>
		/// Удаляет информацию о человеке в заявке.
		/// </summary>
		private void DeletePerson()
		{
			if (SelectedElement == null)
			{
				return;
			}

			MessageBoxResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить этот элемент заявки?", "Внимание",
				MessageBoxButton.OKCancel, MessageBoxImage.Question);
			if (dialogResult == MessageBoxResult.OK)
			{
				BidsModel.DeletePerson();
				SelectedElement = BidsModel.SelectedElement;
			}

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));

		}

		private void DoubleClickElement(object arg)
		{
			if (arg is OrderElement)
			{
				OrderElement orderElement = (OrderElement)arg;
				VisitsModel model = new VisitsModel();

				model.CurrentItem = model.Find(orderElement.VisitorId);
				VisitorsView view = new VisitorsView();
				view.Width = WinSet.Width;
				view.Height = WinSet.Height;
				view.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
				view.WindowStartupLocation = WindowStartupLocation.CenterOwner;

				if (view.DataContext is Views.VisitsViewModel visitsView)
				{
					visitsView.DocumentScanerRemoveSubscription();
				}

				Views.VisitsViewModel vm = new Views.VisitsViewModel(view) { Model = model };
				view.DataContext = vm;
				view.ShowDialog();
			}

			if (arg is Order)
			{
				CurrentSelectedOrder = (Order)arg;

				SelectedIndex = CurrentSelectedOrder.TypeId - 1;

				IsSingleOrder = SelectedIndex == 0;
				IsTempOrder = SelectedIndex == 1;
				IsVirtueOrder = SelectedIndex == 2;
				
				ApplyCurrentSelectedOrder();
			}

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Begin()
		{
			BidsModel.Begin();
			ChangeCurrentSelectedOrder();
			
		}

		private void End()
		{
			BidsModel.End();

			ChangeCurrentSelectedOrder();
		}

		private void Next()
		{
			BidsModel.Next();

			ChangeCurrentSelectedOrder();
		}

		private void Prev()
		{
			BidsModel.Prev();

			ChangeCurrentSelectedOrder();
		}

		private void Search()
		{
			BidsModel.Search();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Delay()
		{
			BidsModel.Delay();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		/// <summary>
		/// Создание новой заявки
		/// </summary>
		private void New()
		{
			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Single:
					CurrentSelectedOrder = CurrentSingleOrder;
					break;
				case OrderType.Temp:
					CurrentSelectedOrder = CurrentTemporaryOrder;
					break;
				case OrderType.Virtue:
					CurrentSelectedOrder = CurrentVirtueOrder;
					break;
			}

			BidsModel = new NewBidsModel(CurrentOrderType);

			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.CurrentSingleOrder;
					break;
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.CurrentVirtueOrder;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			TextEnable = true; // При открытии окна поля недоступны.
			AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
			NavigateButtonEnable = false;
			EditButtonEnable = false;
			IsEnabled = false;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		/// <summary>
		/// Редактирование заявки.
		/// </summary>
		private void Edit()
		{
			BidsModel = new EditBidsModel(CurrentSingleOrder,
				CurrentTemporaryOrder, CurrentVirtueOrder, CurrentOrder);

			SelectedElement = BidsModel.SelectedElement;

			ChangeCurrentSelectedOrder();

			TextEnable = true; // При открытии окна поля недоступны.
			AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
			NavigateButtonEnable = false;
			EditButtonEnable = false;
			IsEnabled = false;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Ok()
		{
			Order currentOrder;
			switch (CurrentOrderType)
			{
				case OrderType.None:
					currentOrder = null;
					break;
				case OrderType.Single:
					currentOrder = BidsModel.CurrentSingleOrder;
					break;
				case OrderType.Temp:
					currentOrder = BidsModel.CurrentTemporaryOrder;
					break;
				case OrderType.Virtue:
					currentOrder = BidsModel.CurrentVirtueOrder;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (currentOrder != null)
			{
				if (!currentOrder.IsOrderDataCorrect(CurrentOrderType, out string errorMessage))
				{
					MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}
				BidsModel.Ok();

				CurrentSelectedOrder = currentOrder;

				BidsModel = new BidsModel();

				ApplyCurrentSelectedOrder();
			}
			TextEnable = false;
			AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
			NavigateButtonEnable = true;
			EditButtonEnable = true;
			IsEnabled = true;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ApplyCurrentSelectedOrder()
		{
			CheckOrderType();

			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
					CurrentTemporaryOrder = BidsModel.TemporaryOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder;
					break;
				case OrderType.Single:
					CurrentSingleOrder = BidsModel.SingleOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					CurrentSelectedOrder = BidsModel.CurrentSingleOrder;
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder = BidsModel.VirtueOrdersSet.FirstOrDefault(x => x.Id == CurrentSelectedOrder.Id);
					CurrentSelectedOrder = BidsModel.CurrentVirtueOrder;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void CheckOrderType()
		{
			if (CurrentSelectedOrder == null)
			{
				return;
			}
			if (CurrentSelectedOrder.TypeId - 1 != selectedIndex)
			{
				switch (CurrentOrderType)
				{
					case OrderType.None:
						break;
					case OrderType.Temp:
						CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder; ; // Запомнить временную заявку перед добавлением новой.
						break;
					case OrderType.Single:
						CurrentSelectedOrder = BidsModel.CurrentSingleOrder; ; // Запомнить разовую заявку перед добавлением новой.
						break;
					case OrderType.Virtue:
						CurrentSelectedOrder = BidsModel.CurrentVirtueOrder;// Запомнить заявку на основании перед добавлением новой.
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}



			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ChangeCurrentSelectedOrder()
		{
			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
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

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ChangeCurrentSelectedOrder1()
		{
			switch (CurrentOrderType)
			{
				case OrderType.None:
					break;
				case OrderType.Temp:
					CurrentSelectedOrder = BidsModel.CurrentTemporaryOrder; // Запомнить временную заявку перед добавлением новой.
					CurrentTemporaryOrder = CurrentSelectedOrder;
					break;
				case OrderType.Single:
					CurrentSelectedOrder = BidsModel.CurrentSingleOrder; // Запомнить разовую заявку перед добавлением новой.
					CurrentSingleOrder = CurrentSelectedOrder;
					break;
				case OrderType.Virtue:
					CurrentSelectedOrder = BidsModel.CurrentVirtueOrder; // Запомнить заявку на основании перед добавлением новой.
					CurrentVirtueOrder = CurrentSelectedOrder;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Cancel()
		{
			if (MessageBox.Show("Вы уверены, что хотите отменить изменения?", "Внимание",
				    MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
			{
				return;
			}

			BidsModel = new BidsModel();

			ApplyCurrentSelectedOrder();

			TextEnable = false; // При открытии окна поля недоступны.
			AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
			NavigateButtonEnable = true;
			EditButtonEnable = true;
			IsEnabled = true;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Further()
		{
			BidsModel.Further();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void Reload()
		{
			BidsModel = new BidsModel();

			BidsModel.Reload();

			if (CurrentSelectedOrder == null)
			{
				ChangeCurrentSelectedOrder1();
			}
			ApplyCurrentSelectedOrder();

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ChooseVisitorForVirtue()
		{
			var model = new VisitorsListModel<Visitor>();
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

			if (result == null)
			{
				return;
			}

			OrderElement currentOrderElement = CurrentVirtueOrder.FirstOrderElement;

			currentOrderElement.VisitorId = result.Id;
			currentOrderElement.Position = currentOrderElement.VisitorMainPosition;
			currentOrderElement.OrganizationId = result.OrganizationId;
			currentOrderElement.IsBlock = result.IsBlock;
			currentOrderElement.IsCardIssued = result.IsCardIssue;

			CurrentVirtueOrder = CurrentVirtueOrder;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ChooseOrganizationForVirtue()
		{
			var model = new OrganizationsListModel<Organization>();
			var viewModel = new Base4ViewModel<Organization>()
			{
				Model = model
			};
			var view = new Base4OrganizationsWindView()
			{
				DataContext = viewModel
			};
			view.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			// model.OnClose += view.Handling_OnClose;
			view.ShowDialog();
			BaseModelResult result = view.WindowResult as BaseModelResult;
			if (result == null)
			{
				return;
			}

			OrderElement currentOrderElement = CurrentVirtueOrder.FirstOrderElement;

			currentOrderElement.OrganizationId = result.Id;

			CurrentVirtueOrder = CurrentVirtueOrder;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}

		private void ChooseZonesForVirtue()
		{
			OrderElement currentOrderElement = CurrentVirtueOrder.FirstOrderElement;

			// todo: переделать нормально
			AddUpdateAbstrModel zonesModel = new AddUpdateZonesToBidModel(
				currentOrderElement);
			AddUpdateBaseViewModel viewModel = new AddUpdateZonesToBidViewModel
			{
				Model = zonesModel,
				Schedule = currentOrderElement.Schedule
			};
			AssigningZonesView wind = new AssigningZonesView
			{
				DataContext = viewModel
			};
			wind.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			wind.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			viewModel.Model.OnClose += wind.Handling_OnClose;
			wind.ShowDialog();
			if (wind.WindowResult as OrderElement == null)
			{
				return;
			}

			currentOrderElement.Templates = (wind.WindowResult as OrderElement).Templates;
			currentOrderElement.TemplateIdList =
				AndoverEntityListHelper.TemplatesSchedulesToString(currentOrderElement.Templates);
			currentOrderElement.Areas = (wind.WindowResult as OrderElement).Areas;
			currentOrderElement.AreaIdList =
				AndoverEntityListHelper.AreasSchedulesToString(currentOrderElement.Areas);
			currentOrderElement.ScheduleId = (wind.WindowResult as OrderElement).ScheduleId;

			CurrentVirtueOrder = CurrentVirtueOrder;

			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentSingleOrder));
			OnPropertyChanged(nameof(LoadedOriginalScan_CurrentTemporaryOrder));
		}
	}
}