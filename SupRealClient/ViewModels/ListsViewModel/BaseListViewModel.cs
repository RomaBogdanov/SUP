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
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.Models;
using SupRealClient.EnumerationClasses;
using SupRealClient.Search;
using SupRealClient.Common;

namespace SupRealClient.ViewModels
{
    /// <summary>
    /// Интерфейс для команд BaseListViewModel запускаемых из необобщённых классов
    /// или классов, которые не знают на базе чего было сделано обобщение.
    /// </summary>
    interface IBaseListViewModelStandartCommands
    {
        ICommand Begin { get; set; }
        ICommand Prev { get; set; }
        ICommand Next { get; set; }
        ICommand End { get; set; }
        ICommand Add { get; set; }
        ICommand Update { get; set; }
        ICommand Search { get; set; }
        ICommand Farther { get; set; }
        ICommand Ok { get; set; }
        ICommand Close { get; set; }
        ICommand Zones { get; set; }
        ICommand Watch { get; set; }
        ICommand DoubleClick { get; set; }
        ICommand RightClick { get; set; }
    }

    class BaseListViewModel<T> : INotifyPropertyChanged, 
        IBaseListViewModelStandartCommands
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

        bool Searching(string pattern);
    }

    /// <summary>
    /// Класс предназначен для обработки базовой логики всех форм со списком,
    /// полем поиска, и базовыми кнопками работы со списком (до 12 шт.)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class BaseListModel<T> : IBaseListModel<T>, ISearchHelper
    {
        
        public event ModelPropertyChanged OnModelPropertyChanged;
        public event Action<object> OnClose;

        protected ObservableCollection<T> set;
        protected T currentItem;
        protected int selectedIndex;
        protected SearchResult searchResult = new SearchResult();

        public DataGridColumn CurrentColumn { get; set; }

        public IWindow Parent { get; set; }

        public virtual DataRow[] Rows
        { get { return Table.AsEnumerable().ToArray(); } }

        protected virtual DataTable Table { get; }

        protected virtual IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Множество, которое выводится в список.
        /// </summary>
        public virtual ObservableCollection<T> Set
        {
            get { return set; }
            set
            {
                set = value;
                OnModelPropertyChanged?.Invoke("Set");
            }
        }

        /// <summary>
        /// Выделенный объект списка с которым идёт работа.
        /// </summary>
        public virtual T CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        /// <summary>
        /// Индекс выделенного объекта списка, с которым идёт работа.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }
        
        /// <summary>
        /// Процедура обработки нажатия кнопки Начало
        /// </summary>
        public virtual void Begin()
        {
            if (Set.Count > 0)
            {
                SelectedIndex = 0;
                CurrentItem = Set[SelectedIndex];
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Процедура обработки нажатия кнопки Конец
        /// </summary>
        public virtual void End()
        {
            if (Set.Count > 0)
            {
                SelectedIndex = Set.Count - 1;
                CurrentItem = Set[SelectedIndex];
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Процедура обработки нажатия кнопки Пред.
        /// </summary>
        public virtual void Prev()
        {
            if (Set.Count > 0)
            {
                if (SelectedIndex > 0)
                {
                    SelectedIndex--;
                    CurrentItem = Set[SelectedIndex];
                }
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Процедура обработки нажатия кнопки След.
        /// </summary>
        public virtual void Next()
        {
            if (Set.Count > 0)
            {
                if (SelectedIndex < Set.Count - 1)
                {
                    SelectedIndex++;
                    CurrentItem = Set[SelectedIndex];
                }
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Процедура обработки нажатия кнопки Добавить
        /// </summary>
        public abstract void Add();

        /// <summary>
        /// Процедура обработки нажатия кнопки Отредактировать
        /// (при определённых вариантах Удалить)
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Процедура обработки нажатия кнопки Далее
        /// </summary>
        public virtual void Farther()
        {
            SetAt(searchResult.Next());
        }

        /// <summary>
        /// Отрабатывает нажатие кнопки поиска
        /// </summary>
        public virtual void Search()
        {
            ViewManager.Instance.Search(this, Parent);
        }

        /// <summary>
        /// Процедура обработки кнопки Ок
        /// </summary>
        public virtual void Ok()
        {
            OnClose?.Invoke(GetResult());
        }

        /// <summary>
        /// Процедура обработки кнопки Закрыть
        /// </summary>
        public virtual void Close()
        {
            OnClose?.Invoke(null);
        }

        /// <summary>
        /// Процедура обработки нажатия правой кнопки мыши
        /// </summary>
        public virtual void RightClick() { }

        /// <summary>
        /// TODO: пока не совсем понятно.
        /// Возможно процедура получения результата.
        /// </summary>
        /// <returns></returns>
        protected virtual BaseModelResult GetResult()
        { return null; }

        /// <summary>
        /// Процедура, которая должна будет обработать двойное нажатие ЛКМ
        /// </summary>
        public virtual void DoubleClick() { }

        /// <summary>
        /// TODO: вообще непонятно, что это.
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// TODO: непонятно.
        /// Скорее всего, индекс строки.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual long GetId(int index)
        {
            return -1;
        }

        /// <summary>
        /// TODO: непонятно.
        /// </summary>
        /// <param name="id"></param>
        public virtual void SetAt(long id)
        {
            for (int i = 0; i < Set.Count(); i++)
            {
                if ((Set.ElementAt(i) as IdEntity).Id == id)
                {
                    CurrentItem = Set.ElementAt(i);
                    OnModelPropertyChanged?.Invoke("CurrentItem");
                    break;
                }
            }
        }

        /// <summary>
        /// TODO: непонятно.
        /// Скорее всего, поиск.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public virtual bool Searching(string pattern)
        {
            searchResult = new SearchResult();
            if (CurrentColumn == null || string.IsNullOrEmpty(pattern) ||
                !GetColumns().ContainsKey(CurrentColumn.SortMemberPath))
            {
                return false;
            }
            string path = GetColumns()[CurrentColumn.SortMemberPath];
            for (int i = 0; i < Rows.Length; i++)
            {
                object obj = Rows[i].Field<object>(path);
                if (CommonHelper.IsSearchConditionMatch(obj.ToString(), pattern))
                {
                    searchResult.Add(GetId(i));
                }
            }
            SetAt(searchResult.Begin());

            return searchResult.Any();
        }

        /// <summary>
        /// Обработка кнопки Просмотр
        /// </summary>
        public virtual void Watch() { }

        /// <summary>
        /// Обработка кнопки Зоны
        /// </summary>
        public virtual void Zones() { }

        protected void Query()
        {
            int oldIndex = SelectedIndex;

            DoQuery();

            if (oldIndex >= 0 && oldIndex < Set.Count - 1)
            {
                SelectedIndex = oldIndex;
                CurrentItem = Set[SelectedIndex];
            }
            else if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
            OnModelPropertyChanged?.Invoke("CurrentItem");
        }

        protected virtual void DoQuery() { }

    }

    class OrgsSample<T>: BaseListModel<T>
        where T: Organization, new()
    {
        public OrgsSample()
        {
            Query();
        }

        public override void Add()
        {
            System.Windows.Forms.MessageBox.Show("Add");
        }

        public override void Update()
        {
            System.Windows.Forms.MessageBox.Show("Update");
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
    where orgs.Field<int>("f_org_id") != 0
    select new T
    {
        Id = orgs.Field<int>("f_org_id"),
        Type = orgs.Field<string>("f_org_type"),
        Name = OrganizationsHelper.UntrimName(
            orgs.Field<string>("f_org_name")),
        FullName = OrganizationsHelper.
            GenerateFullName(orgs.Field<int>("f_org_id")),
        Comment = orgs.Field<string>("f_comment"),
        CountryId = orgs.Field<int>("f_cntr_id"),
        Country = orgs.Field<int>("f_cntr_id") == 0 ?
            "" : CountriesWrapper.CurrentTable()
            .Table.AsEnumerable().FirstOrDefault(
            arg => arg.Field<int>("f_cntr_id") ==
            orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
        RegionId = orgs.Field<int>("f_region_id"),
        Region = orgs.Field<int>("f_region_id") == 0 ?
            "" : RegionsWrapper.CurrentTable()
            .Table.AsEnumerable().FirstOrDefault(
            arg => arg.Field<int>("f_region_id") ==
            orgs.Field<int>("f_region_id"))["f_region_name"].ToString(),
        SynId = orgs.Field<int>("f_syn_id")
    });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult
            {
                Id = CurrentItem.Id,
                Name = CurrentItem.FullName
            };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "f_org_type", "Тип" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
            };
        }

        protected override DataTable Table
        {
            get
            {
                return OrganizationsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>
            {
                { "Type", "f_org_type" },
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }
    }
}
