using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class Base1ViewModel : IBase1ViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Base1ModelAbstr model;
        private IEnumerable<object> set;
        private object currentItem;
        private DataGridCellInfo currentCell;
        private DataGridColumn currentColumn;
        private object selectedValue;
        private bool focused = false;
        private int selectedIndex;
        private string searchingText;

        public int NumItem { get; set; } = -1;

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged("SearchingText");
                this.model?.Searching(this.searchingText.ToUpper());
            }
        }

        public IEnumerable<object> Set
        {
            get { return this.set; }
            set
            {
                this.set = value;
                OnPropertyChanged("Set");
            }
        }

        public object SelectedValue
        {
            get { return this.selectedValue; }
            set
            {
                this.selectedValue = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        public object CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (value != null)
                {
                    this.currentItem = value;
                    model?.EnterCurrentItem(this.currentItem);
                    OnPropertyChanged("CurrentItem");
                }
            }
        }

        public DataGridColumn CurrentColumn
        {
            get { return this.currentColumn; }
            set
            {
                if (value != null)
                {
                    this.currentColumn = value;
                    OnPropertyChanged("CurrentColumn");
                }
            }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                this.selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public bool Focused
        {
            get { return focused; }
            set
            {
                this.focused = value;
                OnPropertyChanged("Focused");
            }
        }

        public ICommand Add
        { get; set; }

        public ICommand Update
        { get; set; }

        public ICommand Search
        { get; set; }

        public ICommand Farther
        { get; set; }

        public ICommand Begin
        { get; set; }

        public ICommand Prev
        { get; set; }

        public ICommand Next
        { get; set; }

        public ICommand End
        { get; set; }

        public ICommand Close
        { get; set; }
        /*
        public ICommand TextChanged
        { get; set; }
        */
        public virtual void SetModel(Base1ModelAbstr modelAbstr)
        {
            this.model = modelAbstr;
            this.Add = new RelayCommand(arg => model.Add());
            this.Update = new RelayCommand(arg => model.Update());
            this.Search = new RelayCommand(arg => model.Search());
            this.Farther = new RelayCommand(arg => model.Farther());
            this.Begin = new RelayCommand(arg => model.Begin());
            this.Prev = new RelayCommand(arg => model.Prev());
            this.Next = new RelayCommand(arg => model.Next());
            this.End = new RelayCommand(arg => model.End());
            this.Close = new RelayCommand(arg => model.Close());
            //this.TextChanged = new RelayCommand(arg => Chan(arg));
        }

        /*private void Chan(object arg)
        {
            //throw new NotImplementedException();
        }*/

        public Base1ViewModel()
        {
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
