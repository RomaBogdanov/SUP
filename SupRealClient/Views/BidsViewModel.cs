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

        private bool _textEnable = false;
        /// <summary>
        /// Доступность редактирования полей.
        /// </summary>
        public bool TextEnable
        {
            get { return _textEnable; }
            set { _textEnable = value; OnPropertyChanged("TextEnable"); }
        }

        private bool _cceptButtonEnable = false;
        /// <summary>
        /// Доступность кнопок Применить и Отмена.
        /// </summary>
        public bool AcceptButtonEnable
        {
            get { return _cceptButtonEnable; }
            set { _cceptButtonEnable = value; OnPropertyChanged("AcceptButtonEnable"); }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public BidsViewModel()
        {
            Settings = new ChildWinSet() { Top = 120 };
            Settings.Left = Settings.Width;

            TextEnable = false; // При открытии окна поля недоступны.
            AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
