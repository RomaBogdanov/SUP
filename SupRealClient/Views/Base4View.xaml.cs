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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4View.xaml
    /// </summary>
    public partial class Base4View : UserControl
    {
        public Base4View()
        {
            InitializeComponent();
        }
    }

    public class Base4ViewModel<T>
    {
        private IBase4Model<T> _model;
        private T currentItem;

        public ICommand Add { get; set; }
        public ICommand Update { get; set; }
        public ICommand Search { get; set; }
        public ICommand Farther { get; set; }
        public ICommand Begin { get; set; }
        public ICommand Prev { get; set; }
        public ICommand Next { get; set; }
        public ICommand End { get; set; }
        public ICommand Close { get; set; }

        public T CurrentItem
        {
            get
            {
                return Model != null ? Model.CurrentItem : default(T);
            }
            set
            {
                //this.currentItem = value;
                if (Model != null)
                {
                    Model.CurrentItem = value;
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
            }
        }

        public IBase4Model<T> Model
        {
            get { return _model; }
            set
            {
                _model = value;
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddCom() { this.Model.Add(); }
        private void UpdateCom() { this.Model.Update(); }
        private void SearchCom() { this.Model.Search(); }
        private void FartherCom() { this.Model.Farther(); }
        private void BeginCom() { this.Model.Begin(); }
        private void PrevCom() { this.Model.Prev(); }
        private void NextCom() { this.Model.Next(); }
        private void EndCom() { this.Model.End(); }
        private void CloseCom() { this.Model.Close(); }
    }

    public interface IBase4Model<T>
    {
        ObservableCollection<T> Set { get; set; }
        T CurrentItem { get; set; }

        void Add();
        void Begin();
        void Close();
        void End();
        void Farther();
        void Next();
        void Prev();
        void Search();
        void Update();
    }

    public class VisitorsListModel<T> : IBase4Model<T>
        where T : SupRealClient.EnumerationClasses.Visitor, new()
    {
        private ObservableCollection<T> set;
        private T currentItem;

        public VisitorsListModel()
        {
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
        }

        public ObservableCollection<T> Set
        {
            get { return set; }
            set { set = value; }
        }

        public T CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        public void Add()
        {
            //System.Windows.Forms.MessageBox.Show("Add");
            Visitor.AddVisitorView wind = new Visitor.AddVisitorView();
            wind.Show();
        }

        public void Begin()
        {
            System.Windows.Forms.MessageBox.Show("Begin");
        }

        public void Close()
        {
            System.Windows.Forms.MessageBox.Show("Close");
        }

        public void End()
        {
            System.Windows.Forms.MessageBox.Show("End");
        }

        public void Farther()
        {
            System.Windows.Forms.MessageBox.Show("Farther");
        }

        public void Next()
        {
            System.Windows.Forms.MessageBox.Show("Next");
        }

        public void Prev()
        {
            System.Windows.Forms.MessageBox.Show("Prev");
        }

        public void Search()
        {
            System.Windows.Forms.MessageBox.Show("Search");
        }

        public void Update()
        {
            System.Windows.Forms.MessageBox.Show("Update");
        }

        private void Query()
        {
            set = new ObservableCollection<T>(
                from visitors in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                where visitors.Field<int>("f_visitor_id") != 0
                select new T
                {
                    Id = visitors.Field<int>("f_visitor_id"),
                    FullName = visitors.Field<string>("f_full_name"),
                    Organization = (string)OrganizationsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") == 
                        visitors.Field<int>("f_org_id"))["f_full_org_name"],
                    Comment = visitors.Field<string>("f_vr_text")
                }
                );
        }
    }
}
