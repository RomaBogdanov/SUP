using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    class SynonimViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object> OnClose;

        private string fullName = "";
        private string firstSynonim = "";
        private Visibility firstSynonimVisibility = Visibility.Collapsed;
        private Dictionary<int, string> synonims = new Dictionary<int, string>();
        private KeyValuePair<int, string>? selectedSynonim;

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public string FirstSynonim
        {
            get { return firstSynonim; }
            set
            {
                firstSynonim = value;
                OnPropertyChanged("FirstSynonim");
            }
        }

        public Visibility FirstSynonimVisibility
        {
            get { return firstSynonimVisibility; }
            set
            {
                firstSynonimVisibility = value;
                OnPropertyChanged("FirstSynonimVisibility");
            }
        }

        public Dictionary<int, string> Synonims
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

        public KeyValuePair<int, string>? SelectedSynonim
        {
            get { return selectedSynonim; }
            set
            {
                selectedSynonim = value;
                OnPropertyChanged("SelectedSynonim");
            }
        }

        public ICommand Cancel { get; set; }
        public ICommand FullNameDoubleClickCommand { get; set; }
        public ICommand FirstSynonimDoubleClickCommand { get; set; }
        public ICommand SynonimsDoubleClickCommand { get; set; }

        public SynonimViewModel(Organization org)
        {
            if (org != null)
            {
                var result = OrganizationsHelper.GetSynonims(org);
                FullName = result.Key;
                FirstSynonim = OrganizationsHelper.GenerateFullName(org);
                FirstSynonimVisibility = org.SynId == 0 ?
                    Visibility.Collapsed : Visibility.Visible;
                Synonims = result.Value;
            }
            this.Cancel = new RelayCommand(arg => OnCancel());
            this.FullNameDoubleClickCommand = new RelayCommand(arg => OnOk(org.SynId));
            this.FirstSynonimDoubleClickCommand = new RelayCommand(arg => OnOk(null));
            this.SynonimsDoubleClickCommand = new RelayCommand(arg => SynonimsDoubleClick());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void OnCancel()
        {
            OnClose?.Invoke(null);
        }

        private void OnOk(int? id)
        {
            OnClose?.Invoke(id);
        }

        private void SynonimsDoubleClick()
        {
            if (SelectedSynonim != null)
            {
                OnOk(SelectedSynonim.Value.Key);
            }
        }
    }
}
