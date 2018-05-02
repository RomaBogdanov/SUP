using SupHost.Connectors;

namespace SupHost
{
    class VisImagesTableBehavior : BaseTableBehavior
    {
        private Connector connector = ImagesConnector.CurrentConnector;

        public VisImagesTableBehavior()
        {
            this.query = $"select f_image_id, f_image_alias, f_visitor_id, f_image_type, f_deleted from vis_image";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_image_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_image";
        }

        protected override Connector GetConnector()
        {
            return connector;
        }
    }
}
