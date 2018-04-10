using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    class Base2ViewModel : IBase2ViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IOrganizations1Model model;
        private IEnumerable<Organization> organizations;
        private Organization currentItem;
        private object selectedValue;

        public IEnumerable<Organization> Organizations
        {
            get { return this.organizations; }
            set
            {
                this.organizations = value;
                OnPropertyChanged("Items");
            }
        }

        public Organization CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (this.currentItem != value & value != null)
                {
                    this.currentItem = value;
                    OnPropertyChanged("CurrentItem");
                }
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

        public ICommand Create
        { get; set; }

        public ICommand Edit
        { get; set; }

        public ICommand Delete
        { get; set; }

        public Base2ViewModel()
        {
            this.model = new Organizations1Model(this);
            this.CreateCommands();
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        private void CreateCommands()
        {
            this.Create = new RelayCommand(arg => this.model.Create());
            this.Edit = new RelayCommand(arg => this.model.Edit());
            this.Delete = new RelayCommand(arg => this.model.Delete());
        }

    }
}
