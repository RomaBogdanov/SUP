using SupHost.Connectors;

namespace SupHost
{
    class VisImageDocumentTableBehavior : BaseTableBehavior
    {
        private Connector connector = ImagesConnector.CurrentConnector;

        public VisImageDocumentTableBehavior()
        {
            this.query = $"select * from vis_image_document";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_img_doc_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_image_document";
        }

        protected override Connector GetConnector()
        {
            return connector;
        }
    }
}
