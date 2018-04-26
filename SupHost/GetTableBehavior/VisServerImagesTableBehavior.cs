using SupHost.Connectors;

namespace SupHost
{
    class VisServerImagesTableBehavior : BaseTableBehavior
    {
        private Connector connector = ImagesConnector.CurrentConnector;

        public VisServerImagesTableBehavior()
        {
            this.StandartSetup("vis_image", "f_image_id");
        }

        public Connector GetImageConnector()
        {
            return connector;
        }

        protected override Connector GetConnector()
        {
            return connector;
        }
    }
}
