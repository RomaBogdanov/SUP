namespace SupHost
{
    class VisClientUsersTableWrapper : AbstractTableWrapper
    {
        public VisClientUsersTableWrapper()
        {
            this.getTableBehavior = new VisClientUsersTableBehavior();
        }
    }
}
