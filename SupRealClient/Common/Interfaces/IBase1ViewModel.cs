using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа методов Base1ViewModel из Base1Model
    /// ПЕРЕИМЕНОВАТЬ В БОЛЕЕ ПОДХОДЯЩЕЕ НАЗВАНИЕ!!!
    /// </summary>
    public interface IBase1ViewModel : ISuperBaseViewModel
    {
        //ICommand Ok { get; set; }
        //ICommand Zones { get; set; }
        //ICommand RightClick { get; set; }

        int NumItem { get; set; }
        
        IEnumerable<object> Set { get; set; }
        
        object CurrentItem { get; set; }
        object SelectedValue { get; set; }
        
        bool Focused { get; set; }
        
        
        
        //ICommand Watch { get; set; }
        //ICommand DoubleClick { get; set; }
        
    }
}
