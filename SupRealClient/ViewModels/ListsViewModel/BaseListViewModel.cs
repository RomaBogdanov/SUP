using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using SupRealClient.Annotations;
using System.Runtime.CompilerServices;
using SupRealClient.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SupRealClient.Common.Interfaces;
using System.Windows;

namespace SupRealClient.ViewModels
{
    class BaseListViewModel<T> : INotifyPropertyChanged
    {

        private IBaseListModel<T> model;
        private string searchingText;
        private string okCaption;
        private Visibility zonesVisibility;

        public IBaseListModel<T> Model
        {
            get { return model; }
            set
            {
                if (model != null)
                {
                    model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                model = value;
                model.Parent = Parent;
                model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<T> Set
        {
            get { return Model?.Set; }
            set
            {
                if (Model != null) Model.Set = value;
                OnPropertyChanged();
            }
        }

        public T CurrentItem
        {
            get
            {
                return Model != null ? Model.CurrentItem : default(T);
            }
            set
            {
                if (Model != null && value != null)
                {
                    Model.CurrentItem = value;
                    OnPropertyChanged();
                }
            }
          }

        public int SelectedIndex
        {
            get { return Model != null ? Model.SelectedIndex : default(int); }
            set
            {
                if (Model != null)
                {
                    Model.SelectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public DataGridColumn CurrentColumn
        {
            get { return Model != null ? Model.CurrentColumn : null; }
            set
            {
                if (Model != null && value != null)
                {
                    Model.CurrentColumn = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OkCaption
        {
            get { return okCaption; }
            set
            {
                okCaption = value;
                OnPropertyChanged();
            }
        }

        public Visibility ZonesVisibility
        {
            get { return zonesVisibility; }
            set
            {
                zonesVisibility = value;
                OnPropertyChanged();
            }
        }

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged();
                model?.Searching(this.searchingText.ToUpper());
            }
        }

        public IWindow Parent { get; set; }
        
        public ICommand Begin { get; set; } // всегда отображается
        public ICommand Prev { get; set; }// всегда отображается
        public ICommand Next { get; set; }// всегда отображается
        public ICommand End { get; set; }// всегда отображается
        public ICommand Add { get; set; }// всегда отображается
        public ICommand Update { get; set; }// всегда отображается возможно смысл удалить
        public ICommand Search { get; set; } // всегда отображается
        public ICommand Farther { get; set; } // всегда отображается
        public ICommand Ok { get; set; } // по обстоятельствам
        public ICommand Close { get; set; } // всегда отображается
        public ICommand Zones { get; set; } // по обстоятельствам
        public ICommand Watch { get; set; } // по обстоятельствам
        public ICommand DoubleClick { get; set; } // всегда срабатывает
        public ICommand RightClick { get; set; } // всегда срабатывает

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BaseListViewModel()
        {
            Begin = new RelayCommand(obj => BeginCommand());
            Prev = new RelayCommand(obj => PrevCommand());
            Next = new RelayCommand(obj => NextCommand());
            End = new RelayCommand(obj => EndCommand());
            Add = new RelayCommand(obj => AddCommand());
            Update = new RelayCommand(obj => UpdateCommand());
            Search = new RelayCommand(obj => SearchCommand());
            Farther = new RelayCommand(obj => FartherCommand());
            Close = new RelayCommand(obj => CloseCommand());
            Ok = new RelayCommand(obj => OkCommand());
            Zones = new RelayCommand(obj => ZonesCommand());
            Watch = new RelayCommand(obj => WatchCommand());
            DoubleClick = new RelayCommand(obj => DoubleClickCommand());
            RightClick = new RelayCommand(obj => RightClickCommand());
        }

        private void AddCommand() { this.Model.Add(); }
        private void UpdateCommand() { this.Model.Update(); }
        private void SearchCommand() { this.Model.Search(); }
        private void FartherCommand() { this.Model.Farther(); }
        private void BeginCommand()
        {
            this.Model.Begin();
            Reset();
        }
        private void EndCommand()
        {
            this.Model.End();
            Reset();
        }
        private void PrevCommand()
        {
            this.Model.Prev();
            Reset();
        }
        private void NextCommand()
        {
            this.Model.Next();
            Reset();
        }
        private void OkCommand()
        {
            this.Model.Ok();
        }
        private void CloseCommand()
        {
            this.Model.Close();
        }
        private void RightClickCommand() { this.Model.RightClick(); }
        private void DoubleClickCommand() { this.Model.DoubleClick(); }
        private void WatchCommand() { this.Model.Watch(); }
        private void ZonesCommand() { this.Model.Zones(); }


        private void Reset()
        {
            SelectedIndex = Model.SelectedIndex;
            CurrentItem = Model.CurrentItem;
        }
    }

    interface IBaseListModel<T>
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
        void RightClick();
        void DoubleClick();
        void Watch();
        void Zones();

        void Searching(string pattern);
    }
}
