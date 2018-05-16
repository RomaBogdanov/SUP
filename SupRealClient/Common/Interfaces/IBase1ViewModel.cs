using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа методов Base1ViewModel из Base1Model
    /// ПЕРЕИМЕНОВАТЬ В БОЛЕЕ ПОДХОДЯЩЕЕ НАЗВАНИЕ!!!
    /// </summary>
    public interface IBase1ViewModel
    {
        ICommand Add { get; set; }
        ICommand Update { get; set; }
        ICommand Search { get; set; }
        ICommand Farther { get; set; }
        ICommand Begin { get; set; }
        ICommand Prev { get; set; }
        ICommand Next { get; set; }
        ICommand End { get; set; }
        //ICommand Ok { get; set; }
        ICommand Close { get; set; }
        //ICommand Zones { get; set; }
        //ICommand RightClick { get; set; }

        string SearchingText { get; set; }
        int SelectedIndex { get; set; }
        bool FartherEnabled { get; set; }
        DataGridColumn CurrentColumn { get; set; }



        int NumItem { get; set; }
        
        IEnumerable<object> Set { get; set; }
        
        object CurrentItem { get; set; }
        object SelectedValue { get; set; }
        
        bool Focused { get; set; }
        
        
        
        //ICommand Watch { get; set; }
        //ICommand DoubleClick { get; set; }
        
    }
}
