using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SupClientConnectionLib;
using SupRealClient.Views;
using SupRealClient.EnumerationClasses;
using System.Reflection;
using System;
using System.Windows.Threading;

namespace SupRealClient.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ContentControl control = ViewManager.AuthorizeView;
        string authorizedUser;
        private string _authorizedUserOnline; // Время оператора в системе.
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

        /// <summary>
        /// Логин оператора.
        /// </summary>
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

        /// <summary>
        /// Время оператора в сети.
        /// </summary>
        public string AuthorizedUserOnline
        {
            get { return _authorizedUserOnline; }
            set
            {
                _authorizedUserOnline = value;
                OnPropertyChanged("AuthorizedUserOnline");
            }
        }

        public DispatcherTimer DispatcherTimer = new DispatcherTimer();
        private int _timeOnline = 0; // Время онлайн в минутах.

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!DispatcherTimer.IsEnabled) { _timeOnline = 0; }
            _timeOnline++; // Прибавляем минуту.
            AuthorizedUserOnline = SetUserTime(_timeOnline);
        }

        /// <summary>
        /// Остановка таймера в случае выхода из системы.
        /// </summary>
        private void StopTimer()
        {
            DispatcherTimer.Stop();
            _timeOnline = 0;
            AuthorizedUserOnline = ""; // Обнуляем время онлайн.
            AuthorizedUser = ""; // Обнуляем оператора.
        }

        /// <summary>
        /// Переводит время в определенный формат.
        /// </summary>
        private string SetUserTime(int timeOnline)
        {
            string result = String.Empty;
            int hours = 0, minutes = 0;
            hours = timeOnline / 60;
            minutes = timeOnline%60;

            result = (hours > 0) ? String.Format("{0} ч. {1} мин.", hours, minutes) :
                String.Format("{0} мин.", minutes);
            return result;
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

        private System.Version _appVersion = Assembly.GetExecutingAssembly().GetName().Version;
        /// <summary>
        /// Версия сборки.
        /// </summary>
        public System.Version AppVersion
        {
            get { return _appVersion; }
        }

        public ICommand ListOrganizationsClick { get; set; }
        public ICommand ListDocumentsClick { get; set; }
        public ICommand ListNationsClick { get; set; }
        public ICommand ListCardsClick { get; set; }        
        public ICommand ListRegionsClick { get; set; }
        public ICommand LogsClick { get; set; }
        public ICommand ListBaseOrgsStructClick { get; set; }
        public ICommand ListChildOrgs { get; set; }
        public ICommand ListBaseOrgs { get; set; }
        public ICommand ListVisitorsClick { get; set; }

        public ICommand ListSpacesClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4SpacesWindView"));
        public ICommand ListDoorsClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4DoorsWindView"));
        public ICommand ListAreasClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4AreasWindView"));
        public ICommand ListAreasSpacesClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4AreasSpacesWindView"));
        public ICommand ListAccessPointsClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4AccessPointsWindView"));
        public ICommand ListKeysClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4KeysWindView"));
        public ICommand ListKeyHoldersClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4KeyHoldersWindView"));
        public ICommand ListKeyCasesClick { get; set; } = new RelayCommand(arg =>
                 ViewManager.Instance.OpenWindow("Base4KeyCasesWindView"));
        public ICommand ListSchedulesClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4SchedulesWindView"));
        public ICommand ListAccessLevelsClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4AccessLevelsWindView"));
        public ICommand ListCarsClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4CarsWindView"));
        public ICommand ListEquipmentsClick { get; set; } = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4EquipmentsWindView"));

        public ICommand OpenAboutWindow { get; set; }

        public ICommand UserExit { get; set; }
        public ICommand Close { get; set; }
        public ICommand OpenVisitorsCommand { get; set; }
        public ICommand OpenBidsCommand { get; set; }
        public ICommand OpenVisitsCommand { get; set; }

        /// <summary>
        /// Тестовая кнопка
        /// </summary>
        public ICommand TestCommand { get; set; }

        public MainWindowViewModel()
        {
            Current = this;
            ListOrganizationsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("Base4OrganizationsLargeWindView"));
            //ViewManager.Instance.OpenWindow("OrganizationsWindView"));
            ListDocumentsClick = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4DocumentsWindView"));
            //ViewManager.Instance.OpenWindow("DocumentsWindView"));
            ListNationsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("Base4NationsWindView"));
            //ViewManager.Instance.OpenWindow("NationsWindView"));
            ListCardsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("Base4CardsWindView"));
            //ViewManager.Instance.OpenWindow("CardsWindView"));            
            ListRegionsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("Base4RegionsWindView"));
            LogsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("LogsWindView"));
            ListBaseOrgsStructClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("MainOrganisationStructureView"));
            ListChildOrgs = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("Base4ChildOrgsWindView"));
            //ViewManager.Instance.OpenWindow("ChildOrgsView"));
            //ListBaseOrgs = new RelayCommand(arg => 
            //    ViewManager.Instance.OpenWindow("BaseOrgsView"));
            ListBaseOrgs = new RelayCommand(arg =>
                ViewManager.Instance.OpenWindow("Base4BaseOrgsWindView"));
            ListVisitorsClick = new RelayCommand(arg => 
                ViewManager.Instance.OpenWindow("VisitorsListWindView"));
            UserExit = new RelayCommand(arg => UserExitProc());
            setupStorage.ChangeUserExit += arg => IsUserEnter = !arg;
            Close = new RelayCommand(arg => ExitApp());

            OpenVisitorsCommand = new RelayCommand(obj => OpenVisitors());
            OpenBidsCommand = new RelayCommand(obj => OpenBids());
            OpenVisitsCommand = new RelayCommand(obj => OpenVisits());
            TestCommand = new RelayCommand(obj => TestMethod());

            OpenAboutWindow = new RelayCommand(obj=>OpenAbout());

            // Таймер для время онлайн.
            DispatcherTimer.Tick += dispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            AuthorizedUserOnline = SetUserTime(0);
        }

        /// <summary>
        /// Открыть окно "О программе".
        /// </summary>
        private void OpenAbout()
        {
            Views.AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OpenVisitors()
        {
            if (IsUserEnter)
            {
                ViewManager.Instance.OpenWindow("VisitorsView");
            }
        }

        private void OpenBids()
        {
            if (IsUserEnter)
            {
                var bidsViewModel = new BidsViewModel
                {
                    BidsModel = new BidsModel()
                };

                var window = new BidsView {DataContext = bidsViewModel};
                window.Show();

                var dc = (BidsViewModel) window.DataContext;
                dc.ToString();
            }
        }

        public void OpenVisits()
        {
            if (IsUserEnter)
            {
                var visitsViewModel = new VisitsViewModel();

                var window = new VisitsView {DataContext = visitsViewModel};
                window.ShowDialog();

                var dc = (VisitsViewModel) window.DataContext;
                dc.ToString();
            }
        }

        private void UserExitProc()
        {
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            clientConnector.ExitAuthorize();
            setupStorage.UserExit = true;
            ViewManager.Instance.ExitApp();
            // TODO - Отвязать ссылку на View из ViewModel
            ViewManager.AuthorizeView.Reset();
            Control = ViewManager.AuthorizeView;
            LoginVisibility = Visibility.Visible;
            DataVisibility = Visibility.Hidden;

            // Работа с таймером.
            StopTimer();
        }

        private void TestMethod()
        {
            IBaseListModel<Organization> model = 
                new OrgsSample<Organization>();

            BaseListViewModel<Organization> viewModel = 
                new BaseListViewModel<Organization>();
            viewModel.Model = model;
            var window = new TestView { DataContext = viewModel };

            window.ShowDialog();
        }

        private void ExitApp()
        {
            //TableWrapper.DisposeAll();
            InputProvider.GetInputProvider().Dispose();
            ViewManager.Instance.ExitApp();
            try
            {
                ClientConnector clientConnector = ClientConnector.CurrentConnector;
                clientConnector.ExitAuthorize();
            }
            catch
            {
            }
        }
    }
}
