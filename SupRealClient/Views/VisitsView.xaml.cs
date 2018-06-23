using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using SupClientConnectionLib;
using SupContract;
using SupRealClient.Annotations;
using SupRealClient.Common;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.TabsSingleton;
using SupRealClient.Views.Visitor;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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

        private void VisitsView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    public class VisitsViewModel : INotifyPropertyChanged
    {
        private IVisitsModel model;
        private IWindow view;
        private int selectedMainDocument = -1;
        private int selectedDocument = -1;

        public CollectionView PositionList { get; private set; }

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
                VisitorsEnable = model.VisitorsEnable;
                VisitorsVisible = model.VisitorsVisible;
                TextEnable = model.TextEnable;
                ButtonEnable = model.ButtonEnable;
                AccessVisibility = model.AccessVisibility;
            }
        }

        /// <summary>
        /// Объект со списком свойств Enable для кнопок
        /// </summary>
        public VisitorsEnableOrVisible VisitorsEnable
        {
            get { return Model.VisitorsEnable; }
            set
            {
                Model.VisitorsEnable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Объект со списком свойтсв Visible для кнопок
        /// </summary>
        public VisitorsEnableOrVisible VisitorsVisible
        {
            get { return Model.VisitorsVisible; }
            set
            {
                Model.VisitorsVisible = value;
                OnPropertyChanged();
            }
        }

        public bool TextEnable
        {
            get { return Model.TextEnable; }
            set
            {
                Model.TextEnable = value;
                OnPropertyChanged();
            }
        }

        public bool ButtonEnable
        {
            get { return Model.ButtonEnable; }
            set
            {
                Model.ButtonEnable = value;
                OnPropertyChanged();
            }
        }

        public bool AccessVisibility
        {
            get { return Model.AccessVisibility; }
            set
            {
                Model.AccessVisibility = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EnumerationClasses.Visitor> Set
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

        public int SelectedMainDocument
        {
            get { return selectedMainDocument; }
            set
            {
                selectedMainDocument = value;
                OnPropertyChanged();
            }
        }

        public int SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged();
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
        public ICommand DepartmentsCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public ICommand ExtraditeCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand FindCommand { get; set; }

        public ICommand AddImageSourceCommand { get; set; }
        public ICommand RemoveImageSourceCommand { get; set; }
        public ICommand AddSignatureCommand { get; set; }
        public ICommand RemoveSignatureCommand { get; set; }

        public ICommand OpenDocumentCommand { get; set; }
        public ICommand AddDocumentCommand { get; set; }
        public ICommand EditDocumentCommand { get; set; }
        public ICommand RemoveDocumentCommand { get; set; }

        public ICommand OpenMainDocumentCommand { get; set; }
        public ICommand AddMainDocumentCommand { get; set; }
        public ICommand EditMainDocumentCommand { get; set; }
        public ICommand RemoveMainDocumentCommand { get; set; }

        private ChildWindowSettings _windowSettings;
        public ChildWindowSettings WindowSettings
        {
            get { return _windowSettings; }
            set { _windowSettings = value; OnPropertyChanged("WindowSettings"); }
        }

        public ChildWinSet WinSet { get; set; }

        public VisitsViewModel(IWindow view)
        {
            WinSet = new ChildWinSet() { Left = 0, Top = 120 };
            //GlobalSettings.GetChildWindowSettings();     

            this.view = view;
            Model = new VisitsModel();

            this.PositionList =
                new CollectionView(Model.CurrentItem != null ?
                VisitorsHelper.GetPositions(
                Model.CurrentItem.Position) : new List<string>());

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
            DepartmentsCommand = new RelayCommand(arg => DepartmentsList());

            ClearCommand = new RelayCommand(arg => Clear(arg as string));

            ExtraditeCommand = new RelayCommand(obj => Extradite());
            ReturnCommand = new RelayCommand(obj => Return());

            OkCommand = new RelayCommand(arg => Ok());
            CancelCommand = new RelayCommand(arg => Cancel());
            EditCommand = new RelayCommand(arg => Edit());
            FindCommand = new RelayCommand(arg => Find());

            AddImageSourceCommand = new RelayCommand(arg => AddImageSource(ImageType.Photo));
            RemoveImageSourceCommand= new RelayCommand(arg => RemoveImageSource(ImageType.Photo));
            AddSignatureCommand = new RelayCommand(arg => AddImageSource(ImageType.Signature));
            RemoveSignatureCommand = new RelayCommand(arg => RemoveImageSource(ImageType.Signature));

            OpenDocumentCommand = new RelayCommand(arg => OpenDocument());
            AddDocumentCommand = new RelayCommand(arg => AddDocument());
            EditDocumentCommand = new RelayCommand(arg => EditDocument());
            RemoveDocumentCommand = new RelayCommand(arg => RemoveDocument());

            OpenMainDocumentCommand = new RelayCommand(arg => OpenMainDocument());
            AddMainDocumentCommand = new RelayCommand(arg => AddMainDocument());
            EditMainDocumentCommand = new RelayCommand(arg => EditMainDocument());
            RemoveMainDocumentCommand = new RelayCommand(arg => RemoveMainDocument());
        }

        /// <summary>
        /// Конструктор с возможностью загрузки нового посетителя.
        /// todo: скорее всего, под удаление.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="isNew"></param>
        public VisitsViewModel(IWindow view, bool isNew) : this(view)
        {
            if (isNew)
            {
                this.New();
            }
        }

        private void CabinetsList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4CabinetsWindViewOk", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            CurrentItem.CabinetId = result.Id;
            CurrentItem.Cabinet = result.Name;
            OnPropertyChanged("CurrentItem");
        }

        private void DepartmentsList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "MainOrganisationStructureView", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            CurrentItem.DepartmentId = result.Id;
            CurrentItem.Department =
                Model.GetDepartmenstList(CurrentItem.DepartmentId);
            OnPropertyChanged("CurrentItem");
        }

        private void OrganizationsList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4OrganizationsWindView", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            CurrentItem.OrganizationId = result.Id;
            CurrentItem.Organization = OrganizationsHelper.
                GenerateFullName(result.Id, true);
            OnPropertyChanged("CurrentItem");
        }

        private void CountyList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4NationsWindViewOk", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            CurrentItem.NationId = result.Id;
            CurrentItem.Nation = result.Name;
            OnPropertyChanged("CurrentItem");
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Nation":
                    CurrentItem.NationId = -1;
                    CurrentItem.Nation = "";
                    break;
                case "Organization":
                    CurrentItem.OrganizationId = -1;
                    CurrentItem.Organization = "";
                    break;
                case "DocType":
                    CurrentItem.DocumentId = -1;
                    CurrentItem.DocType = "";
                    break;
                case "Department":
                    CurrentItem.DepartmentId = -1;
                    CurrentItem.Department = "";
                    break;
                case "Cabinet":
                    CurrentItem.CabinetId = -1;
                    CurrentItem.Cabinet = "";
                    break;
                case "Position":
                    CurrentItem.Position = "";
                    break;
                case "Comment":
                    CurrentItem.Comment = "";
                    break;
                default:
                    return;
            }

            OnPropertyChanged("CurrentItem");
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

        private void Extradite()
        {
            Base4ViewModel<Order> viewModel =
                new Base4ViewModel<Order>
                {
                    Model = new AddZoneModel(CurrentItem.Orders)
                };
            var window = new AddZone
            {
                DataContext = viewModel
            };
            window.ShowDialog();
        }

        private void Return()
        {
            var window = new ReturnBid();

            window.ShowDialog();
        }

        private void Edit()
        {
            Model = new EditVisitsModel(Set, CurrentItem);
        }

        private void Find()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindView", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            CurrentItem = Model.Find(result.Id);
        }

        private void Ok()
        {
            if (Model.Ok())
            {
                if (view.ParentWindow is VisitorsListWindView)
                    view.CloseWindow(new CancelEventArgs());
                else
                    Model = new VisitsModel();
            }                
        }

        private void Cancel()
        {
            if (Model is NewVisitsModel)
            {
                if (view.ParentWindow is SupRealClient.Views.VisitorsListWindView)
                    view.CloseWindow(new CancelEventArgs());
                else
                    Model = new VisitsModel();
            }
            else if (Model is EditVisitsModel)
            {
                Model = new VisitsModel(Set, ((EditVisitsModel)Model).OldVisitor);
            }
        }

        private void AddImageSource(ImageType imageType)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Model.AddImageSource(dlg.FileName, imageType);
            }
        }

        private void RemoveImageSource(ImageType imageType)
        {
            Model.RemoveImageSource(imageType);
        }

        private void OpenDocument()
        {
            if (SelectedDocument < 0)
            {
                return;
            }

            var window = new DocumentImagesView(
                CurrentItem.Documents[SelectedDocument]);
            window.ShowDialog();
        }

        private void AddDocument()
        {
            var window = new VisitorsDocumentView(
                new VisitorsDocumentModel(null));
            window.ShowDialog();
            var document = window.WindowResult as VisitorsDocument;
           
            if (document == null)
            {
                return;
            }

            Model.AddDocument(document);
        }

        private void EditDocument()
        {
            if (SelectedDocument < 0)
            {
                return;
            }
            var window = new VisitorsDocumentView(
                new VisitorsDocumentModel(
                    CurrentItem.Documents[SelectedDocument]));
            window.ShowDialog();
            var document = window.WindowResult as VisitorsDocument;

            if (document == null)
            {
                return;
            }

            Model.EditDocument(SelectedDocument, document);
        }

        private void RemoveDocument()
        {
            if (SelectedDocument < 0)
            {
                return;
            }
            Model.RemoveDocument(SelectedDocument);
        }

        private void OpenMainDocument()
        {
            if (SelectedMainDocument < 0)
            {
                return;
            }

            var window = new VisitorsMainDocumentView(
               new VisitorsMainDocumentModel(
                   CurrentItem.MainDocuments[SelectedMainDocument]), false);
            window.ShowDialog();
        }

        private void AddMainDocument()
        {
            var window = new VisitorsMainDocumentView(
                new VisitorsMainDocumentModel(null), true);
            window.ShowDialog();
            var document = window.WindowResult as VisitorsMainDocument;

            if (document == null)
            {
                return;
            }

            Model.AddMainDocument(document);
        }

        private void EditMainDocument()
        {
            if (SelectedMainDocument < 0)
            {
                return;
            }
            var window = new VisitorsMainDocumentView(
                new VisitorsMainDocumentModel(
                    CurrentItem.MainDocuments[SelectedMainDocument]), true);
            window.ShowDialog();
            var document = window.WindowResult as VisitorsMainDocument;

            if (document == null)
            {
                return;
            }

            Model.EditMainDocument(SelectedMainDocument, document);
        }

        private void RemoveMainDocument()
        {
            if (SelectedMainDocument < 0)
            {
                return;
            }
            Model.RemoveMainDocument(SelectedMainDocument);
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
        event Action<object> OnClose;
        string PhotoSource { get; }
        string Signature { get; }

        ObservableCollection<EnumerationClasses.Visitor> Set { get; set; }
        EnumerationClasses.Visitor CurrentItem { get; set; }
        bool TextEnable { get; set; }
        VisitorsEnableOrVisible VisitorsEnable { get; set; }
        VisitorsEnableOrVisible VisitorsVisible { get; set; }
        bool ButtonEnable { get; set; }
        bool AccessVisibility { get; set; }

        EnumerationClasses.Visitor Begin();
        EnumerationClasses.Visitor End();
        EnumerationClasses.Visitor Next();
        EnumerationClasses.Visitor Prev();
        EnumerationClasses.Visitor Find(int id);

        void AddImageSource(string path, ImageType imageType);
        void RemoveImageSource(ImageType imageType);
        bool Ok();

        string GetDepartmenstList(int? id);
        void AddDocument(VisitorsDocument document);
        void EditDocument(int index, VisitorsDocument document);
        void RemoveDocument(int index);
        void AddMainDocument(VisitorsMainDocument document);
        void EditMainDocument(int index, VisitorsMainDocument document);
        void RemoveMainDocument(int index);
    }

    public abstract class BaseVisitsModel : IVisitsModel
    {
        public event ModelPropertyChanged OnModelPropertyChanged;
        public virtual event Action<object> OnClose;

        private EnumerationClasses.Visitor currentItem;
        protected VisitorsEnableOrVisible visitorsEnable;

        protected Guid photoAlias;
        protected Guid signAlias;

        protected bool isMainDocumentsChanged = false;
        protected bool isDocumentsChanged = false;

        protected BaseVisitsModel()
        {
            ImagesHelper.Init();
            ImagesWrapper.CurrentTable().OnChanged += EmptyQuery;
            VisitorsDocumentsWrapper.CurrentTable().OnChanged += EmptyQuery;
            ImageDocumentWrapper.CurrentTable().OnChanged += EmptyQuery;
        }

        public string PhotoSource { get; private set; }
        public string Signature { get; private set; }

        public ObservableCollection<EnumerationClasses.Visitor> Set { get; set; }

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

        public abstract bool TextEnable { get; set; }

        public VisitorsEnableOrVisible VisitorsEnable
        {
            get { return visitorsEnable; }
            set { visitorsEnable = value; }
        }

        public VisitorsEnableOrVisible VisitorsVisible { get; set; } =
            new VisitorsEnableOrVisible();

        public abstract bool ButtonEnable { get; set; }

        public abstract bool AccessVisibility { get; set; }

        public virtual EnumerationClasses.Visitor Begin()
        {
            throw new NotImplementedException();
        }

        public virtual EnumerationClasses.Visitor End()
        {
            throw new NotImplementedException();
        }

        public virtual EnumerationClasses.Visitor Next()
        {
            throw new NotImplementedException();
        }

        public virtual EnumerationClasses.Visitor Prev()
        {
            throw new NotImplementedException();
        }

        public virtual EnumerationClasses.Visitor Find(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void AddImageSource(string path, ImageType imageType)
        {
            SetImageSource(ImagesHelper.LoadImage(path), imageType);
        }

        public virtual void RemoveImageSource(ImageType imageType)
        {
            SetImageSource(Guid.Empty, imageType);
        }

        public virtual bool Ok()
        {
            return false;
        }

        public string GetDepartmenstList(int? id)
        {
            var departmentList = new List<string>();
            while (id.HasValue && id.Value > 0)
            {
                DataRow row = DepartmentWrapper.CurrentTable().
                    Table.AsEnumerable().FirstOrDefault(arg =>
                    arg.Field<int>("f_dep_id") == id);
                if (row == null)
                {
                    break;
                }
                departmentList.Insert(0, row["f_dep_name"] as string);
                id = row["f_parent_id"] as int?;
            }

            var sb = new StringBuilder();
            int k = 0;
            foreach (var department in departmentList)
            {
                for (int i = 0; i < k; i++)
                {
                    sb.Append("   ");
                }
                sb.AppendLine(department);
                k++;
            }
            return sb.ToString();
        }

        public void AddDocument(VisitorsDocument document)
        {
            CurrentItem.Documents.Add(document);
            isDocumentsChanged = true;
            document.IsChanged = true;
        }

        public void EditDocument(int index, VisitorsDocument document)
        {
            CurrentItem.Documents.RemoveAt(index);
            CurrentItem.Documents.Insert(index, document);
            isDocumentsChanged = true;
            document.IsChanged = true;
        }

        public void RemoveDocument(int index)
        {
            CurrentItem.Documents.RemoveAt(index);
            isDocumentsChanged = true;
        }

        public void AddMainDocument(VisitorsMainDocument document)
        {
            CurrentItem.MainDocuments.Add(document);
            isMainDocumentsChanged = true;
            document.IsChanged = true;
        }

        public void EditMainDocument(int index, VisitorsMainDocument document)
        {
            CurrentItem.MainDocuments.RemoveAt(index);
            CurrentItem.MainDocuments.Insert(index, document);
            isMainDocumentsChanged = true;
            document.IsChanged = true;
        }

        public void RemoveMainDocument(int index)
        {
            CurrentItem.MainDocuments.RemoveAt(index);
            isMainDocumentsChanged = true;
        }

        protected bool Validate()
        {
            if (string.IsNullOrEmpty(CurrentItem.Family) ||
                string.IsNullOrEmpty(CurrentItem.Name) ||
                string.IsNullOrEmpty(CurrentItem.Patronymic) ||
                string.IsNullOrEmpty(CurrentItem.Organization))
            {
                MessageBox.Show("Не все поля заполнены корректно!");
                return false;
            }
            if (!CurrentItem.IsNotFormular &
                (string.IsNullOrEmpty(CurrentItem.Telephone) ||
                string.IsNullOrEmpty(CurrentItem.Nation) ||
                !CurrentItem.MainDocuments.Any()))
            {
                MessageBox.Show("Не все поля вкладки Основная заполнены!");
                return false;
            }
            if (!ValidateDocumentDates() &&
                MessageBox.Show("В документах разные даты рождения. Все равно сохранить?",
                "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }

            return true;
        }

        protected void SaveAdditionalData(int id)
        {
            List<KeyValuePair<Guid, ImageType>> images =
                new List<KeyValuePair<Guid, ImageType>>();

            if (isMainDocumentsChanged)
            {
                foreach (var document in CurrentItem.MainDocuments)
                {
                    if (!document.Images.Any())
                    {
                        document.Images =
                            DocumentsHelper.CacheImages(document.Id);
                    }
                }

                RemoveOldDocuments(id, true);
                foreach (var document in CurrentItem.MainDocuments)
                {
                    DataRow row = VisitorsDocumentsWrapper.
                        CurrentTable().Table.NewRow();
                    row["f_visitor_id"] = id;
                    row["f_doctype_id"] = document.TypeId;
                    row["f_doc_name"] = "";
                    row["f_doc_seria"] = document.Seria;
                    row["f_doc_num"] = document.Num;
                    row["f_doc_date"] = document.Date;
                    row["f_doc_date_to"] = document.DateTo;
                    row["f_doc_org"] = document.Org;
                    row["f_doc_code"] = document.Code;
                    row["f_birth_date"] = document.BirthDate;
                    row["f_comment"] = document.Comment;
                    row["f_deleted"] = "N";
                    VisitorsDocumentsWrapper.
                        CurrentTable().Table.Rows.Add(row);
                    int documentId = row.Field<int>("f_vd_id");
                    foreach (var alias in document.Images)
                    {
                        DataRow imageRow = ImagesWrapper.
                            CurrentTable().Table.NewRow();
                        imageRow["f_image_alias"] = alias;
                        imageRow["f_visitor_id"] = id;
                        imageRow["f_image_type"] = ImageType.Document;
                        imageRow["f_deleted"] = "N";
                        images.Add(new KeyValuePair<Guid, ImageType>(
                            alias, ImageType.Document));
                        ImagesWrapper.CurrentTable().Table.Rows.Add(imageRow);
                        int imageId = imageRow.Field<int>("f_image_id");

                        DataRow imageDocRow = ImageDocumentWrapper.
                            CurrentTable().Table.NewRow();
                        imageDocRow["f_image_id"] = imageId;
                        imageDocRow["f_doc_id"] = documentId;
                        imageDocRow["f_deleted"] = "N";
                        ImageDocumentWrapper.CurrentTable().
                            Table.Rows.Add(imageDocRow);
                    }
                }
            }

            if (isDocumentsChanged)
            {
                foreach (var document in CurrentItem.Documents)
                {
                    if (!document.Images.Any())
                    {
                        document.Images =
                            DocumentsHelper.CacheImages(document.Id);
                    }
                }

                RemoveOldDocuments(id, false);
                foreach (var document in CurrentItem.Documents)
                {
                    DataRow row = VisitorsDocumentsWrapper.
                        CurrentTable().Table.NewRow();
                    row["f_visitor_id"] = id;
                    row["f_doctype_id"] = 0;
                    row["f_doc_name"] = document.Name;
                    row["f_doc_seria"] = "";
                    row["f_doc_num"] = "";
                    row["f_doc_date"] = DateTime.MinValue;
                    row["f_doc_date_to"] = DateTime.MinValue;
                    row["f_doc_org"] = "";
                    row["f_doc_code"] = "";
                    row["f_comment"] = "";
                    row["f_deleted"] = "N";
                    VisitorsDocumentsWrapper.
                        CurrentTable().Table.Rows.Add(row);
                    int documentId = row.Field<int>("f_vd_id");
                    foreach (var alias in document.Images)
                    {
                        DataRow imageRow = ImagesWrapper.
                            CurrentTable().Table.NewRow();
                        imageRow["f_image_alias"] = alias;
                        imageRow["f_visitor_id"] = id;
                        imageRow["f_image_type"] = ImageType.Document;
                        imageRow["f_deleted"] = "N";
                        images.Add(new KeyValuePair<Guid, ImageType>(
                            alias, ImageType.Document));
                        ImagesWrapper.CurrentTable().Table.Rows.Add(imageRow);
                        int imageId = imageRow.Field<int>("f_image_id");

                        DataRow imageDocRow = ImageDocumentWrapper.
                            CurrentTable().Table.NewRow();
                        imageDocRow["f_image_id"] = imageId;
                        imageDocRow["f_doc_id"] = documentId;
                        imageDocRow["f_deleted"] = "N";
                        ImageDocumentWrapper.CurrentTable().
                            Table.Rows.Add(imageDocRow);
                    }
                }
            }

            images.Add(new KeyValuePair<Guid, ImageType>(
                photoAlias, ImageType.Photo));
            images.Add(new KeyValuePair<Guid, ImageType>(
                signAlias, ImageType.Signature));
            ImagesHelper.AddImages(id, images);
        }

        protected string GetBirthDate(EnumerationClasses.Visitor visitor)
        {
            DateTime? birthDate = null;
            foreach (var document in visitor.MainDocuments)
            {
                if (document.BirthDate > DateTime.MinValue)
                {
                    if (!birthDate.HasValue)
                    {
                        birthDate = document.BirthDate;
                    }
                    else
                    {
                        if (!document.BirthDate.Equals(birthDate))
                        {
                            return "В документах указаны разные даты рождения";
                        }
                    }
                }
            }
            return birthDate.HasValue ? birthDate.Value.ToShortDateString() : "";
        }

        private bool ValidateDocumentDates()
        {
            DateTime? birthDate = null;
            foreach (var document in CurrentItem.MainDocuments)
            {
                if (document.BirthDate > DateTime.MinValue)
                {
                    if (!birthDate.HasValue)
                    {
                        birthDate = document.BirthDate;
                    }
                    else
                    {
                        if (!document.BirthDate.Equals(birthDate))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void RemoveOldDocuments(int visitorId, bool isMain)
        {
            foreach (DataRow r in VisitorsDocumentsWrapper.
                CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == visitorId &&
                    (isMain ? r.Field<int>("f_doctype_id") != 0 :
                    r.Field<int>("f_doctype_id") == 0) &&
                    r.Field<string>("f_deleted") == "N")
                {
                    r["f_deleted"] = "Y";
                    //r.Delete(); // TODO
                    RemoveOldDocumentImages(r.Field<int>("f_vd_id"));
                }
            }
        }

        private void RemoveOldDocumentImages(int docId)
        {
            foreach (DataRow r in ImageDocumentWrapper.
                CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_doc_id") == docId &&
                    r.Field<string>("f_deleted") == "N")
                {
                    r["f_deleted"] = "Y";
                    //r.Delete(); // TODO
                    RemoveOldImages(r.Field<int>("f_image_id"));
                }
            }
        }

        private void RemoveOldImages(int imageId)
        {
            foreach (DataRow r in ImagesWrapper.
                CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_image_id") == imageId &&
                    r.Field<string>("f_deleted") == "N")
                {
                    r["f_deleted"] = "Y";
                    //r.Delete(); // TODO
                }
            }
        }

        private void EmptyQuery()
        {
            // TODO - пустой вызов, чтобы не падало
        }

        private void SetImageSource(Guid alias, ImageType imageType)
        {
            if (imageType == ImageType.Photo)
            {
                photoAlias = alias;
                PhotoSource = ImagesHelper.GetImagePath(photoAlias);
                OnModelPropertyChanged?.Invoke("PhotoSource");
            }
            else if (imageType == ImageType.Signature)
            {
                signAlias = alias;
                Signature = ImagesHelper.GetImagePath(signAlias);
                OnModelPropertyChanged?.Invoke("Signature");
            }
        }

        private void GetPhoto()
        {
            SetImageSource(CurrentItem == null ? Guid.Empty :
                ImagesHelper.GetImage(
                    CurrentItem.Id, ImageType.Photo), ImageType.Photo);
        }

        private void GetSign()
        {
            SetImageSource(CurrentItem == null ? Guid.Empty : 
                ImagesHelper.GetImage(
                    CurrentItem.Id, ImageType.Signature), ImageType.Signature);
        }
    }

    public class VisitsModel : BaseVisitsModel
    {
        private int selectedIndex;

        public override bool TextEnable
        {
            get { return false; }
            set { }
        }

        public override bool ButtonEnable
        {
            get { return false; }
            set { }
        }

        public override bool AccessVisibility
        {
            get { return true; }
            set { }
        }

        public VisitsModel()
        {
            visitorsEnable =
            new VisitorsEnableOrVisible
            {
                AcceptButtonEnable = false,
                CancelButtonEnable = false
            };
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
        }

        public VisitsModel(
            ObservableCollection<EnumerationClasses.Visitor> set, 
            EnumerationClasses.Visitor visitor)
        {
            visitorsEnable =
            new VisitorsEnableOrVisible
            {
                AcceptButtonEnable = false,
                CancelButtonEnable = false
            };
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Set = set;
            CurrentItem = visitor;
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
                    /*Organization = OrganizationsHelper.
                        GenerateFullName(visitors.Field<int>("f_org_id"), true),*/
                    Comment = visitors.Field<string>("f_vr_text"),
                    IsAccessDenied = CommonHelper.StringToBool(visitors.Field<string>(
                        "f_persona_non_grata")),
                    IsCanHaveVisitors = CommonHelper.StringToBool(visitors.Field<string>(
                        "f_can_have_visitors")),
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
                    DocCode = visitors.Field<string>("f_doc_code"),
                    DocPlace = visitors.Field<string>("f_doc_org"),
                    IsAgree = CommonHelper.StringToBool(visitors.Field<string>(
                        "f_personal_data_agreement")),
                    AgreeToDate = visitors.Field<DateTime>("f_personal_data_last_date"),
                    Operator = GetOperator(visitors.Field<int>("f_rec_operator_pass"),
                        visitors.Field<DateTime>("f_rec_date_pass")),
                    Department = GetDepartmenstList(visitors.Field<int>("f_dep_id")),
                    Position = visitors.Field<string>("f_job"),
                    IsRightSign = CommonHelper.StringToBool(visitors.Field<string>(
                        "f_can_sign_orders")),
                    IsAgreement = CommonHelper.StringToBool(visitors.Field<string>(
                        "f_can_adjust_orders")),
                    Cabinet = (string)CabinetsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_cabinet_id") ==
                        visitors.Field<int>("f_cabinet_id"))?["f_cabinet_desc"],
                });
            if (Set.Count > 0)
            {
                OrdersCardsToVisitor(0);
                DocumentsToVisitor(0);
                CurrentItem = Set[0];
            }
        }

        public override EnumerationClasses.Visitor Begin()
        {
            if (Set.Count > 0)
            {
                selectedIndex = 0;
                OrdersCardsToVisitor(selectedIndex);
                DocumentsToVisitor(selectedIndex);
                CurrentItem = Set[0];
            }
            return CurrentItem;
        }

        public override EnumerationClasses.Visitor End()
        {
            if (Set.Count > 0)
            {
                selectedIndex = Set.Count - 1;
                OrdersCardsToVisitor(selectedIndex);
                DocumentsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        public override EnumerationClasses.Visitor Prev()
        {
            if (Set.Count > 0 && selectedIndex > 0)
            {
                selectedIndex--;
                OrdersCardsToVisitor(selectedIndex);
                DocumentsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        public override EnumerationClasses.Visitor Find(int id)
        {
            for (int i = 0; i < Set.Count; i++)
            {
                if (Set[i].Id == id)
                {
                    selectedIndex = i;
                    OrdersCardsToVisitor(selectedIndex);
                    DocumentsToVisitor(selectedIndex);
                    CurrentItem = Set[selectedIndex];
                    break;
                }
            }
            return CurrentItem;
        }

        public override EnumerationClasses.Visitor Next()
        {
            if (Set.Count > 0 && selectedIndex < Set.Count - 1)
            {
                selectedIndex++;
                OrdersCardsToVisitor(selectedIndex);
                DocumentsToVisitor(selectedIndex);
                CurrentItem = Set[selectedIndex];
            }
            return CurrentItem;
        }

        public override void AddImageSource(string path, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        public override void RemoveImageSource(ImageType imageType)
        {
            throw new NotImplementedException();
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

        private void DocumentsToVisitor(int index)
        {
            if (Set[index].MainDocuments == null)
            {
                Set[index].MainDocuments = new ObservableCollection<VisitorsMainDocument>(
                    from documents in VisitorsDocumentsWrapper.CurrentTable().
                    Table.AsEnumerable()
                    where documents.Field<int>("f_visitor_id") == Set[index].Id &&
                          documents.Field<int>("f_doctype_id") != 0 &&
                          documents.Field<string>("f_deleted") == "N"
                    select new VisitorsMainDocument
                    {
                        Id = documents.Field<int>("f_vd_id"),
                        VisitorId = Set[index].Id,
                        TypeId = documents.Field<int>("f_doctype_id"),
                        Type = (string)DocumentsWrapper.CurrentTable().Table
                            .AsEnumerable().FirstOrDefault(arg =>
                            arg.Field<int>("f_doc_id") ==
                            documents.Field<int>("f_doctype_id"))?["f_doc_name"],
                        Seria = documents.Field<string>("f_doc_seria"),
                        Num = documents.Field<string>("f_doc_num"),
                        Date = documents.Field<DateTime>("f_doc_date"),
                        DateTo = documents.Field<DateTime>("f_doc_date_to"),
                        Org = documents.Field<string>("f_doc_org"),
                        Code = documents.Field<string>("f_doc_code"),
                        BirthDate = documents.Field<DateTime>("f_birth_date"),
                        Comment = documents.Field<string>("f_comment")
                    });
                Set[index].BirthDate = GetBirthDate(Set[index]);
            }
            if (Set[index].Documents == null)
            {
                Set[index].Documents = new ObservableCollection<VisitorsDocument>(
                    from documents in VisitorsDocumentsWrapper.CurrentTable().
                    Table.AsEnumerable()
                    where documents.Field<int>("f_visitor_id") == Set[index].Id &&
                          documents.Field<int>("f_doctype_id") == 0 &&
                          documents.Field<string>("f_deleted") == "N"
                    select new VisitorsDocument
                    {
                        Id = documents.Field<int>("f_vd_id"),
                        VisitorId = Set[index].Id,
                        TypeId = 0,
                        Name = documents.Field<string>("f_doc_name"),
                    });
            }
        }

        private string GetOperator(int id, DateTime date)
        {
            return (string)UsersWrapper.CurrentTable()
                .Table.AsEnumerable().FirstOrDefault(arg =>
                arg.Field<int>("f_user_id") == id)?["f_user"] +
                " " + date.ToShortDateString() + " " +
                date.ToShortTimeString();
        }
    }

    public class NewVisitsModel : BaseVisitsModel
    {
        public override event Action<object> OnClose;

        public override bool TextEnable
        {
            get { return true; }
            set { }
        }

        public override bool ButtonEnable
        {
            get { return true; }
            set { }
        }

        public override bool AccessVisibility
        {
            get { return false; }
            set { }
        }

        public NewVisitsModel()
        {
            visitorsEnable =
            new VisitorsEnableOrVisible
            {
                StartButtonEnable = false,
                PreviousButtonEnable = false,
                NextButtonEnable = false,
                EndButtonEnable = false,
                ExtraditeButtonEnable = false,
                ReturnButtonEnable = false,
                NewButtonEnable = false,
                EditButtonEnable = false,
                SearchButtonEnable = false,
                RefreshButtonEnable = false
            };
            Set = new ObservableCollection<EnumerationClasses.Visitor>();
            CurrentItem = new EnumerationClasses.Visitor() {AgreeToDate=DateTime.Now };
            CurrentItem.MainDocuments = new ObservableCollection<VisitorsMainDocument>();
            CurrentItem.Documents = new ObservableCollection<VisitorsDocument>();
            Set.Add(CurrentItem);
        }
        
        public override bool Ok()
        {
            if (!Validate())
            {
                return false;
            }

            DataRow row = VisitorsWrapper.CurrentTable().Table.NewRow();

            row["f_rec_date_pass"] = DateTime.Now;
            row["f_is_short_data"] = CommonHelper.BoolToString(false);
            row["f_rec_operator_pass"] = Authorizer.AppAuthorizer.Id;
            row["f_full_name"] = CommonHelper.CreateFullName(CurrentItem.Family,
                CurrentItem.Name, CurrentItem.Patronymic);

            row["f_deleted"] = CommonHelper.BoolToString(false);
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;

            row["f_family"] = CurrentItem.Family;
            row["f_fst_name"] = CurrentItem.Name;
            row["f_sec_name"] = CurrentItem.Patronymic;
            row["f_org_id"] = CurrentItem.OrganizationId >= 0 ?
                CurrentItem.OrganizationId : 0;
            row["f_vr_text"] = CurrentItem.Comment ?? "";

            row["f_persona_non_grata"] =
                CommonHelper.BoolToString(CurrentItem.IsAccessDenied);
            row["f_can_have_visitors"] =
                CommonHelper.BoolToString(CurrentItem.IsCanHaveVisitors);
            row["f_personal_data_agreement"] =
                CommonHelper.BoolToString(CurrentItem.IsAgree);
            row["f_personal_data_last_date"] = CurrentItem.AgreeToDate;
            row["f_birth_date"] = Convert.ToDateTime(CurrentItem.BirthDate);
            row["f_phones"] = CurrentItem.Telephone ?? "";
            row["f_cntr_id"] = CurrentItem.NationId >= 0 ?
                CurrentItem.NationId : 0;
            row["f_doc_id"] = CurrentItem.DocumentId >= 0 ?
                CurrentItem.DocumentId : 0;
            row["f_doc_seria"] = CurrentItem.DocSeria ?? "";
            row["f_doc_num"] = CurrentItem.DocNum ?? "";
            row["f_doc_date"] = CurrentItem.DocDate;
            row["f_doc_code"] = CurrentItem.DocCode;
            row["f_doc_org"] = CurrentItem.DocPlace ?? "";
            row["f_job"] = CurrentItem.Position ?? "";
            row["f_can_sign_orders"] =
                CommonHelper.BoolToString(CurrentItem.IsRightSign);
            row["f_can_adjust_orders"] =
                CommonHelper.BoolToString(CurrentItem.IsAgreement);

            row["f_dep_id"] = CurrentItem.DepartmentId >= 0 ?
                CurrentItem.DepartmentId : 0;
            row["f_cabinet_id"] = CurrentItem.CabinetId >= 0 ?
                CurrentItem.CabinetId : 0;
            VisitorsWrapper.CurrentTable().Table.Rows.Add(row);

            SaveAdditionalData((int)row["f_visitor_id"]);
            OnClose?.Invoke(CurrentItem);
            return true;
        }
    }

    public class EditVisitsModel : BaseVisitsModel
    {
        public EnumerationClasses.Visitor OldVisitor { get; set; }

        public override bool TextEnable
        {
            get { return true; }
            set { }
        }

        public override bool ButtonEnable
        {
            get { return true; }
            set { }
        }

        public override bool AccessVisibility
        {
            get { return false; }
            set { }
        }

        public EditVisitsModel(ObservableCollection<EnumerationClasses.Visitor> set, 
            EnumerationClasses.Visitor visitor)
        {
            visitorsEnable =
            new VisitorsEnableOrVisible
            {
                StartButtonEnable = false,
                PreviousButtonEnable = false,
                NextButtonEnable = false,
                EndButtonEnable = false,
                ExtraditeButtonEnable = false,
                ReturnButtonEnable = false,
                NewButtonEnable = false,
                EditButtonEnable = false,
                SearchButtonEnable = false,
                RefreshButtonEnable = false
            };
            Set = set;
            CurrentItem = (EnumerationClasses.Visitor)visitor.Clone();
            OldVisitor = visitor;
        }

        public override bool Ok()
        {
            if (!Validate())
            {
                return false;
            }

            DataRow row = VisitorsWrapper.CurrentTable().Table.Rows.Find(
                CurrentItem.Id);

            row.BeginEdit();

            row["f_rec_date_pass"] = DateTime.Now;
            row["f_rec_operator_pass"] = Authorizer.AppAuthorizer.Id;

            bool fullNameChanged = false;
            if (OldVisitor.Family != CurrentItem.Family)
            {
                row["f_family"] = CurrentItem.Family;
                fullNameChanged = true;
            }
            if (OldVisitor.Name != CurrentItem.Name)
            {
                row["f_fst_name"] = CurrentItem.Name;
                fullNameChanged = true;
            }
            if (OldVisitor.Patronymic != CurrentItem.Patronymic)
            {
                row["f_sec_name"] = CurrentItem.Patronymic;
                fullNameChanged = true;
            }
            if (fullNameChanged)
            {
                row["f_full_name"] = CommonHelper.CreateFullName(
                    CurrentItem.Family, CurrentItem.Name, CurrentItem.Patronymic);
            }
            if (OldVisitor.OrganizationId != CurrentItem.OrganizationId)
                row["f_org_id"] = CurrentItem.OrganizationId >= 0 ?
                    CurrentItem.OrganizationId : 0;
            if (OldVisitor.Comment != CurrentItem.Comment)
                row["f_vr_text"] = CurrentItem.Comment;
            if (OldVisitor.IsAccessDenied != CurrentItem.IsAccessDenied)
            {
                row["f_persona_non_grata"] =
                    CommonHelper.BoolToString(CurrentItem.IsAccessDenied);
            }
            if (OldVisitor.IsCanHaveVisitors != CurrentItem.IsCanHaveVisitors)
            {
                row["f_can_have_visitors"] =
                    CommonHelper.BoolToString(CurrentItem.IsCanHaveVisitors);
            }
            if (OldVisitor.IsAgree != CurrentItem.IsAgree)
            {
                row["f_personal_data_agreement"] =
                    CommonHelper.BoolToString(CurrentItem.IsAgree);
            }
            if (OldVisitor.AgreeToDate != CurrentItem.AgreeToDate)
            {
                row["f_personal_data_last_date"] = CurrentItem.AgreeToDate;
            }
            if (OldVisitor.Telephone != CurrentItem.Telephone)
                row["f_phones"] = CurrentItem.Telephone;
            if (OldVisitor.Nation != CurrentItem.Nation)
                row["f_cntr_id"] = CurrentItem.NationId >= 0 ?
                    CurrentItem.NationId : 0;
            if (OldVisitor.DocType != CurrentItem.DocType)
                row["f_doc_id"] = CurrentItem.DocumentId >= 0 ?
                    CurrentItem.DocumentId : 0;
            if (OldVisitor.DocSeria != CurrentItem.DocSeria)
                row["f_doc_seria"] = CurrentItem.DocSeria;
            if (OldVisitor.DocNum != CurrentItem.DocNum)
                row["f_doc_num"] = CurrentItem.DocNum;
            if (OldVisitor.DocDate != CurrentItem.DocDate)
                row["f_doc_date"] = CurrentItem.DocDate;
            if (OldVisitor.DocCode != CurrentItem.DocCode)
                row["f_doc_code"] = CurrentItem.DocCode;
            if (OldVisitor.DocPlace != CurrentItem.DocPlace)
                row["f_doc_org"] = CurrentItem.DocPlace;
            if (OldVisitor.Position != CurrentItem.Position)
                row["f_job"] = CurrentItem.Position;
            if (OldVisitor.IsRightSign != CurrentItem.IsRightSign)
            {
                row["f_can_sign_orders"] =
                    CommonHelper.BoolToString(CurrentItem.IsRightSign);
            }
            if (OldVisitor.IsAgreement != CurrentItem.IsAgreement)
            {
                row["f_can_adjust_orders"] =
                    CommonHelper.BoolToString(CurrentItem.IsAgreement);
            }
            if (OldVisitor.Cabinet != CurrentItem.Cabinet)
            {
                row["f_cabinet_id"] = CurrentItem.CabinetId >= 0 ?
                    CurrentItem.CabinetId : 0;
            }
            if (OldVisitor.Department != CurrentItem.Department)
            {
                row["f_dep_id"] = CurrentItem.DepartmentId >= 0 ?
                    CurrentItem.DepartmentId : 0;
            }
            row.EndEdit();

            SaveAdditionalData(CurrentItem.Id);

            return true;
        }
    }
}
