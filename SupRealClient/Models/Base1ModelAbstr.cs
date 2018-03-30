using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    public abstract class Base1ModelAbstr
    {
        protected DataTable table;
        protected ClientConnector tabConnector;
        protected string tabName;
        protected Base1ViewModel viewModel;

        public event Action OnClose;
        public abstract void EnterCurrentItem(object item);
        public abstract void Add();
        public abstract void Update();
        public virtual void Search()
        {
            Search1View search1View = new Search1View();
            search1View.Show();
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
        protected abstract void Query();
    }
}
