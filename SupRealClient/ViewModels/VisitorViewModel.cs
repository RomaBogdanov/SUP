using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models;

namespace SupRealClient.ViewModels
{
    public class VisitorViewModel : ViewModelBase
    {
        public string WindowName
        {
            get { return _windowName; }
            set
            {
                _windowName = value;
                OnPropertyChanged();
            }
        }
        private string _windowName;

        public string CloseButtonName
        {
            get { return _closeButtonName; }
            set
            {
                _closeButtonName = value;
                OnPropertyChanged();
            }
        }
        private string _closeButtonName;

        public bool EmployeeTabVisible
        {
            get { return _employeeTabVisible; }
            set
            {
                _employeeTabVisible = value;
                OnPropertyChanged();
            }
        }
        private bool _employeeTabVisible;

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnPropertyChanged();
            }
        }
        private bool _enable;

        public VisitorViewModel(VisitorModeEnum mode)
        {
            if (mode == VisitorModeEnum.Add)
            {
                WindowName = "Добавление посетителя";
                CloseButtonName = "Отмена";

                EmployeeTabVisible = false;
                Enable = true;
            }
            else if (mode == VisitorModeEnum.Info)
            {
                WindowName = "Информация о постетите";
                CloseButtonName = "Отмена";

                EmployeeTabVisible = true;
                Enable = true;
            }
            else if (mode == VisitorModeEnum.Show)
            {
                WindowName = "Просмотр";
                CloseButtonName = "Закрыть";

                EmployeeTabVisible = true;
                Enable = false;
            }
        }
    }
}
