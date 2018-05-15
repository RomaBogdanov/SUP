using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System;
using SupRealClient.EnumerationClasses;
using System.Linq;
using SupRealClient.Search;
using SupRealClient.Common;

namespace SupRealClient.ViewModels
{
    public class FullNameOrganization : Organization
    {
        public FullNameOrganization(Organization org)
        {
            this.Id = org.Id;
            this.Type = org.Type;
            this.Name = org.Name;
            this.CountryId = org.CountryId;
            this.Country = org.Country;
            this.RegionId = org.RegionId;
            this.Region = org.Region;
        }

        public string OrgFullName
        {
            get
            {
                return OrganizationsHelper.
                  GenerateFullName(Id, true);
            }
        }

        public override string ToString()
        {
            return OrgFullName;
        }
    }

    public class FullNameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object> OnClose;

        private int orgId;
        private ObservableCollection<Organization> orgs =
            new ObservableCollection<Organization>();
        private int selectedOrg = -1;
        private string searchingText;
        private bool fartherEnabled;

        private SearchResult searchResult = new SearchResult();

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

        public bool FartherEnabled
        {
            get { return fartherEnabled; }
            set
            {
                this.fartherEnabled = value;
                OnPropertyChanged("FartherEnabled");
            }
        }

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged("SearchingText");
                FartherEnabled = Searching(this.searchingText.ToUpper());
            }
        }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand FartherCommand { get; set; }

        public FullNameViewModel(int orgId)
        {
            this.orgId = orgId;
            Orgs = new ObservableCollection<Organization>(
                OrganizationsHelper.GetFullNameOrganizations(orgId).Select(o =>
                    new FullNameOrganization(o)));

            this.Ok = new RelayCommand(arg => OnOk(0));
            this.Cancel = new RelayCommand(arg => OnCancel());
            this.FartherCommand = new RelayCommand(arg => Farther());
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

        private bool Searching(string pattern)
        {
            searchResult = new SearchResult();
            if (string.IsNullOrEmpty(pattern))
            {
                return false;
            }
            for (int i = 0; i < Orgs.Count; i++)
            {
                if (CommonHelper.IsSearchConditionMatch(
                    Orgs[i].ToString(), pattern))
                {
                    searchResult.Add(Orgs[i].Id);
                }
            }
            SetAt(searchResult.Begin());

            return searchResult.Any();
        }

        private void Farther()
        {
            SetAt(searchResult.Next());
        }

        private void SetAt(long id)
        {
            for (int i = 0; i < Orgs.Count(); i++)
            {
                if (Orgs[i].Id == id)
                {
                    SelectedOrg = i;
                    break;
                }
            }
        }
    }
}
