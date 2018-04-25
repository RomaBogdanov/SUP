using SupHost.Connectors;

namespace SupHost
{
    /// <summary>
    /// Класс с обобщённым кодом для таблиц получаемых из базы данных Visitors.
    /// </summary>
    abstract class VisitorsDBTableBehavior : BaseTableBehavior
    {
        protected Connector connector = VisConnector.CurrentConnector;

        protected override Connector GetConnector()
        {
            return connector;
        }
    }
}
