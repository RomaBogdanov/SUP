using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Timers;
using SupClientConnectionLib;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Xml;
using System;

namespace SupRealClient.ViewModels
{
    class Authorize1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string login;
        private string password;
        private List<string> hosts;
        bool IsAuthorization = false;
        Timer timer;
        int timerInterval = 3000;
        private ClientConnector connector;
        private MainWindowViewModel mainWindowViewModel;
        private string msg = "";
        private Brush infoStyle = Brushes.Red;
        SetupStorage setupStorage = SetupStorage.Current;

        public string Login
        {
            get { return this.login; }
            set
            {
                if (this.login != value)
                {
                    this.login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Msg
        {
            get { return msg; }
            set
            {
                this.msg = value;
                OnPropertyChanged("Msg");
            }
        }

        public Brush InfoStyle
        {
            get { return this.infoStyle; }
            set
            {
                this.infoStyle = value;
                OnPropertyChanged("InfoStyle");
            }
        }

        public List<string> Hosts
        {
            get { return this.hosts; }
            set
            {
                this.hosts = value;
                OnPropertyChanged("Hosts");
            }
        }

        public ICommand Enter
        { get; set; }

        public Authorize1ViewModel()
        {
            //this.mainWindowViewModel = MainWindowViewModel.Current;
            this.connector = ClientConnector.CurrentConnector;
            Enter = new RelayCommand(arg => Entering(arg));
            timer = new Timer(timerInterval);
            timer.Elapsed += Timer_Elapsed;

            var hostList = new List<string>
            {
                "<default>"
            };
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("Hosts.xml");
                XmlNode root = doc.FirstChild.NextSibling;
                foreach (XmlNode host in root.ChildNodes)
                {
                    hostList.Add(host.Attributes["Name"].Value);
                }
            }
            catch (Exception)
            {
            }
            Hosts = hostList;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Entering(object password)
        {
            this.mainWindowViewModel = MainWindowViewModel.Current;
            this.Password = ((PasswordBox)password).Password;
            ((PasswordBox)password).Clear();
            if (IsAuthorization)
            {
                this.connector.ExitAuthorize();
                IsAuthorization = false;
                //System.Windows.Forms.MessageBox.Show("Войти");
                this.mainWindowViewModel.AuthorizedUser = "Пользователь не назначен";
                this.InfoStyle = Brushes.Red;
                this.Msg = "Неуспешная попытка авторизации!";
                setupStorage.UserExit = true;
                this.ClearEnterData();
                //this.EnterButtonContent = "Войти";
                timer.Stop();
            }
            else
            {
                int id = this.connector.Authorize(Login, Password);
                if (id > 0)
                {
                    IsAuthorization = true;
                    //System.Windows.Forms.MessageBox.Show("Выйти");
                    this.mainWindowViewModel.AuthorizedUser = Login;
                    this.InfoStyle = Brushes.Green;
                    this.Msg = "Пользователь авторизован!";
                    setupStorage.UserExit = false;
                    this.ClearEnterData();
                    this.mainWindowViewModel.DataVisibility = Visibility.Visible;
                    new System.Threading.Thread(Invisible).Start();
                    //this.EnterButtonContent = "Выйти";
                    timer.Start();
                }
                else
                {
                    IsAuthorization = false;
                    //System.Windows.Forms.MessageBox.Show("Войти");
                    this.mainWindowViewModel.AuthorizedUser = "Пользователь не назначен";
                    this.InfoStyle = Brushes.Red;
                    this.Msg = "Неуспешная попытка авторизации!";
                    setupStorage.UserExit = true;
                    this.ClearEnterData();
                    //this.EnterButtonContent = "Войти";
                    //this.Msgs = "Аутентификация не прошла";
                    timer.Stop();
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool a = this.connector.CheckAuthorize();
            if (a)
            {
                IsAuthorization = true;
                //this.EnterButtonContent = "Выйти";
            }
            else
            {
                IsAuthorization = false;
                //this.EnterButtonContent = "Войти";
                timer.Stop();
            }
        }

        private void ClearEnterData()
        {
            this.Login = "";
            this.Password = "";
            
        }

        private void Invisible()
        {
            System.Threading.Thread.Sleep(1000);
            this.mainWindowViewModel.LoginVisibility = Visibility.Hidden;
        }
    }
}
