using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    /// <summary>
    /// Model для размеров окна.
    /// </summary>
    public class ChildWinSet : INotifyPropertyChanged
    {
        // Формируем размеры окна в зависимости от разрешения монитора.
        private double screenWidth { get; set; }
        private double screenHeighth { get; set; }
        private double sizePercentHeight = 0.78;
        private double sizePercentWidth = 0.5;

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
        public double Top { get; set; } = 140;

        public ChildWinSet()
        {
            screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            screenHeighth = System.Windows.SystemParameters.PrimaryScreenHeight;

            Height = screenHeighth * sizePercentHeight;
            Width = screenWidth * sizePercentWidth;
        }

        public ChildWinSet(double heightPercent, double widthPersent)
        {
            this.sizePercentHeight = heightPercent;
            this.sizePercentWidth = widthPersent;

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
