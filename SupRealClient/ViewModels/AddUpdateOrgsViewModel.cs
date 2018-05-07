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
        private int countryId = 0;
        private string country = "";
        private int regionId = 0;
        private string region = "";
        public int FontSize => GlobalSettings.GetSettings();

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
                new CollectionView(OrganizationsHelper.GetTypes());
            this.DescriptionList =
                new CollectionView(OrganizationsHelper.GetDescriptions());
            this.Type = model.Data.Type;
            this.Name = model.Data.Name;
            this.Comment = model.Data.Comment;
            this.FullName = model.Data.FullName;
            this.countryId = model.Data.CountryId;
            this.Country = model.Data.Country;
            this.regionId = model.Data.RegionId;
            this.Region = model.Data.Region;

            this.Ok = new RelayCommand(arg => this.model.Ok(new Organization
            {
                Type = Type,
                Name = Name,
                Comment = Comment,
                FullName = FullName,
                CountryId = countryId,
                Country = Country,
                RegionId = regionId,
                Region = Region
            }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());

            CountryCommand = new RelayCommand(arg => CountyList());
            RegionCommand = new RelayCommand(arg => RegionList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void CountyList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4NationsWindView", null) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            countryId = result.Id <= 0 ? 0 : result.Id;
            Country = result.Name;
        }

        private void RegionList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4RegionsWindView", null) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            regionId = result.Id <= 0 ? 0 : result.Id;
            Region = result.Name;
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Country":
                    countryId = 0;
                    Country = "";
                    break;
                case "Region":
                    regionId = 0;
                    Region = "";
                    break;
                case "FullName":
                    FullName = "";
                    break;
                default:
                    return;
            }
        }
    }
}
