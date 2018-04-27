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
using SupRealClient.Views.Visitor;

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

            AddNewVisitorCommand = new RelayCommand(obj => AddNewVisitor());
            InfoVisitorCommand = new RelayCommand(obj => InfoVisitor());
            ShowVisitorCommand = new RelayCommand(obj => ShowVisitor());
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
        /// Свойство видимости для вкладки 'Пропуска'
        /// </summary>
        public bool AccessVisibility
        {
            get { return _accessVisibility; }
            set
            {
                _accessVisibility = value;
                OnPropertyChanged();
            }
        }
        private bool _accessVisibility;

        /// <summary>
        /// Свойство видимости для вкладки 'Заявки'
        /// </summary>
        public bool BidsVisibility
        {
            get { return _bidsVisibility; }
            set
            {
                _bidsVisibility = value;
                OnPropertyChanged();
            }
        }
        private bool _bidsVisibility;
        
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

        /// <summary>
        /// Кнопка добавить в панели кнопок
        /// </summary>
        public ICommand AddNewVisitorCommand { get; set; }
        /// <summary>
        /// Кнопка правка в панели кнопок
        /// </summary>
        public ICommand InfoVisitorCommand { get; set; }

        /// <summary>
        /// Команда пока просто висит, потому что я не знаю на какую кнопку её навесить
        /// </summary>
        public ICommand ShowVisitorCommand { get; set; }

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

        private void AddNewVisitor()
        {
            var visitorViewModel = new VisitorViewModel(VisitorModeEnum.Add);

            var window = new AddVisitorView {DataContext = visitorViewModel};
            window.ShowDialog();

            visitorViewModel.ToString();
        }

        private void ShowVisitor()
        {
            var visitorViewModel = new VisitorViewModel(VisitorModeEnum.Show);

            var window = new AddVisitorView { DataContext = visitorViewModel };
            window.ShowDialog();

            visitorViewModel.ToString();
        }

        private void InfoVisitor()
        {
            var visitorViewModel = new VisitorViewModel(VisitorModeEnum.Info);

            var window = new AddVisitorView { DataContext = visitorViewModel };
            window.ShowDialog();

            visitorViewModel.ToString();
        }
    }
}
