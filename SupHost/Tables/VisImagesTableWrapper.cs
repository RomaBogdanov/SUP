namespace SupHost
{
    /// <summary>
    /// 
    /// </summary>
    class VisImagesTableWrapper : AbstractTableWrapper
    {
        public VisImagesTableWrapper()
        {
            this.getTableBehavior = new VisImagesTableBehavior();
        }
    }
}
