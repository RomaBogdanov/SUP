using System.ComponentModel;
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

        public string Caption { get; private set; }

        public string Name
        {
            get => name;
            set
            {
                if (value != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Country
        {
            get => country;
            set
            {
                if (value != null)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public CollectionView CountryList { get; private set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetModel(IAddUpdateRegionModel model)
        {
            this.model = model;
            var isAdding = model.Data.Id <= 0;
            Caption = isAdding ? "Добавление региона" : "Редактирование региона";
            var countries = RegionsHelper.GetCountries().ToList();
            if (!countries.Any(p => p.EqualsWithoutSpacesAndCase(model.Data.Country)))
                countries.Add(model.Data.Country);
            var visibleCountries = countries.Count(p => !p.IsNullOrEmptyOrWhiteSpaces()) > 0
                ? countries.Where(p => !p.IsNullOrEmptyOrWhiteSpaces())
                : countries;

            CountryList = new CollectionView(visibleCountries);
            Name = model.Data.Name;
            countryId = model.Data.CountryId;
            Country = model.Data.Country;

            Ok = new RelayCommand(arg => this.model.Ok(new Region
            {
                Name = Name,
                CountryId = countryId,
                Country = Country
            }));
            Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}