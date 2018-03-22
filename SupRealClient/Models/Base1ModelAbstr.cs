using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    public abstract class Base1ModelAbstr
    {
        public event Action OnClose;
        public abstract void EnterCurrentItem(object item);
        public abstract void Add();
        public abstract void Update();
        public abstract void Search();
        public abstract void Farther();
        public abstract void Begin();
        public abstract void Prev();
        public abstract void Next();
        public abstract void End();
        public abstract void Searching(string pattern);
        public virtual void Close()
        {
            OnClose?.Invoke();
        }
    }
}
