using System.Collections.Generic;
using System.Data;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Данные и методы для поиска в модели
    /// </summary>
    public interface ISearchHelper
    {
        DataRow[] Rows { get; }
        void SetAt(int index);
        IDictionary<string, string> GetFields();
    }
}
