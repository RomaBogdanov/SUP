using System.ComponentModel;

namespace SupRealClient.Views
{
    /// <summary>
    /// ViewModel окна "Заявки".
    /// </summary>
    public class BidsViewModel : INotifyPropertyChanged
    {
        private ChildWinSet _settings;
        /// <summary>
        /// Настройки окна "Заявки".
        /// </summary>
        public ChildWinSet Settings
        {
            get { return _settings; }
            set { _settings = value; OnPropertyChanged("Settings"); }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public BidsViewModel()
        {
            Settings = new ChildWinSet(); //0.9, 0.4
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
