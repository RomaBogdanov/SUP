using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;

namespace SUPClient
{
    class Visitors1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IVisitors1Model visitors1Model;
        private IEnumerable<FullOrder> fullOrders;
        private FullOrder currentItem;
        private BitmapImage picture;
        public string numOrd = "0";

        public IEnumerable<FullOrder> FullOrders
        {
            get { return this.fullOrders; }
            set
            {
                this.fullOrders = value;
                OnPropertyChanged("FullOrders");
            }
        }
        public FullOrder CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (this.currentItem != value & value != null)
                {
                    this.currentItem = value;
                    this.numOrd = currentItem.OrderID;
                    OnPropertyChanged("CurrentItem");
                    this.visitors1Model?.GetImage(this.currentItem);
                }
            }
        }

        public BitmapImage Picture
        {
            get { return this.picture; }
            set
            {
                if (this.picture != value)
                {
                    this.picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }

        public Visitors1ViewModel()
        {
            this.visitors1Model = new Visitors1Model1(this);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
