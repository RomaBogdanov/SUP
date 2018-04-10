using System.Collections.Generic;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа методов Base1ViewModel из Base1Model
    /// ПЕРЕИМЕНОВАТЬ В БОЛЕЕ ПОДХОДЯЩЕЕ НАЗВАНИЕ!!!
    /// </summary>
    public interface IBase1ViewModel
    {
        int NumItem { get; set; }
        IEnumerable<object> Set { get; set; }
        int SelectedIndex { get; set; }
        object CurrentItem { get; set; }
        object SelectedValue { get; set; }
    }
}
