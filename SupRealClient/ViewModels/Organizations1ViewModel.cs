using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;
using System.Windows.Controls;

namespace SupRealClient
{
    class Organizations1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IOrganizations1Model model;
        private IEnumerable<Organization> organizations;
        private Organization currentItem;

        public IEnumerable<Organization> Organizations
        {
            get { return this.organizations; }
            set
            {
                this.organizations = value;
                OnPropertyChanged("Organizations");
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

        public ICommand Create
        { get; set; }

        public ICommand Edit
        { get; set; }

        public ICommand Delete
        { get; set; }

        public Organizations1ViewModel()
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
