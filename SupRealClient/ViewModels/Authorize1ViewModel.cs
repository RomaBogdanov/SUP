using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Timers;
using SupClientConnectionLib;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Models;

namespace SupRealClient.ViewModels
{
    class Authorize1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string login;
        private string password;
        private KeyValuePair<string, string> selectedHost;
        private Dictionary<string, string> hosts;
        bool IsAuthorization = false;
        Timer timer;
        int timerInterval;
        private ClientConnector connector;
        private MainWindowViewModel mainWindowViewModel;
        private string msg = "";
        private Brush infoStyle = Brushes.Red;
        SetupStorage setupStorage = SetupStorage.Current;
        Timer logoutTimer;
        int logoutInterval = 180000;
        public int FontSize => GlobalSettings.GetFontSize() + 4;

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

        public KeyValuePair<string, string> SelectedHost
        {
            get { return this.selectedHost; }
            set
            {
                this.selectedHost = value;
                OnPropertyChanged("SelectedHost");
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

        public Dictionary<string, string> Hosts
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
            int res;
            timerInterval = int.TryParse(
                ConfigurationManager.AppSettings["PingTimeout"], out res) ? res: 3000;
            //this.mainWindowViewModel = MainWindowViewModel.Current;
            //this.connector = ClientConnector.CurrentConnector;
            Enter = new RelayCommand(arg => Entering(arg));
            timer = new Timer(timerInterval);
            timer.Elapsed += Timer_Elapsed;
            logoutTimer = new Timer(logoutInterval);
            logoutTimer.Elapsed += LogoutTimer_Elapsed;

            var hostList = new Dictionary<string, string>();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                hostList.Add(key.ToUpper(), ConfigurationManager.AppSettings[key]);
            }
            Hosts = hostList;
            Reset();
        }

        public void Reset()
        {
            IsAuthorization = false;
            if (Hosts.Count > 0)
            {
                SelectedHost = Hosts.ElementAt(0);
            }
            timer.Stop();
            setupStorage.UserExit = true;
            this.ClearEnterData();
            this.Msg = "";
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
                SetLoginInfo(false, "Неуспешная попытка авторизации!");
            }
            else
            {
                try
                {
                    this.connector = ClientConnector.ResetConnector(ParseUri());
                    int id = this.connector.Authorize(Login, Password);

                    logoutTimer.Stop();
                    int timeout = GetUserTimeout(id);
                    if (timeout > 0)
                    {
                        //InputProvider.GetInputProvider().Init(OnInput);
                        logoutTimer.Interval = GetUserTimeout(id) * 1000;
                        logoutTimer.Start();
                    }

                    SetLoginInfo(id > 0, id > 0 ? "Пользователь авторизован!" :
                        "Пользователь не назначен");
                }
                catch (Exception ex)
                {
                    SetLoginInfo(false, ex.Message);
                }
            }
        }

        private void SetLoginInfo(bool success, string message)
        {
            IsAuthorization = success;
            this.mainWindowViewModel.AuthorizedUser = success ?
                Login : "Пользователь не назначен";
            this.InfoStyle = success ? Brushes.Green : Brushes.Red;
            this.Msg = message;
            setupStorage.UserExit = !success;
            this.ClearEnterData();
            if (success)
            {
                this.mainWindowViewModel.DataVisibility = Visibility.Visible;
                new System.Threading.Thread(Invisible).Start();
                timer.Start();
            }
            else
            {
                //InputProvider.GetInputProvider().Close(OnInput);
                this.mainWindowViewModel.DataVisibility = Visibility.Hidden;
                this.mainWindowViewModel.LoginVisibility = Visibility.Visible;
                timer.Stop();
                logoutTimer.Stop();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool a = this.connector.Ping();
            if (a)
            {
                IsAuthorization = true;
                //this.EnterButtonContent = "Выйти";
            }
            else
            {
                IsAuthorization = false;
                //this.EnterButtonContent = "Войти";
                //this.connector.ExitAuthorize();
                SetLoginInfo(false, "Потеряно соединение с сервером!");
            }
        }

        private void LogoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            /*logoutTimer.Stop();
            this.connector.ExitAuthorize();
            SetLoginInfo(false, "Пользователь вышел по таймауту!");*/
        }

        private int GetUserTimeout(int id)
        {
            UsersWrapper usersWrapper = UsersWrapper.CurrentTable();
            DataTable table = usersWrapper.Table;
            ClientConnector tabConnector = usersWrapper.Connector;
            string tabName = usersWrapper.Table.TableName;
            var users = from u in table.AsEnumerable()
                       where u.Field<int>("f_user_id") == id
                       select new
                       {
                           Timeout = u.Field<int>("f_timeout")
                       };
            var user = users.FirstOrDefault();

            return user != null ? user.Timeout : -1;
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

        private Uri ParseUri()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                var serviceModel = config.SectionGroups["system.serviceModel"];
                ClientSection client = (ClientSection)serviceModel.Sections["client"];
                foreach (ChannelEndpointElement endpoint in client.Endpoints)
                {
                    if (SelectedHost.Key == endpoint.Address.Host.ToUpper())
                    {
                        return endpoint.Address;
                    }
                }
            }
            catch (Exception)
            {
            }
            throw new ArgumentException("Неправильная конфигурация!");
        }

        private void OnInput()
        {
            logoutTimer.Stop();
            logoutTimer.Start();
        }
    }
}
