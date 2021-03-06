﻿using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using System.Windows;
using System.Windows.Data;

namespace SupRealClient.Views
{
    public class Base4ViewModel<T> : INotifyPropertyChanged, IBase4ViewModel
    {
        // ==========
        private string searchingText;
        private string okCaption;
        private Visibility zonesVisibility;
        private Visibility watchVisibility;
        private Visibility okVisibility;
        private bool fartherEnabled;

        // ==========
        private IBase4Model<T> _model;

        public ICommand Add { get; set; }
        public ICommand Update { get; set; }
        public ICommand Search { get; set; }
        public ICommand Farther { get; set; }
        public ICommand Begin { get; set; }
        public ICommand Prev { get; set; }
        public ICommand Next { get; set; }
        public ICommand End { get; set; }
        public ICommand Ok { get; set; }
        public ICommand Close { get; set; }
        public ICommand Zones { get; set; }
        public ICommand Watch { get; set; }
        public ICommand RightClickCommand { get; set; }
        public ICommand Remove { get; set; }

        public IWindow Parent { get; set; }

        public string OkCaption
        {
            get { return okCaption; }
            set
            {
                okCaption = value;
                OnPropertyChanged();
            }
        }

        //todo Вот тут можно даже ручками увеличить значение, что бы заметить реакцию окна на его изменение
        public int FontSize => GlobalSettings.GetFontSize();

        /// <summary>
        /// В контексте контрола base4View соответствует видимости кнопки
        /// Зоны.
        /// </summary>
        public Visibility ZonesVisibility
        {
            get { return zonesVisibility; }
            set
            {
                zonesVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// В контексте контрола base4View соответствует видимости кнопки
        /// Просмотр.
        /// </summary>
        public Visibility WatchVisibility
        {
            get { return watchVisibility; }
            set
            {
                watchVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility OkVisibility
        {
            get { return okVisibility; }
            set
            {
                okVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// В контексте контрола base4View соответствует доступности для нажатия
        /// кнопки Далее.
        /// </summary>
        public bool FartherEnabled
        {
            get { return fartherEnabled; }
            set
            {
                this.fartherEnabled = value;
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
                FartherEnabled = _model?.Searching(
                    this.searchingText.ToUpper()) ?? false;
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

        public DataGridColumn CurrentColumn
        {
            get { return Model != null ? Model.CurrentColumn : null; }
            set
            {
                if (Model != null && value != null && value.SortDirection != null)
                {
                    Model.CurrentColumn = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<T> Set
        {
            get { return Model?.Set; }
            set
            {
                if (Model != null) Model.Set = value;
                OnPropertyChanged();
                OnPropertyChanged("CollectionView");
            }
        }

        public CollectionView CollectionView
        {
            get { return Model?.CollectionView; }
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

        public IBase4Model<T> Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                {
                    _model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                _model = value;
                Set = _model.Set;
                _model.Parent = Parent;
                _model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
            }
        }

        public Base4ViewModel()
        {
            Add = new RelayCommand(obj => AddCom());
            Update = new RelayCommand(obj => UpdateCom());
            Search = new RelayCommand(obj => SearchCom());
            Farther = new RelayCommand(obj => FartherCom());
            Begin = new RelayCommand(obj => BeginCom());
            Prev = new RelayCommand(obj => PrevCom());
            Next = new RelayCommand(obj => NextCom());
            End = new RelayCommand(obj => EndCom());
            Close = new RelayCommand(obj => CloseCom());
            Ok = new RelayCommand(obj => OkCom());
            Zones = new RelayCommand(obj => ZonesCom());
            Watch = new RelayCommand(obj => WatchCom());
            RightClickCommand = new RelayCommand(obj => RightClickCom(obj));
            Remove = new RelayCommand(obj => RemoveCom());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public System.Action ScrollCurrentItem
        {
            get { return Model != null ? Model.ScrollCurrentItem : null; }
            set
            {
                if (Model != null && value != null)
                {
                    Model.ScrollCurrentItem = value;                 
                }
            }
        }


        private void AddCom()
        {
            this.Model.Add();
            ScrollCurrentItem?.Invoke();
        }
        private void UpdateCom()
        {
            this.Model.Update();
            ScrollCurrentItem?.Invoke();
        }
        private void SearchCom()
        {
            this.Model.Search();
        }
        private void FartherCom()
        {
            this.Model.Farther();
            ScrollCurrentItem?.Invoke();
        }
        private void BeginCom()
        {
            this.Model.Begin();
            Reset();
        }
        private void EndCom()
        {
            this.Model.End();
            Reset();
        }
        private void PrevCom()
        {
            this.Model.Prev();
            Reset();
        }
        private void NextCom()
        {
            this.Model.Next();
            Reset();
        }
        private void OkCom()
        {
            if (OkVisibility == Visibility.Visible)
                this.Model.Ok();
        }
        private void CloseCom() { this.Model.Close(); }
        private void ZonesCom() { this.Model.Zones(); }
        private void WatchCom() { this.Model.Watch(); }
        private void RightClickCom(object param)
        {
            this.Model.RightClick();
        }

        /// <summary>
        /// Команда на удаление. Почему не работает???
        /// </summary>
        public void RemoveCom()
        {
            if (CurrentItem != null &&
                MessageBox.Show("Вы действительно хотите удалить эту запись?", 
                "Удаление", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                int memIndexSel = SelectedIndex;
                this.Model.Remove();

                int countItem = CollectionView.Count;
                if (countItem > 0)
                {
                    if (memIndexSel < countItem)
                        SelectedIndex = memIndexSel;
                    else
                        SelectedIndex = countItem - 1;
                }
            }
        }

        private void Reset()
        {
            SelectedIndex = Model.SelectedIndex;
            CurrentItem = Model.CurrentItem;
            ScrollCurrentItem?.Invoke();
        }
    }
}
