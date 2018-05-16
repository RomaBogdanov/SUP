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
    interface IBase4ViewModel
    {
        ICommand Add { get; set; }
        ICommand Update { get; set; }
        ICommand Search { get; set; }
        ICommand Farther { get; set; }
        ICommand Begin { get; set; }
        ICommand Prev { get; set; }
        ICommand Next { get; set; }
        ICommand End { get; set; }
        ICommand Ok { get; set; }
        ICommand Close { get; set; }
        ICommand Zones { get; set; }
        ICommand RightClickCommand { get; set; }

        string SearchingText { get; set; }
        int SelectedIndex { get; set; }
        bool FartherEnabled { get; set; }
        DataGridColumn CurrentColumn { get; set; }

        IWindow Parent { get; set; }

        string OkCaption { get; set; }
        Visibility ZonesVisibility { get; set; }
        
        
        //T CurrentItem { get; set; }
        
        //ObservableCollection<T> Set { get; set; }
        
        //IBase4Model<T> Model { get; set; }

    }
}
