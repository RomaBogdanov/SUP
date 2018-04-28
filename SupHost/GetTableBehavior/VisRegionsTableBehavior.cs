namespace SupHost
{
    class VisRegionsTableBehavior : VisitorsDBTableBehavior
    {
        public VisRegionsTableBehavior()
        {
            this.query = "select * from vis_regions";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_region_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_regions";
        }
    }
}
