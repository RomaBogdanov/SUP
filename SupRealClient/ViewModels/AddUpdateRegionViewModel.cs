using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Data;

namespace SupRealClient.ViewModels
{
    public class AddUpdateRegionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAddUpdateRegionModel model;
        private string name = "";
        private int countryId = 0;
        private string country = "";
        public int FontSize => GlobalSettings.GetFontSize();

        public string Caption { get; private set; }

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

        public CollectionView CountryList { get; private set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateRegionViewModel()
        {
        }

        public void SetModel(IAddUpdateRegionModel model)
        {
            this.model = model;
            this.Caption = model.Data.Id <= 0 ? "Добавление региона" :
                "Редактирование региона";
            this.CountryList =
                new CollectionView(RegionsHelper.GetCountries(model.Data.Country));
            this.Name = model.Data.Name;
            this.countryId = model.Data.CountryId;
            this.Country = model.Data.Country;

            this.Ok = new RelayCommand(arg => this.model.Ok(new Region
            {
                Name = Name,
                CountryId = countryId,
                Country = Country,
            }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
