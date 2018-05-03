namespace SupHost
{
    /// <summary>
    /// 
    /// </summary>
    class VisImageDocumentTableWrapper : AbstractTableWrapper
    {
        public VisImageDocumentTableWrapper()
        {
            this.getTableBehavior = new VisImageDocumentTableBehavior();
        }
    }
}
