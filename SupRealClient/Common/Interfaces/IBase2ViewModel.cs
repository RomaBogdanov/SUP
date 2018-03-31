using SupRealClient.EnumerationClasses;
using System.Collections.Generic;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа методов Base2ViewModel из Base2Model
    /// </summary>
    interface IBase2ViewModel
    {
        IEnumerable<Organization> Organizations { set; }
    }
}
