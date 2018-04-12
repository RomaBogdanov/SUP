using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Behaviour;

namespace SupRealClient.ViewModels
{
    public class VisitorsViewModel : ViewModelBase
    {
        public VisitorsViewModel()
        {
            AddImageSourceCommand = new RelayCommand(obj => AddImageSource());
            RemoveImageSourceCommand = new RelayCommand(obj => RemoveImageSource());
        }
        
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

        public ICommand AddImageSourceCommand { get; set; }
        public ICommand RemoveImageSourceCommand { get; set; }

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
    }
}
