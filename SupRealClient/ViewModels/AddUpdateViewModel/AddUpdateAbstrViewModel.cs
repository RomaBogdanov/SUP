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
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Common;

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

		public ICommand ClearVisitor { get; set; }
		public ICommand ClearOrganization { get; set; }
		public ICommand ClearPosition { get; set; }
		public ICommand ClearCatcher { get; set; }
		public ICommand ClearZones { get; set; }

	    private OrderElement CurrentOrderElement => CurrentItem as OrderElement;

	    public AddUpdateBidsViewModel(AddUpdateAbstrModel model) : base()
	    {
		    Model = model;
			ChooseVisitor = new RelayCommand(arg => VisitorNameCommand());
		    ChooseOrganization = new RelayCommand(arg => OrganizationNameCommand());
		    ChooseCatcher = new RelayCommand(arg => CatcherNameCommand());
		    UpdateZones = new RelayCommand(arg => UpdateZonesCommand());

		    ClearVisitor = new RelayCommand(arg => ClearVisitorCommand());
		    ClearOrganization = new RelayCommand(arg => ClearOrganiztionCommand());
		    ClearPosition= new RelayCommand(arg => ClearPositionCommand());
			ClearCatcher = new RelayCommand(arg => ClearCatcherCommand());
		    ClearZones = new RelayCommand(arg => ClearZonesCommand());
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
		    CurrentOrderElement.OrganizationId = result.OrganizationId;

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
            CurrentItem = CurrentItem;
        }

        private void UpdateZonesCommand()
        {
            // todo: переделать нормально
            AddUpdateAbstrModel zonesModel = new AddUpdateZonesToBidModel(
                CurrentOrderElement);
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
            if (wind.WindowResult as OrderElement == null)
            {
                return;
            }
            CurrentOrderElement.Areas = (wind.WindowResult as OrderElement).Areas;
            CurrentOrderElement.AreaIdList =
                AndoverEntityListHelper.AndoverEntitiesToString(CurrentOrderElement.Areas);
            string st = "";
            foreach (var area in (wind.WindowResult as OrderElement).Areas)
            {
                st += area.Name + ", ";
            }

	        if (st.Length - 2 >= 0)
	        {
		        CurrentOrderElement.Passes = st.Remove(st.Length - 2);
			}
        }

	    private void ClearVisitorCommand()
	    {
		    CurrentOrderElement.VisitorId = 0;
		    CurrentItem = CurrentItem;
	    }

	    private void ClearOrganiztionCommand()
	    {
		    CurrentOrderElement.OrganizationId = 0;
		    CurrentItem = CurrentItem;
		}

	    private void ClearPositionCommand()
	    {
		    CurrentOrderElement.Position = "";
		    CurrentItem = CurrentItem;
		}

	    private void ClearCatcherCommand()
	    {
		    CurrentOrderElement.CatcherId = 0;
		    CurrentItem = CurrentItem;
		}

	    private void ClearZonesCommand()
	    {
		    CurrentItem = CurrentItem;
		}

	    protected override void OkCommand()
	    {
		    if (string.IsNullOrEmpty(CurrentOrderElement.Visitor) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Position) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Catcher) ||
		        string.IsNullOrEmpty(CurrentOrderElement.Organization))
		    {
			    MessageBox.Show("Не все поля заполнены.", "Ошибка");
				return;
		    }

			if (!CommonHelper.IsPositionCorrect(CurrentOrderElement.Position))
			{
				MessageBox.Show("Некорретно введена должность.", "Ошибка");
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

        public Area CurrentAllZone
        {
            get { return ((AddUpdateZonesToBidModel)this.Model).CurrentAllZone; }
            set
            {
                ((AddUpdateZonesToBidModel)this.Model).CurrentAllZone = value;
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
            CurrentAllZone = ((AddUpdateZonesToBidModel) this.Model).ToAppointZones();
        }

        private void ToAllZonesCommand()
        {
            CurrentAppointZone = ((AddUpdateZonesToBidModel) this.Model).ToAllZonesCommand();
        }
    }

    public class AddUpdateDoorViewModel : AddUpdateBaseViewModel
    {
        public ICommand SpaceInCommand { get; set; }
        public ICommand SpaceOutCommand { get; set; }
        public ICommand AccessPointCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public AddUpdateDoorViewModel() : base()
        {
            SpaceInCommand = new RelayCommand(arg => SpaceList("in"));
            SpaceOutCommand = new RelayCommand(arg => SpaceList("out"));
            AccessPointCommand = new RelayCommand(arg => AccessPointList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        public string SpaceIn
        {
            get { return ((Door)Model.CurrentItem).SpaceIn; }
            set
            {
                ((Door)Model.CurrentItem).SpaceIn = value;
                OnPropertyChanged("SpaceIn");
            }
        }

        public string SpaceOut
        {
            get { return ((Door)Model.CurrentItem).SpaceOut; }
            set
            {
                ((Door)Model.CurrentItem).SpaceOut = value;
                OnPropertyChanged("SpaceOut");
            }
        }

        public string AccessPoint
        {
            get { return ((Door)Model.CurrentItem).AccessPoint; }
            set
            {
                ((Door)Model.CurrentItem).AccessPoint = value;
                OnPropertyChanged("AccessPoint");
            }
        }

        private void SpaceList(string direction)
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4SpacesWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            if (direction == "in")
            {
                ((Door)Model.CurrentItem).SpaceInId = result.Id <= 0 ? 0 : result.Id;
                SpaceIn = result.Name;
            }
            else
            {
                ((Door)Model.CurrentItem).SpaceOutId = result.Id <= 0 ? 0 : result.Id;
                SpaceOut = result.Name;
            }
        }

        private void AccessPointList()
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4AccessPointsWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            DataRow row = AccessPointsWrapper.CurrentTable().Table.Rows.Find(result.Id);
            if (row == null)
            {
                return;
            }

            ((Door)Model.CurrentItem).AccessPointIdHi = row.Field<int>("f_object_id_hi");
            ((Door)Model.CurrentItem).AccessPointIdLo = row.Field<int>("f_object_id_lo");
            AccessPoint = result.Name;
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "SpaceIn":
                    ((Door)Model.CurrentItem).SpaceInId = 0;
                    SpaceIn = "";
                    break;
                case "SpaceOut":
                    ((Door)Model.CurrentItem).SpaceOutId = 0;
                    SpaceOut = "";
                    break;
                case "AccessPoint":
                    ((Door)Model.CurrentItem).AccessPointIdHi = 0;
                    ((Door)Model.CurrentItem).AccessPointIdLo = 0;
                    AccessPoint = "";
                    break;
                default:
                    return;
            }
        }
    }

    public class AddUpdateAccessLevelViewModel : AddUpdateBaseViewModel
    {
        public ICommand AreaCommand { get; set; }
        public ICommand ScheduleCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public AddUpdateAccessLevelViewModel() : base()
        {
            AreaCommand = new RelayCommand(arg => AreaList());
            ScheduleCommand = new RelayCommand(arg => ScheduleList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        public string Area
        {
            get { return ((AccessLevel)Model.CurrentItem).Area; }
            set
            {
                ((AccessLevel)Model.CurrentItem).Area = value;
                OnPropertyChanged("Area");
            }
        }

        public string Schedule
        {
            get { return ((AccessLevel)Model.CurrentItem).Schedule; }
            set
            {
                ((AccessLevel)Model.CurrentItem).Schedule = value;
                OnPropertyChanged("Schedule");
            }
        }

        private void AreaList()
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4AreasWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            DataRow row = AreasWrapper.CurrentTable().Table.Rows.Find(result.Id);
            if (row == null)
            {
                return;
            }

            ((AccessLevel)Model.CurrentItem).AreaIdHi = row.Field<int>("f_object_id_hi");
            ((AccessLevel)Model.CurrentItem).AreaIdLo = row.Field<int>("f_object_id_lo");
            Area = result.Name;
        }

        private void ScheduleList()
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4SchedulesWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            DataRow row = SchedulesWrapper.CurrentTable().Table.Rows.Find(result.Id);
            if (row == null)
            {
                return;
            }

            ((AccessLevel)Model.CurrentItem).ScheduleIdHi = row.Field<int>("f_object_id_hi");
            ((AccessLevel)Model.CurrentItem).ScheduleIdLo = row.Field<int>("f_object_id_lo");
            Schedule = result.Name;
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Area":
                    ((AccessLevel)Model.CurrentItem).AreaIdHi = 0;
                    ((AccessLevel)Model.CurrentItem).AreaIdLo = 0;
                    Area = "";
                    break;
                case "Schedule":
                    ((AccessLevel)Model.CurrentItem).ScheduleIdHi = 0;
                    ((AccessLevel)Model.CurrentItem).ScheduleIdLo = 0;
                    Schedule = "";
                    break;
                default:
                    return;
            }
        }
    }

    public class AddUpdateAreaSpaceViewModel : AddUpdateBaseViewModel
    {
        public ICommand AreaCommand { get; set; }
        public ICommand SpaceCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public AddUpdateAreaSpaceViewModel() : base()
        {
            AreaCommand = new RelayCommand(arg => AreaList());
            SpaceCommand = new RelayCommand(arg => SpaceList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        public string Area
        {
            get { return ((AreaSpace)Model.CurrentItem).Area; }
            set
            {
                ((AreaSpace)Model.CurrentItem).Area = value;
                OnPropertyChanged();
            }
        }

        public string Space
        {
            get { return ((AreaSpace)Model.CurrentItem).Space; }
            set
            {
                ((AreaSpace)Model.CurrentItem).Space = value;
                OnPropertyChanged();
            }
        }

        private void AreaList()
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4AreasWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            DataRow row = AreasWrapper.CurrentTable().Table.Rows.Find(result.Id);
            if (row == null)
            {
                return;
            }

            ((AreaSpace)Model.CurrentItem).AreaIdHi = row.Field<int>("f_object_id_hi");
            ((AreaSpace)Model.CurrentItem).AreaIdLo = row.Field<int>("f_object_id_lo");
            Area = result.Name;
        }

        private void SpaceList()
        {
            var result = ViewManager.Instance.OpenWindowModal("Base4SpacesWindView") as BaseModelResult;

            if (result == null)
            {
                return;
            }

            ((AreaSpace)Model.CurrentItem).SpaceId = result.Id <= 0 ? 0 : result.Id;
            Space = result.Name;
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Area":
                    ((AreaSpace)Model.CurrentItem).AreaIdHi = 0;
                    ((AreaSpace)Model.CurrentItem).AreaIdLo = 0;
                    Area = "";
                    break;
                case "Space":
                    ((AreaSpace)Model.CurrentItem).SpaceId = 0;
                    Space = "";
                    break;
                default:
                    return;
            }
        }
    }

    public class AddUpdateTemplateViewModel : AddUpdateBaseViewModel
    {
        private Area currentAllArea;
        private Area currentAppointArea;

        public ObservableCollection<Area> SetAllAreas
        {
            get { return ((AddUpdateTemplateModel)this.Model).SetAllAreas; }
            set
            {
                ((AddUpdateTemplateModel)this.Model).SetAllAreas = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Area> SetAppointAreas
        {
            get { return ((AddUpdateTemplateModel)this.Model).SetAppointAreas; }
            set
            {
                ((AddUpdateTemplateModel)this.Model).SetAppointAreas = value;
                OnPropertyChanged();
            }
        }

        public Area CurrentAllArea
        {
            get { return currentAllArea; }
            set
            {
                currentAllArea = value;
                OnPropertyChanged();
            }
        }

        public Area CurrentAppointArea
        {
            get { return currentAppointArea; }
            set
            {
                currentAppointArea = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToAppointAreasCommand { get; set; }
        public ICommand ToAllAreasCommand { get; set; }

        public AddUpdateTemplateViewModel() : base()
        {
            ToAppointAreasCommand = new RelayCommand(arg => ToAppointAreas());
            ToAllAreasCommand = new RelayCommand(arg => ToAllAreas());
        }

        private void ToAppointAreas()
        {
            if (CurrentAllArea == null)
            {
                return;
            }
            int i = SetAllAreas.IndexOf(CurrentAllArea);
            SetAppointAreas.Add(CurrentAllArea);
            SetAllAreas.Remove(CurrentAllArea);
            if (SetAllAreas.Count > i)
            {
                CurrentAllArea = SetAllAreas[i];
            }
            else if (SetAllAreas.Count == 0)
            {
                CurrentAllArea = null;
            }
            else
            {
                CurrentAllArea = SetAllAreas[i - 1];
            }
        }

        private void ToAllAreas()
        {
            if (CurrentAppointArea == null)
            {
                return;
            }
            int i = SetAppointAreas.IndexOf(CurrentAppointArea);
            SetAllAreas.Add(CurrentAppointArea);
            SetAppointAreas.Remove(CurrentAppointArea);
            if (SetAppointAreas.Count > i)
            {
                CurrentAppointArea = SetAppointAreas[i];
            }
            else if (SetAppointAreas.Count == 0)
            {
                CurrentAppointArea = null;
            }
            else
            {
                CurrentAppointArea = SetAppointAreas[i - 1];
            }
        }
    }
}
