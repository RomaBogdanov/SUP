using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SupRealClient.Common.Interfaces;
using System.Windows.Data;

namespace SupRealClient.Views
{
    public interface IBase4Model<T>
    {
        event ModelPropertyChanged OnModelPropertyChanged;
        event Action<object> OnClose;

        Action ScrollCurrentItem { get; set; }

        ObservableCollection<T> Set { get; set; }
        CollectionView CollectionView { get; }
        T CurrentItem { get; set; }
        int SelectedIndex { get; set; }
        DataGridColumn CurrentColumn { get; set; }
        IWindow Parent { get; set; }

        void Add();
        void Begin();
        void Ok();
        void Close();
        void End();
        void Farther();
        void Next();
        void Prev();
        void Search();
        void Update();
        void Zones();
        void Watch();
        void RightClick();
        bool Remove();

        bool Searching(string pattern);
    }
}
