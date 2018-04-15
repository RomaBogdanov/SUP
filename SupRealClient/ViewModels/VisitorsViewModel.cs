using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Behaviour;
using SupRealClient.Models;
using SupRealClient.Models.OrganizationStructure;

namespace SupRealClient.ViewModels
{
    public class VisitorsViewModel : ViewModelBase
    {
        public VisitorsViewModel()
        {
            PassList.Add(new Pass
            {
                Human = new Human {FirstName = "Джеймс", SecondName = "Бонд", ThirdName = "007"},
                Id = 1,
                BidId = 1,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                TimePassChange = DateTime.Now,
                Additionally = "- пароль? - я к маме на работу - проходи!"
            });

            PassList.Add(new Pass
            {
                Human = new Human { FirstName = "Джеймс", SecondName = "Бонд", ThirdName = "007" },
                Id = 1,
                BidId = 1,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                TimePassChange = DateTime.Now,
                Additionally = "- пароль? - я к маме на работу - проходи!"
            });

            PassList.Add(new Pass
            {
                Human = new Human { FirstName = "Джеймс", SecondName = "Бонд", ThirdName = "007" },
                Id = 1,
                BidId = 1,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                TimePassChange = DateTime.Now,
                Additionally = "- пароль? - я к маме на работу - проходи!"
            });

            PassList.Add(new Pass
            {
                Human = new Human { FirstName = "Джеймс", SecondName = "Бонд", ThirdName = "007" },
                Id = 1,
                BidId = 1,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                TimePassChange = DateTime.Now,
                Additionally = "- пароль? - я к маме на работу - проходи!"
            });

            PassList.Add(new Pass
            {
                Human = new Human { FirstName = "Джеймс", SecondName = "Бонд", ThirdName = "007" },
                Id = 1,
                BidId = 1,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                TimePassChange = DateTime.Now,
                Additionally = "- пароль? - я к маме на работу - проходи!"
            });

            AddImageSourceCommand = new RelayCommand(obj => AddImageSource());
            RemoveImageSourceCommand = new RelayCommand(obj => RemoveImageSource());

            AddSignatureCommand = new RelayCommand(obj => AddSignature());
            RemoveSignatureCommand = new RelayCommand(obj => RemoveSignature());
        }

        public ObservableCollection<Pass> PassList
        {
            get { return _passList; }
            set
            {
                _passList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Pass> _passList = new ObservableCollection<Pass>();

        /// <summary>
        /// Свойтство содержит в себе путь к фотографии
        /// </summary>
        public string PhotoSource
        {
            get { return _photoSource; }
            set
            {
                _photoSource = value; 
                OnPropertyChanged();
            }
        }
        private string _photoSource;

        /// <summary>
        /// Свойтство содержит в себе путь к фотографии
        /// </summary>
        public string Signature
        {
            get { return _signature; }
            set
            {
                _signature = value;
                OnPropertyChanged();
            }
        }
        private string _signature;

        public ICommand AddImageSourceCommand { get; set; }
        public ICommand RemoveImageSourceCommand { get; set; }

        public ICommand AddSignatureCommand { get; set; }
        public ICommand RemoveSignatureCommand { get; set; }

        private void AddImageSource()
        {
            var path = DialogService.OpenFileDialog();

            if (path != "")
            {
                PhotoSource = path;
            }
        }

        private void RemoveImageSource()
        {
            PhotoSource = "";
        }

        private void AddSignature()
        {
            var path = DialogService.OpenFileDialog();

            if (path != "")
            {
                Signature = path;
            }
        }

        private void RemoveSignature()
        {
            Signature = "";
        }
    }
}
