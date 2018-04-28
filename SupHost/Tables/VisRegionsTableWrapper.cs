namespace SupHost
{
    class VisRegionsTableWrapper : AbstractTableWrapper
    {
        public VisRegionsTableWrapper()
        {
            this.getTableBehavior = new VisRegionsTableBehavior();
        }
    }
}
