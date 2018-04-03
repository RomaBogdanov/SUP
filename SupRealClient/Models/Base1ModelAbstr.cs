﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Models
{
    public abstract class Base1ModelAbstr : ISearchHelper
    {
        protected DataTable table;
        protected ClientConnector tabConnector;
        protected string tabName;
        protected IBase1ViewModel viewModel;

        public event Action OnClose;
        public abstract void EnterCurrentItem(object item);
        public abstract void Add();
        public abstract void Update();

        public virtual void Search()
        {
            ViewManager.Instance.Search(this);
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
        public virtual void SetAt(int index)
        {
            if (index >= 0 && index < this.viewModel.Set.Count())
            {
                this.viewModel.SelectedIndex = index;
            }
        }
        protected abstract void Query();
    }
}
