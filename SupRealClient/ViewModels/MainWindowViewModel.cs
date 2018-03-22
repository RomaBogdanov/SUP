using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ContentControl control = new Authorize1View();
        string authorizedUser;
        bool userExitOpened = false;
        SetupStorage setupStorage = SetupStorage.Current;
        Window windowDocuments;
        Window windowNations;

        public static MainWindowViewModel Current
        {
            get;
            private set;
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

        public ICommand UserExit
        { get; set; }

        public ICommand Close
        { get; set; }

        public MainWindowViewModel()
        {
            Current = this;
            ListOrganizationsClick = new RelayCommand(arg => ListOrganizationsOpen());
            ListDocumentsClick = new RelayCommand(arg => OpenDocumentsWindow());
            ListNationsClick = new RelayCommand(arg => OpenNationsWindow());
            UserExit = new RelayCommand(arg => UserExitProc());
            setupStorage.ChangeUserExit += arg => IsUserEnter = !arg;
            Close = new RelayCommand(arg => ExitApp());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void ListOrganizationsOpen()
        {
            Control = new Organizations1View();
        }


        private void UserExitProc()
        {
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            clientConnector.ExitAuthorize();
            setupStorage.UserExit = true;
            Control = new Authorize1View();
        }

        private void OpenDocumentsWindow()
        {
            windowDocuments = windowDocuments ?? new DocumentsWindView();
            windowDocuments.Show();
            windowDocuments.Activate();
        }

        private void OpenNationsWindow()
        {
            windowNations = windowNations ?? new NationsWindView();
            windowNations.Show();
            windowNations.Activate();
        }

        private void ExitApp()
        {
            if (this.windowDocuments != null)
            {
                (this.windowDocuments as DocumentsWindView).IsRealClose = true;
                this.windowDocuments.Close();
            }
            if (this.windowNations != null)
            {
                (this.windowNations as NationsWindView).IsRealClose = true;
                this.windowNations.Close();
            }
            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            if (clientConnector.CheckAuthorize())
            {
                clientConnector.ExitAuthorize();
            }
        }
    }
}
