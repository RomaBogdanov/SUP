using System.Collections.Generic;
using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.Views;
using System.Windows.Data;

namespace SupRealClient.ViewModels
{
    public class AddUpdateOrgsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAddUpdateOrgsModel model;
        private string type = "";
        private string name = "";
        private string comment = "";
        private string fullName = "";
        private bool fullNameEnabled;
        private int countryId = 0;
        private string country = "";
        private int regionId = 0;
        private string region = "";
        private bool countryRegionEnabled;
        private int synId = 0;
        public int FontSize => GlobalSettings.GetFontSize();

        public ICommand FullNameCommand { get; set; }
        public ICommand CountryCommand { get; set; }
        public ICommand RegionCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public string Caption { get; private set; }

        public string Type
        {
            get { return type; }
            set
            {
                if (value != null)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != null)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value != null)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        public bool FullNameEnabled
        {
            get { return fullNameEnabled; }
            set
            {
                fullNameEnabled = value;
                OnPropertyChanged("FullNameEnabled");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                if (value != null)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public string Region
        {
            get { return region; }
            set
            {
                if (value != null)
                {
                    region = value;
                    OnPropertyChanged("Region");
                }
            }
        }

        public bool CountryRegionEnabled
        {
            get { return countryRegionEnabled; }
            set
            {
                countryRegionEnabled = value;
                OnPropertyChanged("CountryRegionEnabled");
            }
        }

        public CollectionView TypeList { get; private set; }

        public CollectionView DescriptionList { get; private set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateOrgsViewModel()
        {
            
        }

        public void SetModel(IAddUpdateOrgsModel addItem1Model)
        {
            this.model = addItem1Model;
            this.Caption = model.Data.Id <= 0 ? "Добавление организации" :
                "Редактирование организации";
            this.TypeList =
                new CollectionView(OrganizationsHelper.GetTypes(model.Data.Type));
            this.DescriptionList =
                new CollectionView(OrganizationsHelper.GetNames(model.Data.Name));
            this.Type = model.Data.Type;
            this.Name = OrganizationsHelper.TrimName(model.Data.Name);
            this.Comment = model.Data.Comment;
            this.FullName = model.Data.SynId <= 0 ? "" :
                OrganizationsHelper.GenerateFullName(model.Data.Id);
            this.FullNameEnabled = OrganizationsHelper.FullNameEnabled(model.Data.Id);
            this.countryId = model.Data.CountryId;
            this.Country = model.Data.Country;
            this.regionId = model.Data.RegionId;
            this.Region = model.Data.Region;
            this.synId = model.Data.SynId;
            this.CountryRegionEnabled = this.synId <= 0;

            this.Ok = new RelayCommand(arg => this.model.Ok(new Organization
            {
                Id = model.Data.Id,
                Type = Type,
                Name = OrganizationsHelper.TrimName(Name),
                Comment = Comment,
                CountryId = countryId,
                Country = Country,
                RegionId = regionId,
                Region = Region,
                SynId = synId
            }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());

            CountryCommand = new RelayCommand(arg => CountyList());
            RegionCommand = new RelayCommand(arg => RegionList());
            FullNameCommand = new RelayCommand(arg => FullNameList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void CountyList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4NationsWindViewOk", null) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            countryId = result.Id <= 0 ? 0 : result.Id;
            Country = result.Name;

            if (!RegionsHelper.CheckRegion(countryId, regionId))
            {
                regionId = 0;
                Region = "";
            }
        }

        private void RegionList()
        {
            var result = ViewManager.Instance.OpenRegions(
                countryId) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            regionId = result.Id <= 0 ? 0 : result.Id;
            if (!RegionsHelper.CheckRegion(countryId, regionId))
            {
                regionId = 0;
                Region = "";
            }
            else
            {
                Region = result.Name;
            }
        }

        private void FullNameList()
        {
            var window = new FullNameView(
                new FullNameViewModel(model.Data.Id));
            window.ShowDialog();
            var organization = window.WindowResult as Organization;

            if (organization == null)
            {
                return;
            }

            synId = organization.Id;
            CountryRegionEnabled = synId <= 0;
            FullName = OrganizationsHelper.GenerateFullName(synId, true);
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Country":
                    countryId = 0;
                    Country = "";
                    if (!RegionsHelper.CheckRegion(countryId, regionId))
                    {
                        regionId = 0;
                        Region = "";
                    }
                    break;
                case "Region":
                    regionId = 0;
                    Region = "";
                    break;
                case "FullName":
                    synId = 0;
                    CountryRegionEnabled = synId <= 0;
                    FullName = "";
                    break;
                default:
                    return;
            }
        }
    }
}
