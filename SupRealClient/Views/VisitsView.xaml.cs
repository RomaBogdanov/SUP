using System;
using System.Linq;
using System.Windows.Input;
using SupRealClient.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using SupContract;
using System.IO;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for VisitsView.xaml
    /// </summary>
    public partial class VisitsView
    {
        public VisitsView()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Visit)
            {
                var visit = (Visit) sender;

                visit.ToString();
            }
        }
    }

    public class VisitsViewModel : INotifyPropertyChanged
    {
        private IVisitsModel model;
        private IWindow parentWindow;

        public IVisitsModel Model
        {
            get { return model; }
            set
            {
                if (model != null)
                {
                    model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                model = value;
                model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
                CurrentItem = model.CurrentItem;
                Set = model.Set;
            }
        }

        private VisitorsEnableOrVisible _visitorsEnable;
        /// <summary>
        /// Объект со списком свойств Enable для кнопок
        /// </summary>
        public VisitorsEnableOrVisible VisitorsEnable
        {
            get { return _visitorsEnable; }
            set
            {
                _visitorsEnable = value;
                OnPropertyChanged();
            }
        }

        private VisitorsEnableOrVisible _visitorsVisible;
        /// <summary>
        /// Объект со списком свойтсв Visible для кнопок
        /// </summary>
        public VisitorsEnableOrVisible VisitorsVisible
        {
            get { return _visitorsVisible; }
            set
            {
                _visitorsVisible = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<EnumerationClasses.Visitor> Set
        {
            get { return Model?.Set; }
            set
            {
                if (Model != null)
                {
                    Model.Set = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnumerationClasses.Visitor CurrentItem
        {
            get
            {
                 return Model?.CurrentItem;
            }
            set
            {
                if (Model != null)
                {
                    Model.CurrentItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhotoSource
        {
            get { return Model?.PhotoSource; }
        }

        public string Signature
        {
            get { return Model?.Signature; }
        }

        public bool Enable
        { get; set; }

        public ICommand BeginCommand { get; set; }
        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand EndCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand OrganizationCommand { get; set; }
        public ICommand CountryCommand { get; set; }
        public ICommand CabinetsCommand { get; set; }
        public ICommand DocumentsCommand { get; set; }
        public ICommand AddImageSourceCommand { get; set; }
        public ICommand RemoveImageSourceCommand { get; set; }
        public ICommand AddSignatureCommand { get; set; }
        public ICommand RemoveSignatureCommand { get; set; }

        public VisitsViewModel(IWindow parentWindow)
        {
            this.parentWindow = parentWindow;
            Model = new VisitsModel();

            OnPropertyChanged("PhotoSource");
            OnPropertyChanged("Signature");

            BeginCommand = new RelayCommand(arg => Begin());
            PrevCommand = new RelayCommand(arg => Prev());
            NextCommand = new RelayCommand(arg => Next());
            EndCommand = new RelayCommand(arg => End());
            NewCommand = new RelayCommand(arg => New());
            OrganizationCommand = new RelayCommand(arg => OrganizationsList());
            CountryCommand = new RelayCommand(arg => CountyList());
            CabinetsCommand = new RelayCommand(arg => CabinetsList());
            DocumentsCommand = new RelayCommand(arg => DocumentsListModel());
            AddImageSourceCommand = new RelayCommand(arg => AddImageSource(ImageType.Photo));
            RemoveImageSourceCommand= new RelayCommand(arg => RemoveImageSource(ImageType.Photo));
            AddSignatureCommand = new RelayCommand(arg => AddImageSource(ImageType.Signature));
            RemoveSignatureCommand = new RelayCommand(arg => RemoveImageSource(ImageType.Signature));
        }

        private void DocumentsListModel()
        {
            ViewManager.Instance.OpenWindow("Base4DocumentsWindView", parentWindow);
        }

        private void CabinetsList()
        {
            ViewManager.Instance.OpenWindow("Base4CabinetsWindView", parentWindow);
        }

        private void OrganizationsList()
        {
            ViewManager.Instance.OpenWindow("Base4OrganizationsWindView", parentWindow);
        }

        private void CountyList()
        {
            ViewManager.Instance.OpenWindow("Base4NationsWindView", parentWindow);
        }

        private void Begin()
        {
            CurrentItem = Model.Begin();
        }

        private void Prev()
        {
            CurrentItem = Model.Prev();
        }

        private void Next()
        {
            CurrentItem = Model.Next();
        }

        private void End()
        {
            CurrentItem = Model.End();
        }

        private void New()
        {
            Model = new NewVisitsModel();
        }

        private void AddImageSource(ImageType imageType)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Model.AddImageSource(dlg.FileName, imageType);

                //PhotoSource = image;
                //System.Windows.MessageBox.Show("Картинка загружена");
            }
        }

        private void RemoveImageSource(ImageType imageType)
        {
            Model.RemoveImageSource(imageType);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IVisitsModel
    {
        event ModelPropertyChanged OnModelPropertyChanged;

        string PhotoSource { get; }
        string Signature { get; }

        ObservableCollection<EnumerationClasses.Visitor> Set { get; set; }
        EnumerationClasses.Visitor CurrentItem { get; set; }

        EnumerationClasses.Visitor Begin();
        EnumerationClasses.Visitor End();
        EnumerationClasses.Visitor Next();
        EnumerationClasses.Visitor Prev();

        void AddImageSource(string path, ImageType imageType);
        void RemoveImageSource(ImageType imageType);
    }

    public class VisitsModel : IVisitsModel
    {
        private const string Images = "Images";

        public event ModelPropertyChanged OnModelPropertyChanged;

        private ObservableCollection<EnumerationClasses.Visitor> set;
        private EnumerationClasses.Visitor currentItem;
        private int selectedIndex;

        public string PhotoSource { get; private set; }
        public string Signature { get; private set; }

        public ObservableCollection<EnumerationClasses.Visitor> Set
        {
            get { return set; }
            set { set = value; }
        }

        public EnumerationClasses.Visitor CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                GetPhoto();
                GetSign();
            }
        }

        public VisitsModel()
        {
            if (!Directory.Exists(Images))
            {
                Directory.CreateDirectory(Images);
            }
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            ImagesWrapper.CurrentTable().OnChanged += OnImageChanged;
            Query();
        }

        private void Query()
        {
            Set = new ObservableCollection<EnumerationClasses.Visitor>(
                from visitors in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                where visitors.Field<int>("f_visitor_id") != 0
                select new EnumerationClasses.Visitor
                {
                    Id = visitors.Field<int>("f_visitor_id"),
                    FullName = visitors.Field<string>("f_full_name"),
                    Family = visitors.Field<string>("f_family"),
                    Name = visitors.Field<string>("f_fst_name"),
                    Patronymic = visitors.Field<string>("f_sec_name"),
                    Organization = (string)OrganizationsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") ==
                        visitors.Field<int>("f_org_id"))?["f_full_org_name"],
                    Comment = visitors.Field<string>("f_vr_text"),
                    IsAccessDenied = visitors.Field<string>(
                        "f_persona_non_grata").ToUpper() == "Y" ? true : false,
                    IsCanHaveVisitors = visitors.Field<string>(
                        "f_can_sign_orders").ToUpper() == "Y" ? true : false,
                    IsNotFormular = true,
                    Telephone = visitors.Field<string>("f_phones"),
                    Nation = (string)CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_cntr_id") ==
                        visitors.Field<int>("f_cntr_id"))?["f_cntr_name"],
                    DocType = (string)DocumentsWrapper.CurrentTable().Table
                        .AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_doc_id") ==
                        visitors.Field<int>("f_doc_id"))?["f_doc_name"],
                    DocSeria = visitors.Field<string>("f_doc_seria"),
                    DocNum = visitors.Field<string>("f_doc_num"),
                    DocDate = visitors.Field<DateTime>("f_doc_date"),
                    DocPlace = visitors.Field<string>("f_doc_org"),
                    IsAgree = true,
                    AgreeToDate = DateTime.Now,
                    Operator = visitors.Field<int>("f_rec_operator").ToString(),
                    Department = (string)DepartmentWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_dep_id") ==
                        visitors.Field<int>("f_dep_id"))?["f_dep_name"],
                    DepartmentUnit = (string)DepartmentSectionWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_section_id") ==
                        visitors.Field<int>("f_section_id"))?["f_section_name"],
                    Position = visitors.Field<string>("f_job"),
                    IsRightSign = visitors.Field<string>("f_can_sign_orders")
                        .ToUpper() == "Y" ? true :false,
                    IsAgreement = visitors.Field<string>("f_can_adjust_orders")
                        .ToUpper() == "Y" ? true : false,
                    Cabinet = (string)CabinetsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_cabinet_id") ==
                        visitors.Field<int>("f_cabinet_id"))?["f_cabinet_desc"],
                });
            if (Set.Count > 0)
            {
                OrdersCardsToVisitor(0);
                CurrentItem = Set[0];
            }
        }

        private void OnImageChanged()
        {
            // TODO - пустой вызов, чтобы не падало
        }

        public EnumerationClasses.Visitor Begin()
        {
            if (Set.Count > 0)
            {
                selectedIndex = 0;
                OrdersCardsToVisitor(selectedIndex);
                CurrentItem = Set[0];
            }
            return CurrentItem;
        }

        public EnumerationClasses.Visitor End()
        {
            if (Set.Count > 0)
            {
                selectedIndex = Set.Count - 1;
                OrdersCardsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        public EnumerationClasses.Visitor Prev()
        {
            if (Set.Count > 0 && selectedIndex > 0)
            {
                selectedIndex--;
                OrdersCardsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        public EnumerationClasses.Visitor Next()
        {
            if (Set.Count > 0 && selectedIndex < Set.Count - 1)
            {
                selectedIndex++;
                OrdersCardsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        private void OrdersCardsToVisitor(int index)
        {
            if (Set[index].Orders == null)
            {
                Set[index].Orders = new ObservableCollection<Order>(
                        from OrdElem in OrderElementsWrapper.CurrentTable()
                        .Table.AsEnumerable()
                        join
    Ord in OrdersWrapper.CurrentTable().Table.AsEnumerable()
    on OrdElem.Field<int>("f_ord_id") equals Ord.Field<int>("f_ord_id")
                        where OrdElem.Field<int>("f_visitor_id") == Set[index].Id
                        select new Order
                        {
                            Id = Ord.Field<int>("f_ord_id"),
                            RegNumber = Ord.Field<int>("f_reg_number").ToString(),
                            From = Ord.Field<DateTime>("f_date_from"),
                            To = Ord.Field<DateTime>("f_date_to"),
                            Catcher = (string)VisitorsWrapper.CurrentTable().Table
                                .AsEnumerable().FirstOrDefault(arg =>
                                arg.Field<int>("f_visitor_id") ==
                                OrdElem.Field<int>("f_catcher_id"))?["f_full_name"],
                            OrderType = "В разработке",
                            Passes = OrdElem.Field<string>("f_passes")
                        });
            }
            if (Set[index].Cards == null)
            {
                Set[index].Cards = new ObservableCollection<Card2>(
                    from card in CardsWrapper.CurrentTable().Table.AsEnumerable()
                    join visit in VisitsWrapper.CurrentTable().Table.AsEnumerable()
                    on card.Field<int>("f_card_id") equals visit.Field<int>("f_card_id")
                    where visit.Field<int>("f_visitor_id") == Set[index].Id
                    select new Card2
                    {
                        Card = card.Field<string>("f_card_text"),
                        From = visit.Field<DateTime>("f_date_from"),
                        To = visit.Field<DateTime>("f_date_to"),
                        Change = visit.Field<DateTime>("f_rec_date"),
                        Operator = UsersWrapper.CurrentTable().Table
                            .AsEnumerable().FirstOrDefault(arg =>
                            arg.Field<int>("f_user_id") ==
                            visit.Field<int>("f_rec_operator"))?["f_user"].ToString(),
                        OrderNum = OrdersWrapper.CurrentTable().Table
                            .AsEnumerable().FirstOrDefault(arg => arg.Field<int>
                            ("f_ord_id") == visit.Field<int>("f_order_id"))
                            ?["f_reg_number"].ToString(),
                        Comment = visit.Field<string>("f_visit_text")
                    });
            }
        }

        public void AddImageSource(string path, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == CurrentItem.Id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }
            bool find = row != null;
            row = row ?? ImagesWrapper.CurrentTable().Table.NewRow();
            var alias = Guid.NewGuid();
            row["f_image_alias"] = alias;
            if (!find)
            {
                row["f_visitor_id"] = CurrentItem.Id;
                row["f_image_type"] = imageType;
                ImagesWrapper.CurrentTable().Table.Rows.Add(row);
            }
            byte[] data = File.ReadAllBytes(path);

            string image = "";
            if (ImagesWrapper.CurrentTable().Connector.SetImage(alias, data))
            {
                image = Directory.GetCurrentDirectory() + "\\" + Images + "\\" + alias;
                File.WriteAllBytes(image, data);
            }

            SetImageSource(image, imageType);
        }

        public void RemoveImageSource(ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == CurrentItem.Id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                row.Delete();
            }
            SetImageSource("", imageType);
        }

        private void SetImageSource(string source, ImageType imageType)
        {
            if (imageType == ImageType.Photo)
            {
                PhotoSource = source;
                if (OnModelPropertyChanged != null)
                {
                    OnModelPropertyChanged("PhotoSource");
                }
            }
            else if (imageType == ImageType.Signature)
            {
                Signature = source;
                if (OnModelPropertyChanged != null)
                {
                    OnModelPropertyChanged("Signature");
                }
            }
        }

        private void GetPhoto()
        {
            GetImage(ImageType.Photo);
        }

        private void GetSign()
        {
            GetImage(ImageType.Signature);
        }

        private void GetImage(ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == CurrentItem.Id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }

            string source = "";
            if (row != null)
            {
                string path = Directory.GetCurrentDirectory() + "\\" + Images + "\\" + row["f_image_alias"];
                if (!File.Exists(path))
                {
                    byte[] data =
                        ImagesWrapper.CurrentTable().Connector.GetImage((Guid)row["f_image_alias"]);
                    if (data != null)
                    {
                        File.WriteAllBytes(path, data);
                    }
                    else
                    {
                        path = "";
                    }
                }
                source = path;
            }

            SetImageSource(source, imageType);
        }
    }

    public class NewVisitsModel : IVisitsModel
    {
        public event ModelPropertyChanged OnModelPropertyChanged;

        public string PhotoSource { get; private set; }
        public string Signature { get; private set; }

        private ObservableCollection<EnumerationClasses.Visitor> set;
        private EnumerationClasses.Visitor currentItem;

        public NewVisitsModel()
        {
            Set = new ObservableCollection<EnumerationClasses.Visitor>();
            CurrentItem = new EnumerationClasses.Visitor();
            Set.Add(CurrentItem);
        }

        public ObservableCollection<EnumerationClasses.Visitor> Set
        {
            get { return set; }
            set { set = value; }
        }

        public EnumerationClasses.Visitor CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }
        public EnumerationClasses.Visitor Begin()
        {
            throw new NotImplementedException();
        }

        public EnumerationClasses.Visitor End()
        {
            throw new NotImplementedException();
        }

        public EnumerationClasses.Visitor Next()
        {
            throw new NotImplementedException();
        }

        public EnumerationClasses.Visitor Prev()
        {
            throw new NotImplementedException();
        }

        public void AddImageSource(string path, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        public void RemoveImageSource(ImageType imageType)
        {
            throw new NotImplementedException();
        }
    }
}
