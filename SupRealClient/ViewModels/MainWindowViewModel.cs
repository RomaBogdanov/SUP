using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SupClientConnectionLib;
using SupRealClient.Views;
using SupRealClient.TabsSingleton;

namespace SupRealClient.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ContentControl control = new Authorize1View();
        string authorizedUser;
        bool userExitOpened = false;
        SetupStorage setupStorage = SetupStorage.Current;
        Visibility loginVisibility = Visibility.Visible;
        Visibility dataVisibility = Visibility.Hidden;

        public static MainWindowViewModel Current
        {
            get;
            private set;
        }

        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            set
            {
                this.loginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            }
        }

        public Visibility DataVisibility
        {
            get { return dataVisibility; }
            set
            {
                this.dataVisibility = value;
                OnPropertyChanged("DataVisibility");
            }
        }

        public ContentControl Control
        {
            get { return control; }
            set
            {
                if (this.control != value)
                {
                    this.control = value;
                    OnPropertyChanged("Control");
                }
            }
        }

        public string AuthorizedUser
        {
            get { return authorizedUser; }
            set
            {
                if (this.authorizedUser != value)
                {
                    this.authorizedUser = value;
                    OnPropertyChanged("AuthorizedUser");
                }
            }
        }

        public bool IsUserEnter
        {
            get { return userExitOpened; }
            set
            {
                this.userExitOpened = value;
                OnPropertyChanged("IsUserEnter");
            }
        }

        public ICommand ListOrganizationsClick
        { get; set; }

        public ICommand ListDocumentsClick
        { get; set; }

        public ICommand ListNationsClick
        { get; set; }

        public ICommand ListCardsClick
        { get; set; }

        public ICommand ListCabinetsClick
        { get; set; }

        public ICommand ListZonesClick
        { get; set; }

        public ICommand LogsClick
        { get; set; }

        public ICommand ListBaseOrgsStructClick
        { get; set; }

        public ICommand ListChildOrgs
        { get; set; }

        public ICommand ListBaseOrgs
        { get; set; }

        public ICommand UserExit
        { get; set; }

        public ICommand Close
        { get; set; }

        public ICommand OpenVisitorsCommand
        { get; set; }

        public ICommand OpenBidsCommand { get; set; }

        public ICommand OpenVisitsCommand { get; set; }

        public MainWindowViewModel()
        {
            Current = this;
            ListOrganizationsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("OrganizationsWindView"));
            ListDocumentsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("DocumentsWindView"));
            ListNationsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("NationsWindView"));
            ListCardsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("CardsWindView"));
            ListZonesClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("ZonesWindView"));
            ListCabinetsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("CabinetsWindView"));
            LogsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("LogsWindView"));
            ListBaseOrgsStructClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("MainOrganisationStructureView"));
            ListChildOrgs = new RelayCommand(arg => ViewManager.Instance.OpenWindow("ChildOrgsView"));
            ListBaseOrgs = new RelayCommand(arg => ViewManager.Instance.OpenWindow("BaseOrgsView"));
            UserExit = new RelayCommand(arg => UserExitProc());
            setupStorage.ChangeUserExit += arg => IsUserEnter = !arg;
            Close = new RelayCommand(arg => ExitApp());

            OpenVisitorsCommand = new RelayCommand(obj => OpenVisitors());
            OpenBidsCommand = new RelayCommand(obj => OpenBids());
            OpenVisitsCommand = new RelayCommand(obj => OpenVisits());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OpenVisitors()
        {
            var visitorsViewModel = new VisitorsViewModel();

            var window = new VisitorsView {DataContext = visitorsViewModel};
            window.Show();
            
            var dc = (VisitorsViewModel)window.DataContext;
            
            // TODO тут лежит путь к файлику с изображением, которое выбрали во вкладке 'Фото'
            if (dc != null)
            {
                if (dc.PhotoSource != null)
                {
                    dc.PhotoSource.ToString();
                }
            }

            dc.ToString();
        }

        private void OpenBids()
        {
            var bidsViewModel = new BidsViewModel();

            var window = new BidsView {DataContext = bidsViewModel};
            window.ShowDialog();
            
            var dc = (BidsViewModel) window.DataContext;
            dc.ToString();
        }

        public void OpenVisits()
        {
            var visitsViewModel = new VisitsViewModel();

            var window = new VisitsView {DataContext = visitsViewModel};
            window.ShowDialog();

            var dc = (VisitsViewModel) window.DataContext;
            dc.ToString();
        }

        private void UserExitProc()
        {
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            clientConnector.ExitAuthorize();
            setupStorage.UserExit = true;
            ViewManager.Instance.ExitApp();
            // TODO - Отвязать ссылку на View из ViewModel
            Control = new Authorize1View();
            LoginVisibility = Visibility.Visible;
            DataVisibility = Visibility.Hidden;
        }
        
        private void ExitApp()
        {
            //TableWrapper.DisposeAll();
            ViewManager.Instance.ExitApp();
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            if (clientConnector.CheckAuthorize())
            {
                clientConnector.ExitAuthorize();
            }
        }
    }
}
