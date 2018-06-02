using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Settings = new ChildWinSet();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    /// <summary>
    /// Model для размеров окна.
    /// </summary>
    public class ChildWinSet : INotifyPropertyChanged
    {
        // Формируем размеры окна в зависимости от разрешения монитора.
        private double screenWidth { get; set; }
        private double screenHeighth { get; set; }
        private double sizePercentHeight = 0.9;
        private double sizePercentWidth = 0.4;

        private double _height;
        private double _width;

        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }
        public double Left { get; set; }
        public double Top { get; set; }

        public ChildWinSet()
        {
            screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            screenHeighth = System.Windows.SystemParameters.PrimaryScreenHeight;

            Height = screenHeighth * sizePercentHeight;
            Width = screenWidth * sizePercentWidth;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
