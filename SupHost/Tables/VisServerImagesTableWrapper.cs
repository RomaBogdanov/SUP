using SupHost.Connectors;

namespace SupHost
{
    class VisServerImagesTableWrapper : AbstractTableWrapper
    {
        public VisServerImagesTableWrapper()
        {
            this.getTableBehavior = new VisServerImagesTableBehavior();
        }

        public Connector GetConnector()
        {
            return (this.getTableBehavior as VisServerImagesTableBehavior).GetImageConnector();
        }
    }
}
