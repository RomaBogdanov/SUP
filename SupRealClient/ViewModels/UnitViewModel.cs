using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.ViewModels
{
    public class UnitViewModel : ViewModelBase
    {
        public UnitViewModel()
        {
            OkCommand = new RelayCommand(obj => Ok());
            CancelCommand = new RelayCommand(obj => Cancel());
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                if (Model != null) Model.Description = value;
                OnPropertyChanged();
            }
        }
        private string _description;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        private string title;

        public IModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private IModel _model;

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void Ok()
        {
            Model.EditItem();
        }

        private void Cancel()
        {
            Model.Cancel();
        }
    }
}
