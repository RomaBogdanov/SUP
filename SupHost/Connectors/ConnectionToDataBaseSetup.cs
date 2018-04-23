using System.Data;
using System.Data.Common;

namespace SupHost.Connectors
{
    /// <summary>
    /// Класс, содержащий настройки для 
    /// </summary>
    public class ConnectionToDataBaseSetup
    {
        public DataTable Table { get; set; }
        public DbDataAdapter DataAdapter { get; set; }
    }
}
