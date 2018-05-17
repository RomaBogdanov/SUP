using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Задача, интерфейса - свести IBase1ViewModel и IBase4ViewModel
    /// в один интерфейс.
    /// </summary>
    public interface ISuperBaseViewModel
    {
        ICommand Add { get; set; }
        ICommand Update { get; set; }
        ICommand Search { get; set; }
        ICommand Farther { get; set; }
        ICommand Begin { get; set; }
        ICommand Prev { get; set; }
        ICommand Next { get; set; }
        ICommand End { get; set; }

        ICommand Close { get; set; }

        string SearchingText { get; set; }
        int SelectedIndex { get; set; }
        bool FartherEnabled { get; set; }
        DataGridColumn CurrentColumn { get; set; }
    }

    public interface IBase4ViewModel : ISuperBaseViewModel
    {
        ICommand Ok { get; set; }
        ICommand Zones { get; set; }
        ICommand RightClickCommand { get; set; }

        IWindow Parent { get; set; }

        string OkCaption { get; set; }
        Visibility ZonesVisibility { get; set; }
        
        
        //T CurrentItem { get; set; }
        
        //ObservableCollection<T> Set { get; set; }
        
        //IBase4Model<T> Model { get; set; }

    }
}
