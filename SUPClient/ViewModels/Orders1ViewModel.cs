using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SUPClient
{
    class Orders1ViewModel : INotifyPropertyChanged
    {

        #region Values

        public event PropertyChangedEventHandler PropertyChanged;
        IOrders1Model orders1Model;
        private string selectedDate;
        private int countVisitors;
        private int countCars;
        private IEnumerable<FullOrder> fullOrders;
        private IEnumerable<Org> orgs;
        private IEnumerable<People> allPeoples;
        private FullOrder currentItem;
        private bool editingOrder = false;

        public string numOrd = "0";

        #endregion

        #region Properties

        public string SelectedDate
        {
            get { return this.selectedDate; }
            set
            {
                if (this.selectedDate != value && value != null)
                {
                    this.selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        public int CountVisitors
        {
            get { return this.countVisitors; }
            set
            {
                if (this.countVisitors != value)
                {
                    this.countVisitors = value;
                    OnPropertyChanged("CountVisitors");
                }
            }
        }

        public int CountCars
        {
            get { return this.countCars; }
            set
            {
                if (this.countCars != value)
                {
                    this.countCars = value;
                    OnPropertyChanged("CountCars");
                }
            }
        }

        public IEnumerable<FullOrder> FullOrders
        {
            get { return this.fullOrders; }
            set
            {
                this.fullOrders = value;
                OnPropertyChanged("FullOrders");
            }
        }

        public IEnumerable<Org> Orgs
        {
            get { return this.orgs; }
            set
            {
                this.orgs = value;
                OnPropertyChanged("Orgs");
            }
        }

        public IEnumerable<People> AllPeoples
        {
            get { return this.allPeoples; }
            set
            {
                this.allPeoples = value;
                OnPropertyChanged("AllPeoples");
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
                }
            }
        }

        /// <summary>
        /// Редактировать заявку.
        /// </summary>
        public bool EditingOrder
        {
            get { return this.editingOrder; }
            set
            {
                if (this.editingOrder != value)
                {
                    this.editingOrder = value;
                    OnPropertyChanged("EditingOrder");
                }
            }
        }
        
        /// <summary>
        /// Обработчик команды добавки новой заявки.
        /// </summary>
        public ICommand CreateOrder
        { get; set; }

        public ICommand SaveOrder
        { get; set; }

        public ICommand EditOrder
        { get; set; }

        public ICommand DeleteOrder
        { get; set; }

        #endregion

        public Orders1ViewModel()
        {
            this.orders1Model = new Order1Model1(this);
            this.CreateCommands();
            this.StartConditions();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

#region Private

        private void CreateCommands()
        {
            this.CreateOrder = new RelayCommand(arg => this.orders1Model.CreateOrder());
            this.SaveOrder = new RelayCommand(arg => this.orders1Model.SaveOrder());
            this.EditOrder = new RelayCommand(arg => this.orders1Model.EditOrder());
            this.DeleteOrder = new RelayCommand(arg => this.orders1Model.DeleteOrder());
        }

        private void StartConditions()
        {
            this.SelectedDate = DateTime.Now.ToString();
        }
        
#endregion

    }
}
