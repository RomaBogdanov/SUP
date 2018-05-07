using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.ViewModels
{
    public class FullNameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object> OnClose;

        private int orgId;
        private ObservableCollection<Organization> orgs =
            new ObservableCollection<Organization>();
        private int selectedOrg = -1;

        public ObservableCollection<Organization> Orgs
        {
            get { return orgs; }
            set
            {
                if (value != null)
                {
                    orgs = value;
                    OnPropertyChanged("Orgs");
                }
            }
        }

        public int SelectedOrg
        {
            get { return selectedOrg; }
            set
            {
                selectedOrg = value;
                OnPropertyChanged("SelectedOrg");
            }
        }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }

        public FullNameViewModel(int orgId)
        {
            this.orgId = orgId;
            Orgs = new ObservableCollection<Organization>(
                OrganizationsHelper.GetFullNameOrganizations(orgId));

            this.Ok = new RelayCommand(arg => OnOk(0));
            this.Cancel = new RelayCommand(arg => OnCancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void OnCancel()
        {
            OnClose?.Invoke(null);
        }

        public void OnOk(int id)
        {
            if (SelectedOrg < 0)
            {
                OnCancel();
            }
            else
            {
                OnClose?.Invoke(Orgs[SelectedOrg]);
            }
        }
    }
}
