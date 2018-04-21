using SupHost.Connectors;
using System.Data;

namespace SupHost
{
    class VisImagesTableBehavior : BaseTableBehavior
    {
        private Connector connector = ImagesConnector.CurrentConnector;

        public VisImagesTableBehavior()
        {
            this.StandartSetup("vis_image", "f_image_id");
        }

        protected override Connector GetConnector()
        {
            return connector;
        }

        protected override void SetPrimaryKey()
        {
            DataColumn[] dcs = new DataColumn[primaryKeyColumns.Length];
            for (int i = 0; i < dcs.Length; i++)
            {
                dcs[i] = this.table.Columns[this.primaryKeyColumns[i]];
            }
            this.table.PrimaryKey = dcs;
        }
    }
}
