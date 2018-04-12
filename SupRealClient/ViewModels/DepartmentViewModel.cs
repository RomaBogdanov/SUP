using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;

namespace SupRealClient.ViewModels
{
    public class DepartmentViewModel : ViewModelBase
    {
        public DepartmentViewModel()
        {
            OkCommand = new RelayCommand(obj => Ok());
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private string _description;

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

        public void Ok()
        {
            Model = new Department { Description = Description };
        }
    }
}
