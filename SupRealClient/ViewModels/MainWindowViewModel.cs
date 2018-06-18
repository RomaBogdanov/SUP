﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SupClientConnectionLib;
using SupRealClient.Views;
using SupRealClient.EnumerationClasses;
using System.Diagnostics;

namespace SupRealClient.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ContentControl control = ViewManager.AuthorizeView;
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
        }

        private void TestMethod()
        {
            /*IBaseListModel<Organization> model = 
                new OrgsSample<Organization>();

            BaseListViewModel<Organization> viewModel = 
                new BaseListViewModel<Organization>();
            viewModel.Model = model;
            var window = new TestView { DataContext = viewModel };

            window.ShowDialog();*/
            Process.Start("AndoverPersonsManager.exe");
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
