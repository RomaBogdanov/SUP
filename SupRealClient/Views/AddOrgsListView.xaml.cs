using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using SupRealClient.ViewModels;
using SupRealClient.Views;
using SupRealClient.TabsSingleton;
using System.Data;


namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrgsListView.xaml
    /// </summary>
    public partial class AddOrgsListView : Window
    {
        public AddOrgsListView(AddOrgsListModel model)
        {
            AddOrgsListViewModel m = new AddOrgsListViewModel(model);
            m.OnCancel += M_OnCancel;
            DataContext = m;
            InitializeComponent();
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            Orgs.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            Orgs.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            Orgs.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            Orgs.Columns.Add(dataGridTextColumn);
        }

        private void M_OnCancel()
        {
            this.Close();
        }
    }

    public class AddOrgsListViewModel : ViewModelBase
    {
        private ObservableCollection<Organization> organizations;
        private Organization currentOrganization;
        private AddOrgsListModel model;

        public event Action OnCancel;

        public AddOrgsListModel Model
        {
            get { return model; }
            set
            {
                model = value;
                model.OnCancel += Model_OnCancel;
            }
        }

        public ObservableCollection<Organization> Organizations
        {
            get { return Model.Organizations; }
        }

        public Organization CurrentOrganization
        {
            //get { return currentOrganization; }
            set
            {
                //currentOrganization = value;
                Model.CurrentOrganization = value;
                OnPropertyChanged();
            }
        }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddOrgsListViewModel(AddOrgsListModel model)
        {
            this.Model = model;
            this.OkCommand = new RelayCommand(arg => Model.Ok());
            this.CancelCommand = new RelayCommand(arg => Model.Cancel());
        }


        private void Model_OnCancel()
        {
            this.OnCancel?.Invoke();
        }
    }

    public abstract class AddOrgsListModel
    {
        public event Action OnCancel;

        public ObservableCollection<Organization> Organizations
        { get; protected set; }

        public Organization CurrentOrganization { protected get; set; }

        public abstract void Ok();

        public virtual void Cancel()
        {
            OnCancel?.Invoke();
        }
    }

    public class AddChildOrgsListModel : AddOrgsListModel
    {

        public AddChildOrgsListModel()
        {
            Organizations = new ObservableCollection<Organization>
                (from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_org_id") != 0 &
                 orgs.Field<string>("f_has_free_access")
                 .ToString().ToUpper() != "Y" & orgs.Field<int?>("f_syn_id") == 0
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     FullName = orgs.Field<string>("f_full_org_name"),
                     Name = orgs.Field<string>("f_org_name"),
                     Comment = orgs.Field<string>("f_comment")
                 });
        }

        public override void Ok()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(CurrentOrganization.Id);
            row["f_has_free_access"] = "Y";
            Cancel();
        }

    }

    public class AddBaseOrgsListModel : AddOrgsListModel
    {

        public AddBaseOrgsListModel()
        {
            Organizations = new ObservableCollection<Organization>
                (from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_org_id") != 0 &
                 orgs.Field<string>("f_is_basic")
                 .ToString().ToUpper() != "Y" & orgs.Field<int?>("f_syn_id")==0
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     FullName = orgs.Field<string>("f_full_org_name"),
                     Name = orgs.Field<string>("f_org_name"),
                     Comment = orgs.Field<string>("f_comment")
                 });
        }

        public override void Ok()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(CurrentOrganization.Id);
            row["f_is_basic"] = "Y";
            Cancel();
        }
    }
}
