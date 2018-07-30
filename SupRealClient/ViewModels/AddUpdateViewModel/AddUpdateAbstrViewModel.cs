using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupRealClient.Annotations;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Views;
using SupRealClient.Models;
using SupRealClient.Views.Visitor;
using SupRealClient.TabsSingleton;
using System.Data;

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
        //protected object currentItem;
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

    public class AddUpdateBidsViewModel : AddUpdateBaseViewModel
    {
        public ICommand VisitorName { get; set; }

        public ICommand CatcherName { get; set; }

        public ICommand UpdateZones { get; set; }

        public AddUpdateBidsViewModel() : base()
        {
            VisitorName = new RelayCommand(arg => VisitorNameCommand());
            CatcherName = new RelayCommand(arg => CatcherNameCommand());
            UpdateZones = new RelayCommand(arg => UpdateZonesCommand());
        }

        private void VisitorNameCommand()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            (CurrentItem as OrderElement).VisitorId = result.Id;
            (CurrentItem as OrderElement).Visitor = result.Name;
            (CurrentItem as OrderElement).OrganizationId = result.OrganizationId;
            (CurrentItem as OrderElement).Organization = result.Organization;
            CurrentItem = CurrentItem;
        }

        private void CatcherNameCommand()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            (CurrentItem as OrderElement).CatcherId = result.Id;
            (CurrentItem as OrderElement).Catcher = result.Name;
            CurrentItem = CurrentItem;
        }

        private void UpdateZonesCommand()
        {
            // todo: переделать нормально
            AddUpdateAbstrModel zonesModel = new AddUpdateZonesToBidModel(
                (CurrentItem as OrderElement).Areas);
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
            (CurrentItem as OrderElement).Areas = wind.WindowResult as 
                ObservableCollection<Area>;
            string st = "";
            foreach (var area in wind.WindowResult as ObservableCollection<Area>)
            {
                st += area.Name + ", ";
            }

            (CurrentItem as OrderElement).Passes = st.Remove(st.Length - 2);
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
}
