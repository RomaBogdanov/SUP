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

        public ICommand UserExit
        { get; set; }

        public ICommand Close
        { get; set; }

        public MainWindowViewModel()
        {
            Current = this;
            ListOrganizationsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("OrganizationsWindView"));
            ListDocumentsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("DocumentsWindView"));
            ListNationsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("NationsWindView"));
            ListCardsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("CardsWindView"));
            ListZonesClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("ZonesWindView"));
            ListCabinetsClick = new RelayCommand(arg => ViewManager.Instance.OpenWindow("CabinetsWindView"));
            UserExit = new RelayCommand(arg => UserExitProc());
            setupStorage.ChangeUserExit += arg => IsUserEnter = !arg;
            Close = new RelayCommand(arg => ExitApp());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void UserExitProc()
        {
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            clientConnector.ExitAuthorize();
            setupStorage.UserExit = true;
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
