using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Models;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Views;

namespace SupRealClient.ViewModels
{
    public class VisitsViewModel : ViewModelBase
    {
        public ObservableCollection<Visit> VisitsList
        {
            get { return _visitsList; }
            set
            {
                _visitsList = value; 
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Visit> _visitsList = new ObservableCollection<Visit>();

        public Visit SelectedVisit
        {
            get { return _selectedVisit; }
            set
            {
                _selectedVisit = value;
                OnPropertyChanged();
            }
        }
        private Visit _selectedVisit;

        public ICommand AdditiolannyCommand { get; set; }

        public VisitsViewModel()
        {
            VisitsList.Add(new Visit
            {
                Id = 1,
                Visitor = new Human {FirstName = "Игорь", SecondName = "Печкин", ThirdName = "Иванович"},
                Organization = "Супер организация",
                Pass = new Pass {Id = 1},
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Author = new Human {FirstName = "Фёдор", SecondName = "Дядя", ThirdName = "Дмитриевич"},
                RealBidId = "1-1",
                RemovedBidId = "1-1",
                Steward = new Human {FirstName = "Кот", SecondName = "Матроскин", ThirdName = "Шерстяной"}
            });

            AdditiolannyCommand = new RelayCommand(obj => Additionally());
        }

        private void Additionally()
        {
            if (SelectedVisit != null)
            {
                var detailViewModel = new DetailVisitsViewModel(SelectedVisit);

                var window = new DetailVisitsView {DataContext = detailViewModel};
                window.Show();

                detailViewModel.ToString();
            }
            else
            {
                MessageBox.Show("Выберите посещение из списка!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
