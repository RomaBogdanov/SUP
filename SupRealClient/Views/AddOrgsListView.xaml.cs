using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Models;
using SupRealClient.Common.Interfaces;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using SupClientConnectionLib;
using SupRealClient.Search;
using SupRealClient.Common;
using SupRealClient.Annotations;
using System.Runtime.CompilerServices;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrgsListView.xaml
    /// </summary>
    public partial class AddOrgsListView : IWindow
    {
        int memCountRows = 0;
        DataGridColumnHeader headerCliked = null;

        public AddOrgsListView(AddOrgsListModel model)
        {
            AddOrgsListViewModel viewModel = new AddOrgsListViewModel(model);
            viewModel.Model.OnClose += Handling_OnClose;
            this.DataContext = viewModel;

            InitializeComponent();

            baseTab.SelectionChanged -= baseTab_SelectionChanged;
            tbxSearch.Focus();
            AfterInitialize();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
        }

        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                baseTab.SelectionChanged += baseTab_SelectionChanged;
                btnUp.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                baseTab.SelectionChanged += baseTab_SelectionChanged;
                btnDown.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (btnOk.Visibility == Visibility.Visible && btnOk.IsEnabled)
                {
                    btnOk.Command?.Execute(null);
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Insert)
            {
                ((ISuperBaseViewModel)DataContext).Add.Execute(null);
            }
            else if (e.Key == Key.Home)
            {
                ((ISuperBaseViewModel)DataContext).Begin.Execute(null);
            }
            else if (e.Key == Key.End)
            {
                ((ISuperBaseViewModel)DataContext).End.Execute(null);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Hidden)
            {
                tbxSearch.Text = string.Empty;
                baseTab.SelectedItems?.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;

                if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
                    SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
                else if (baseTab.Columns.Count > 0)
                    SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
            }
        }

        static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            if (dataGrid.Items.Count > 0)
                dataGrid.SelectedItem = dataGrid.Items[0];

            dataGrid.CurrentColumn = dataGrid.Columns[columnIndex];

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }

        private void baseTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baseTabCurrentItemScrollIntoView();
            baseTab.SelectionChanged -= baseTab_SelectionChanged;
        }

        void baseTabCurrentItemScrollIntoView()
        {
            if (baseTab.CurrentItem != null)
            {
                baseTab.ScrollIntoView(baseTab.CurrentItem);
                baseTab.UpdateLayout();
                baseTab.ScrollIntoView(baseTab.CurrentItem);
            }
        }

        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
                SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
            else if (baseTab.Columns.Count > 0)
                SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
        }

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (memCountRows + 1 == baseTab.Items.Count)
            {
                memCountRows = 0;
                baseTab.SelectedItems.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;
                baseTab.SelectedItem = baseTab.Items[baseTab.Items.Count - 1];
            }
        }

        private void BaseTab_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Down && e.Key != Key.Up)
            {
                SelectSearchBox();
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnOk.Command?.Execute(null);
            e.Handled = true;
        }

        public void SelectSearchBox()
        {
            tbxSearch.Focus();
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Name == "butAdd")
                memCountRows = baseTab.Items.Count;
            else
                baseTab.SelectionChanged += baseTab_SelectionChanged;
        }

        private void dgColumnHeader_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            headerCliked = sender as DataGridColumnHeader;
        }

        private void baseTab_Sorted(object sender, RoutedEventArgs e)
        {
            if (headerCliked != null)
            {
                baseTab.CurrentColumn = headerCliked.Column;

                if (!string.IsNullOrEmpty(tbxSearch.Text))
                {
                    tbxSearch.Text = tbxSearch.Text;
                    baseTabCurrentItemScrollIntoView();
                }

                headerCliked = null;
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseTabCurrentItemScrollIntoView();
        }

        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddOrgsListView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void M_OnCancel()
        {
            this.Close();
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Регион",
                Binding = new Binding("Region")
            };
            this.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            this.baseTab.CurrentColumn = this.baseTab.Columns[0];
        }
    }

    public class AddOrgsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected AddOrgsListModel model;
        private IEnumerable<object> set;
        private object currentItem;
        private DataGridCellInfo currentCell;
        private DataGridColumn currentColumn;
        private object selectedValue;
        private bool focused = false;
        private int selectedIndex;
        private string searchingText;
        private bool fartherEnabled;

        public int NumItem { get; set; } = -1;

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged("SearchingText");
                FartherEnabled = this.model?.
                    Searching(this.searchingText.ToUpper()) ?? false;
            }
        }

        public AddOrgsListModel Model { get { return model; } }

        /// <summary>
        /// Размер шрифта
        /// </summary>
        public int FontSize => GlobalSettings.GetFontSize();

        public IEnumerable<object> Set
        {
            get { return this.set; }
            set
            {
                this.set = value;
                OnPropertyChanged("Set");
            }
        }

        public object SelectedValue
        {
            get { return this.selectedValue; }
            set
            {
                this.selectedValue = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        public object CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (value != null)
                {
                    this.currentItem = value;
                    model?.EnterCurrentItem(this.currentItem);
                    OnPropertyChanged("CurrentItem");
                }
            }
        }

        public DataGridColumn CurrentColumn
        {
            get { return this.currentColumn; }
            set
            {
                if (value != null)
                {
                    this.currentColumn = value;
                    OnPropertyChanged("CurrentColumn");
                }
            }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                this.selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public bool Focused
        {
            get { return focused; }
            set
            {
                this.focused = value;
                OnPropertyChanged("Focused");
            }
        }

        public bool FartherEnabled
        {
            get { return fartherEnabled; }
            set
            {
                this.fartherEnabled = value;
                OnPropertyChanged("FartherEnabled");
            }
        }

        public ICommand Add { get; set; }
        public ICommand Update { get; set; }
        public ICommand Search { get; set; }
        public ICommand Farther { get; set; }
        public ICommand Begin { get; set; }
        public ICommand Prev { get; set; }
        public ICommand Next { get; set; }
        public ICommand End { get; set; }
        public ICommand Close { get; set; }
        public ICommand Ok { get; set; }

        public AddOrgsListViewModel(AddOrgsListModel model)
        {
            this.model = model;
            this.model.SetViewModel(this);
            this.model.Query();
            this.Add = new RelayCommand(arg => model.Add());
            this.Update = new RelayCommand(arg => model.Update());
            this.Search = new RelayCommand(arg => model.Search());
            this.Farther = new RelayCommand(arg => model.Farther());
            this.Begin = new RelayCommand(arg => model.Begin());
            this.Prev = new RelayCommand(arg => model.Prev());
            this.Next = new RelayCommand(arg => model.Next());
            this.End = new RelayCommand(arg => model.End());
            this.Close = new RelayCommand(arg => model.Close());
            this.Ok = new RelayCommand(arg => model.Ok());
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class AddOrgsListModel : ISearchHelper
    {
        protected DataTable table;
        protected ClientConnector tabConnector;
        protected string tabName;
        protected AddOrgsListViewModel viewModel;
        protected IWindow parent;
        protected SearchResult searchResult = new SearchResult();

        public event Action OnClose;
        public abstract void EnterCurrentItem(object item);
        public abstract void Add();
        public abstract void Update();

        public void SetViewModel(AddOrgsListViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public virtual void Search()
        {
            ViewManager.Instance.Search(this, parent);
        }

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

        public virtual void Farther()
        {
            SetAt(searchResult.Next());
        }

        public virtual bool Searching(string pattern)
        {
            searchResult = new SearchResult();
            if (viewModel.CurrentColumn == null || string.IsNullOrEmpty(pattern) ||
                !GetColumns().ContainsKey(viewModel.CurrentColumn.SortMemberPath))
            {
                return false;
            }
            string path = GetColumns()[viewModel.CurrentColumn.SortMemberPath];
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
        public abstract void Query();
        protected abstract IDictionary<string, string> GetColumns();

        ///

        public ObservableCollection<Organization> Organizations
        { get; protected set; }

        public Organization CurrentOrganization { protected get; set; }

        public abstract void Ok();
    }

    public class AddChildOrgsListModel : AddOrgsListModel
    {
        public AddChildOrgsListModel(IWindow parent)
        {
            this.parent = parent;
            OrganizationsWrapper countriesWrapper = OrganizationsWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
        }

        public override void Ok()
        {
            int currentId = (viewModel.SelectedValue as IdEntity).Id;

            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(currentId);
            row["f_has_free_access"] = "Y";
            Close();

            // TODO - установка CurrentItem - работает, но возможно потом лучше переделать
            Base4ViewModel<Organization> OrgViewModel = (parent as Base4ChildOrgsWindView)?.base4.DataContext as Base4ViewModel<Organization>;
            if (OrgViewModel != null)
            {
                OrgViewModel.CurrentItem = OrgViewModel.Set.FirstOrDefault(
                                                                r =>
                                                                    r.Id == currentId);
            }
        }

        public override void Add()
        {
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Organization).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (this.viewModel.CurrentItem as Organization).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Organization).Id;
        }

        public override void Update()
        {
            ViewManager.Instance.UpdateObject(new UpdateOrgsModel((Organization)this.viewModel.CurrentItem), parent);
        }

        public override void Query()
        {
            Organizations = new ObservableCollection<Organization>
                  (from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                   where orgs.Field<int>("f_org_id") != 0 &
                   orgs.Field<string>("f_has_free_access")
                   .ToString().ToUpper() != "Y" & 
                   orgs.Field<int?>("f_syn_id") == 0 &
                   orgs.Field<string>("f_is_basic")
                   .ToString().ToUpper() != "Y"
                   select new Organization
                   {
                       Id = orgs.Field<int>("f_org_id"),
                       Type = orgs.Field<string>("f_org_type"),
                       FullName = OrganizationsHelper.
                          GenerateFullName(orgs.Field<int>("f_org_id")),
                       Name = OrganizationsHelper.UntrimName(
                           orgs.Field<string>("f_org_name")),
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
                       SynId = 0,
					   IsBasic = true
				   });
            this.viewModel.Set = new ObservableCollection<object>(Organizations);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = Organizations.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        // TODO - переделать без повторов кода
        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in table.AsEnumerable() where orgs.Field<int>("f_org_id") != 0 select orgs).AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_org_type", "Тип" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_org_id");
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "Type", "f_org_type" },
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }

    }

    public class AddBaseOrgsListModel : AddOrgsListModel
    {
        public AddBaseOrgsListModel(IWindow parent)
        {
            this.parent = parent;
            OrganizationsWrapper countriesWrapper = OrganizationsWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
        }

        public override void Ok()
        {
            OrganizationsWrapper organizations =
                 OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find((viewModel.SelectedValue as IdEntity).Id);
            row["f_is_basic"] = "Y";
            Close();
        }

        public override void Add()
        {
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Organization).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (this.viewModel.CurrentItem as Organization).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Organization).Id;
        }

        public override void Update()
        {
            ViewManager.Instance.UpdateObject(new UpdateOrgsModel((Organization)this.viewModel.CurrentItem), parent);
        }

        public override void Query()
        {
            Organizations = new ObservableCollection<Organization>
                (from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_org_id") != 0 &
                 orgs.Field<string>("f_is_basic")
                 .ToString().ToUpper() != "Y" & 
                 orgs.Field<int?>("f_syn_id") == 0 &
                 orgs.Field<string>("f_has_free_access")
                   .ToString().ToUpper() != "Y"
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     FullName = OrganizationsHelper.
                        GenerateFullName(orgs.Field<int>("f_org_id")),
                     Name = OrganizationsHelper.UntrimName(
                         orgs.Field<string>("f_org_name")),
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
                     SynId = 0,
					 IsBasic = true
				 });
            this.viewModel.Set = new ObservableCollection<object>(Organizations);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = Organizations.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        // TODO - переделать без повторов кода
        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in table.AsEnumerable() where orgs.Field<int>("f_org_id") != 0 select orgs).AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_org_id");
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "Type", "f_org_type" },
                { "FullName", "f_full_org_name"},
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }
    }
}
