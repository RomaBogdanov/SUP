﻿using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    public interface IBase4Model<T>
    {
        event ModelPropertyChanged OnModelPropertyChanged;
        event Action<object> OnClose;

        ObservableCollection<T> Set { get; set; }
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
        void Remove();

        bool Searching(string pattern);
    }
}
