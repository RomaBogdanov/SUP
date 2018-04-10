using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Models
{
    public abstract class Base1ModelAbstr : ISearchHelper
    {
        protected DataTable table;
        protected ClientConnector tabConnector;
        protected string tabName;
        protected IBase1ViewModel viewModel;
        protected IWindow parent;

        public event Action OnClose;
        public abstract void EnterCurrentItem(object item);
        public abstract void Add();
        public abstract void Update();

        public virtual void Search()
        {
            ViewManager.Instance.Search(this, parent);
        }

        public abstract void Farther();
        public abstract void Begin();
        public virtual void Prev()
        {
            if (this.viewModel.SelectedIndex > 0)
            {
                this.viewModel.SelectedIndex--;
            }
        }

        public virtual void Next()
        {
            if (this.viewModel.SelectedIndex < this.viewModel.Set.Count() - 1)
            {
                this.viewModel.SelectedIndex++;
            }
        }

        public abstract void End();
        public abstract void Searching(string pattern);
        public virtual void Close()
        {
            OnClose?.Invoke();
        }
        public abstract IDictionary<string, string> GetFields();
        public virtual DataRow[] Rows { get { return table.AsEnumerable().ToArray(); } }
        public virtual void SetAt(long id)
        {
            for (int i = 0; i < this.viewModel.Set.Count(); i++)
            {
                if ((this.viewModel.Set.ElementAt(i) as IdEntity).Id == id)
                {
                    this.viewModel.SelectedValue = this.viewModel.Set.ElementAt(i);
                    break;
                }
            }
        }
        public abstract long GetId(int index);
        protected abstract void Query();
    }
}
