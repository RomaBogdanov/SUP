namespace SupHost
{
    class VisClientLogsTableWrapper : AbstractTableWrapper
    {
        public VisClientLogsTableWrapper()
        {
            this.getTableBehavior = new VisClientLogsTableBehavior();
        }
    }
}
