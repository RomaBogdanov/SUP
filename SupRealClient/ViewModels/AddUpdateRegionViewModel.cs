using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.Utils;

namespace SupRealClient.ViewModels
{
    public class AddUpdateRegionViewModel : INotifyPropertyChanged
    {
        private string country = "";
        private int countryId;
        private IAddUpdateRegionModel model;
        private string name = "";

        public int FontSize => GlobalSettings.GetFontSize();

        public ICommand CountryCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public string Caption { get; private set; }

        public string Name
        {
            get => name;
            set
            {
                if (value != null)
                {
                    name = value?.TrimStart();
                    OnPropertyChanged("Name");
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

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetModel(IAddUpdateRegionModel model)
        {
            this.model = model;
            var isAdding = model.Data.Id <= 0;
            Caption = isAdding ? "Добавление региона" : "Редактирование региона";

            Name = model.Data.Name;
            countryId = model.Data.CountryId;
            Country = model.Data.Country;

            if (isAdding)
            {
                Country = GetDefaultCountry();
                countryId = RegionsHelper.GetId(Country);
            }                          

            Ok = new RelayCommand(arg => this.model.Ok(new Region
            {
                Name = Name,
                CountryId = countryId,
                Country = Country
            }));
            Cancel = new RelayCommand(arg => this.model.Cancel());

            CountryCommand = new RelayCommand(arg => CountyList());
            ClearCommand = new RelayCommand(arg => Clear());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        private string GetDefaultCountry()
        {
            var countries = GetCountries().ToList();
            if (!countries.Any(p => p.EqualsWithoutSpacesAndCase(model.Data.Country)))
                countries.Add(model.Data.Country);
            var visibleCountries = countries.Count(p => !p.IsNullOrEmptyOrWhiteSpaces()) > 0
                ? countries.Where(p => !p.IsNullOrEmptyOrWhiteSpaces())
                : countries;

            return visibleCountries.FirstOrDefault(o => o?.ToUpper() == @"РОССИЯ");
        }

        System.Collections.Generic.IEnumerable<string> GetCountries()
        {
            var countries = TabsSingleton.CountriesWrapper.CurrentTable();
            foreach (DataRow row in countries.Table.Rows)
                if (!row.Field<string>("f_cntr_name").IsNullOrEmptyOrWhiteSpaces() &&
                    Common.CommonHelper.NotDeleted(row))
                    yield return row.Field<string>("f_cntr_name");
        }

        void CountyList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4NationsWindViewOk", null) as Views.BaseModelResult;
            if (result == null)
            {
                return;
            }

            countryId = result.Id <= 0 ? 0 : result.Id;
            Country = result.Name;
        }

        void Clear()
        {
            countryId = 0;
            Country = "";            
        }
    }
}