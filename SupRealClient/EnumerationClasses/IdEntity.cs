using System;
using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid.Converters;

namespace SupRealClient.EnumerationClasses
{
    public abstract class IdEntity : IEntity
    {
        private int id;
        private bool isDeleted = false;

        public event Action OnChangeId;

        public int Id
        {
            get { return id; }

            set
            {
                id = value;
                OnChangeId?.Invoke();
            }
        }

        public bool IsDeleted
        {
            get { return isDeleted; }
            set
            {
                isDeleted = value;
            }
        }
    }
}
