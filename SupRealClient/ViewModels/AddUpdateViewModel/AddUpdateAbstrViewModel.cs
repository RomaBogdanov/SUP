using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Annotations;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Views;

namespace SupRealClient.ViewModels.AddUpdateViewModel
{
    /// <summary>
    /// Класс-претендент на то, чтобы стать основой для всех ViewModel
    /// форм добавления-редактирования данных.
    /// </summary>
    public class AddUpdateBaseViewModel : INotifyPropertyChanged
    {
        protected AddUpdateAbstrModel model;
        protected string okCaption;
        protected string title;

        public IWindow Parent { get; set; }

        public AddUpdateAbstrModel Model
        {
            get { return model; }
            set
            {
                if (model != null)
                {
                    model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                model = value;
                model.Parent = Parent;
                model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
            }
        }

        public object CurrentItem
        {
            get { return Model.CurrentItem; }
            set
            {
                Model.CurrentItem = value;
                OnPropertyChanged();
            }
        }

        public string OkCaption
        {
            get { return okCaption; }
            set
            {
                okCaption = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateBaseViewModel()
        {
            Ok = new RelayCommand(arg => OkCommand());
            Cancel = new RelayCommand(arg => CancelCommand());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]
        string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OkCommand()
        {
            Model.Ok();
        }

        protected virtual void CancelCommand()
        {
            Model.Cancel();

        }
    }

    public class AddUpdateSpaceViewModel: AddUpdateBaseViewModel
    { }

	/// <summary>
	/// ViewModel для окна окна создания и редактирования элементов заявки
	/// </summary>
    public class AddUpdateBidsViewModel : AddUpdateBaseViewModel
    {
        public ICommand ChooseVisitor { get; set; }

		public ICommand ChooseOrganization { get; set; }

        public ICommand ChooseCatcher { get; set; }

        public ICommand UpdateZones { get; set; }

	    private OrderElement CurrentOrderElement => CurrentItem as OrderElement;

	    public AddUpdateBidsViewModel(AddUpdateAbstrModel model) : base()
	    {
		    Model = model;
			ChooseVisitor = new RelayCommand(arg => VisitorNameCommand());
		    ChooseOrganization = new RelayCommand(arg => OrganizationNameCommand());
		    ChooseCatcher = new RelayCommand(arg => CatcherNameCommand());
		    UpdateZones = new RelayCommand(arg => UpdateZonesCommand());
		}

		private void VisitorNameCommand()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
	        if (result == null)
	        {
		        return;
	        }
            CurrentOrderElement.VisitorId = result.Id;
	        CurrentOrderElement.Position = CurrentOrderElement.VisitorMainPosition;
			if (CurrentOrderElement.OrganizationId == 0)
	        {
		        CurrentOrderElement.OrganizationId = result.OrganizationId;
			}

			CurrentItem = CurrentItem;
        }

	    private void OrganizationNameCommand()
	    {
		    BaseModelResult result = ViewManager.Instance.OpenWindowModal(
				"Base4OrganizationsWindView", null) as BaseModelResult;
		    if (result == null)
		    {
			    return;
		    }
		    CurrentOrderElement.OrganizationId = result.Id;
		    CurrentItem = CurrentItem;
		}

        private void CatcherNameCommand()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
	        if (result == null)
	        {
				return;
	        }
            CurrentOrderElement.CatcherId = result.Id;
            CurrentOrderElement.Catcher = result.Name;
            CurrentItem = CurrentItem;
        }

        private void UpdateZonesCommand()
        {
            // todo: переделать нормально
            AddUpdateAbstrModel zonesModel = new AddUpdateZonesToBidModel(
                CurrentOrderElement.Areas);
            AddUpdateBaseViewModel viewModel = new AddUpdateZonesToBidViewModel
            {
                Model = zonesModel
            };
            AssigningZonesView wind = new AssigningZonesView
            {
                DataContext = viewModel
            };
            viewModel.Model.OnClose += wind.Handling_OnClose;
            wind.ShowDialog();
            if (wind.WindowResult as ObservableCollection<Area> == null)
            {
                return;
            }
            CurrentOrderElement.Areas = wind.WindowResult as 
                ObservableCollection<Area>;
            string st = "";
            foreach (var area in wind.WindowResult as ObservableCollection<Area>)
            {
                st += area.Name + ", ";
            }

            CurrentOrderElement.Passes = st.Remove(st.Length - 2);
        }

	    protected override void OkCommand()
	    {
		    if (string.IsNullOrEmpty(CurrentOrderElement.Visitor) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Position) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Catcher) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Organization))
		    {
			    MessageBox.Show("Не все поля заполнены", "Ошибка");
				return;
		    }

			base.OkCommand();
	    }
	}

    public class AddUpdateZonesToBidViewModel : AddUpdateBaseViewModel
    {
        private ObservableCollection<Area> setAllZones;
        private ObservableCollection<Area> setAppointZones;

        public ObservableCollection<Area> SetAllZones
        {
            get { return ((AddUpdateZonesToBidModel)this.Model).SetAllZones; }
            set
            {
                ((AddUpdateZonesToBidModel)this.Model).SetAllZones = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Area> SetAppointZones
        {
            get { return ((AddUpdateZonesToBidModel)this.Model).SetAppointZones; }
            set
            {
                ((AddUpdateZonesToBidModel)this.Model).SetAppointZones = value;
                OnPropertyChanged();
            }
        }

        public Area CurrentAppointZone
        {
            get { return ((AddUpdateZonesToBidModel)this.Model).CurrentAppointZone; }
            set
            {
                ((AddUpdateZonesToBidModel) this.Model).CurrentAppointZone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Отработка кнопки перевода в список назначенных зон
        /// </summary>
        public ICommand ToAppointZones { get; set; }

        /// <summary>
        /// Отработка кнопки перевода в список всех зон
        /// </summary>
        public ICommand ToAllZones { get; set; }

        public AddUpdateZonesToBidViewModel() : base()
        {
            ToAppointZones = new RelayCommand(arg => ToAppointZonesCommand());
            ToAllZones = new RelayCommand(arg => ToAllZonesCommand());
        }

        private void ToAppointZonesCommand()
        {
            CurrentItem = ((AddUpdateZonesToBidModel) this.Model).ToAppointZones();
        }

        private void ToAllZonesCommand()
        {
            CurrentAppointZone = ((AddUpdateZonesToBidModel) this.Model).ToAllZonesCommand();
        }
    }
}
