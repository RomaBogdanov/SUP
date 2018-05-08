using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    class SynonimViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object> OnClose;

        private string fullName = "";
        private List<string> synonims = new List<string>();

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public List<string> Synonims
        {
            get { return synonims; }
            set
            {
                if (value != null)
                {
                    synonims = value;
                    OnPropertyChanged("Synonims");
                }
            }
        }

        public ICommand Cancel { get; set; }

        public SynonimViewModel(Organization org)
        {
            if (org != null)
            {
                var result = OrganizationsHelper.GetSynonims(org);
                FullName = result.Key;
                Synonims = result.Value;
            }
            this.Cancel = new RelayCommand(arg => OnCancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void OnCancel()
        {
            OnClose?.Invoke(null);
        }
    }
}
