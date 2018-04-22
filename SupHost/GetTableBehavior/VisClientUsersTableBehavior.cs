namespace SupHost
{
    class VisClientUsersTableBehavior : VisitorsDBTableBehavior
    {
        public VisClientUsersTableBehavior()
        {
            this.query = "select f_user_id, f_user, f_timeout from vis_new_user";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_user_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_new_user";
        }
    }
}
