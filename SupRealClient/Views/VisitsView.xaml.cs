using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using RegulaLib;
using SupClientConnectionLib;
using SupContract;
using SupRealClient.Annotations;
using SupRealClient.Common;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.TabsSingleton;
using SupRealClient.ViewModels;
using SupRealClient.Views.Visitor;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

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
        private int selectedCard = -1;
        private int selectedOrder = -1;
        private const string _nameDocument_PhotoImageType = "Личная фотография";
	    private const string _nameDocument_SignatureImageType = "Личная подпись";
	    private bool _visibleButton_OpenDocument_InRedactMode = false;
	    private bool _enableButton_OpenDocument_InRedactMode = false;
		private bool _isRedactMode = false;
	    private bool _editingVisitorCommentMode = false;
	    private string _bufer_CurrentItem_Comment = "";


		public event Action<string> MoveNextFocusingElement;

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

				if (model is NewVisitsModel || model is EditVisitsModel)
				{
					IsRedactMode = true;
				}
				else
				{
					IsRedactMode = false;
				}

	            SelectedDocument = -1;
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

	    public bool CommentTextEnable
		{
		    get { return Model.CommentTextEnable; }
		    set
		    {
			    Model.CommentTextEnable = value;
			    OnPropertyChanged(nameof(CommentTextEnable));
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

	    public bool IsRedactMode
	    {
		    get { return _isRedactMode; }
		    set
		    {
			    _isRedactMode = value;
			    VisibleButton_OpenDocument_InRedactMode = _isRedactMode;
			    OnPropertyChanged(nameof(IsRedactMode));
			    OnPropertyChanged(nameof(VisibleTabItem_Employee));
			    OnPropertyChanged(nameof(IsRedactMode_Inverce));
			    EditingVisitorCommentMode = false;
		    }
	    }

	    public bool IsRedactMode_Inverce
	    {
		    get { return !_isRedactMode; }
	    }


		public bool VisibleButton_OpenDocument_InRedactMode
	    {
		    get { return _visibleButton_OpenDocument_InRedactMode; }
		    set
		    {
			    _visibleButton_OpenDocument_InRedactMode = value;
			    OnPropertyChanged(nameof(VisibleButton_OpenDocument_InRedactMode));
			}
	    }

	    public bool EnableButton_OpenDocument_InRedactMode
	    {
		    get { return _enableButton_OpenDocument_InRedactMode; }
		    set
		    {
			    _enableButton_OpenDocument_InRedactMode = value;
			    OnPropertyChanged(nameof(EnableButton_OpenDocument_InRedactMode));
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
	            OnPropertyChanged(nameof(SelectedDocument));
				Change_ButtonEnable();
            }
        }

        public int SelectedCard
        {
            get { return selectedCard; }
            set
            {
                selectedCard = value;
                OnPropertyChanged();
            }
        }

        public int SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
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

	    public bool VisibleTabItem_Employee
		{
		    get
		    {
			    if (!IsRedactMode)
				    return true;
				else if (CurrentItem.OrganizationIsMaster)
				    return false;
			    else
				    return true;
		    }
	    }

	    public bool EditingVisitorCommentMode
	    {
		    get { return _editingVisitorCommentMode; }
		    set
		    {
			    _editingVisitorCommentMode = value;
				OnPropertyChanged(nameof(EditingVisitorCommentMode));
		    }
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
        public ICommand OpenOrderCommand { get; set; }

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
	    public ICommand EditVisitorCommentCommand { get; set; }
	    public ICommand EndVisitorCommentCommand { get; set; }

		public ICommand RefreshCommand { get; set; }

        private ChildWindowSettings _windowSettings;
        public ChildWindowSettings WindowSettings
        {
            get { return _windowSettings; }
            set { _windowSettings = value; OnPropertyChanged("WindowSettings"); }
        }

	    /// <summary>
	    /// Сканер документов.
	    /// </summary>
	    private readonly CDocumentScaner _documentScaner = CDocumentScaner.GetInstance();

		public ChildWinSet WinSet { get; set; }

	    public VisitsViewModel(IWindow view)
	    {
		    WinSet = new ChildWinSet() {Left = 0};
		    //GlobalSettings.GetChildWindowSettings();     

		    this.view = view;
		    Model = new VisitsModel();

		    this.PositionList =
			    new CollectionView(Model.CurrentItem != null
				    ? VisitorsHelper.GetPositions(
					    Model.CurrentItem.Position)
				    : new List<string>());

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
            OpenOrderCommand = new RelayCommand(obj => OpenOrder());

            AddImageSourceCommand = new RelayCommand(arg => AddImageSource(ImageType.Photo, _nameDocument_PhotoImageType));
            RemoveImageSourceCommand= new RelayCommand(arg => RemoveImageSource(ImageType.Photo, _nameDocument_PhotoImageType));
            AddSignatureCommand = new RelayCommand(arg => AddImageSource(ImageType.Signature, _nameDocument_SignatureImageType));
            RemoveSignatureCommand = new RelayCommand(arg => RemoveImageSource(ImageType.Signature, _nameDocument_SignatureImageType));
		    OkCommand = new RelayCommand(arg => Ok());
		    CancelCommand = new RelayCommand(arg => Cancel());
		    EditCommand = new RelayCommand(arg => Edit());
		    FindCommand = new RelayCommand(arg => Find());

		    OpenDocumentCommand = new RelayCommand(arg => OpenDocument());
			AddDocumentCommand = new RelayCommand(arg => AddDocument());
		    EditDocumentCommand = new RelayCommand(arg => EditDocument());
		    RemoveDocumentCommand = new RelayCommand(arg => RemoveDocument());

		    OpenMainDocumentCommand = new RelayCommand(arg => OpenMainDocument());
		    AddMainDocumentCommand = new RelayCommand(arg => AddMainDocument());
		    EditMainDocumentCommand = new RelayCommand(arg => EditMainDocument());
		    RemoveMainDocumentCommand = new RelayCommand(arg => RemoveMainDocument());


		    EditVisitorCommentCommand = new RelayCommand(arg => ActivateEditingVisitorComment());
		    EndVisitorCommentCommand = new RelayCommand(arg => SavingEditedVisitorComment());

			RefreshCommand = new RelayCommand(arg => Refresh());

            _documentScaner.ScanFinished += Scaner_ScanFinished;
	    }

	    ~VisitsViewModel()
	    {
			if(_documentScaner!=null)
				_documentScaner.ScanFinished -= Scaner_ScanFinished;
			if(_documentScaner!=null)
				_documentScaner?.Dispose();
	    }

        private void Refresh()
        {
            Model = new VisitsModel();
        }

	    /// <summary>
	    /// Завершение сканирования.
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void Scaner_ScanFinished(object sender, RegulaLib.Events.ScanFinishedEventArgs e)
	    {
		    if (model.ButtonEnable && CurrentItem != null)
		    {
			    RegulaView regulaView = null;
			    (view as Window).Invoke(() =>
			    {
				    regulaView = new RegulaView(e.Person);
				    regulaView?.ShowDialog();
			    });


			    if (regulaView?.Result ?? false)
			    {
				    AddMainDocumentFromScan(e.Person);
				    AddPortraitAndSignatureFromScan(e.Person);
				    FillCurrentItemFieldsFromScan(e.Person);

				    OnPropertyChanged(nameof(CurrentItem));
			    }
		    }
	    }

	    private void AddPortraitAndSignatureFromScan(CPerson person)
	    {
		    AddImageSource(ImageType.Photo,null,person);
		}

	    /// <summary>
	    /// Добавление отсканированного документа.
	    /// </summary>
	    /// <param name="person"></param>
	    private void AddMainDocumentFromScan(CPerson person)
	    {
		    var document = new VisitorsMainDocument
		    {
			    Num = person.DocumentNumber?.Value,
			    Seria = person.DocumentSeria?.Value,
			    Code = person.DocumentDeliveryPlaceCode?.Value,
			    Images = GetScansByDocNumber(person, person.DocumentNumber?.Value)
		    };

		    try
		    {
			    document.Date = DateTime.Parse(person.DocumentDeliveryDate?.Value);
			    document.DateTo = DateTime.Parse(person.DocumentDateOfExpiry?.Value);
			    document.BirthDate = DateTime.Parse(person.DateOfBirth?.Value);
		    }
		    catch (Exception)
		    {
			    //
		    }

		    document.Comment += GetDocumentComment(person);

		    //проверка на наличие документа в списке документов CurrentItem
		    var isContains = false;
		    for (var index = 0; index < CurrentItem.MainDocuments.Count; index++)
		    {
			    if (string.IsNullOrEmpty(CurrentItem.MainDocuments[index]?.Num?.Trim())
			        || string.IsNullOrEmpty(document.Num?.Trim())
			        || string.Equals(CurrentItem.MainDocuments[index]?.Num?.Trim(), document.Num?.Trim()))
			    {
				    isContains = true;
				    (view as Window)?.Invoke(() => { CurrentItem.MainDocuments[index] = document; });
			    }
		    }

		    if (!isContains)
		    {
			    (view as Window)?.Invoke(() => { Model.AddMainDocument(document); });
		    }
	    }

	    /// <summary>
	    /// Сканы документа по номеру.
	    /// </summary>
	    private List<Guid> GetScansByDocNumber(CPerson person, string number)
	    {
		    var result = new List<Guid>();
		    if (person?.PagesScanHash!=null && person.PagesScanHash.Count != 0)
		    {
				    foreach (var key in person.PagesScanHash.Keys)
				    {
					    if (key.Contains(number.Trim().ToLower()))
					    {
						    var page = person.PagesScanHash[key];
						    result.Add(ImagesHelper.GetGuidFromByteArray(page));
					    }
				    }
		    }

		    return result;
	    }

	    /// <summary>
	    /// заполнение примечания документа в случае измеения ФИО.
	    /// </summary>
	    private string GetDocumentComment(CPerson person)
	    {
		    //если изменились ФИО, то добавить примечание
		    var isNameDifferent = !string.IsNullOrEmpty(CurrentItem.Name) &&
		                          !string.Equals(CurrentItem.Name?.Trim().ToLower(), person.Name?.Value.Trim().ToLower());
		    var isSurnameDifferent = !string.IsNullOrEmpty(CurrentItem.Family) &&
		                             !string.Equals(CurrentItem.Family?.Trim().ToLower(), person.Surname?.Value.Trim().ToLower());
		    var isPatronomycDifferent = !string.IsNullOrEmpty(CurrentItem.Patronymic) &&
		                                !string.Equals(CurrentItem.Patronymic?.Trim().ToLower(),
			                                person.Patronymic?.Value.Trim().ToLower());
		    var isChange = isNameDifferent || isSurnameDifferent || isPatronomycDifferent;
		    var str = "";
		    if (isChange)
		    {
			   str = "\nпрошлые поля: ";
			    if (isNameDifferent)
			    {
				    str += "имя: " + CurrentItem.Name + ", ";
			    }

			    if (isSurnameDifferent)
			    {
				    str += "фамилия: " + CurrentItem.Family + ", ";
			    }

			    if (isSurnameDifferent)
			    {
				    str += "отчество: " + CurrentItem.Patronymic + ", ";
			    }
		    }

		    return str;
	    }


	    /// <summary>
	    /// Заполнение полей CurrentItem из скана документа.
	    /// </summary> 
	    /// <param name="person"></param>
	    private void FillCurrentItemFieldsFromScan(CPerson person)
	    {
		    CurrentItem.Name = person.Name?.Value;
		    CurrentItem.Family = person.Surname?.Value;
		    CurrentItem.Patronymic = person.Patronymic?.Value;
		    CurrentItem.FullName = person.SurnmameAndName?.Value;
		    CurrentItem.BirthDate = person.DateOfBirth?.Value;
		    CurrentItem.DocType = person.DocumentClassCode?.Value;
		    CurrentItem.DocNum = person.DocumentNumber?.Value;
		    CurrentItem.Department = person.DocumentDeliveryPlace?.Value;
		    CurrentItem.DocCode = person.DocumentDeliveryPlaceCode?.Value;
		    CurrentItem.Person = person;

		    if (person.DocumentDeliveryDate?.Value != null)
		    {
			    try
			    {
				    CurrentItem.DocDate = DateTime.Parse(person.DocumentDeliveryDate?.Value);
			    }
			    catch (FormatException)
			    {
				    //
			    }
		    }
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
			CurrentItem.OrganizationIsMaster = OrganizationsHelper.GetMasterParametr(result.Id, true);



			OnPropertyChanged("CurrentItem");
	        MoveNextFocusingElement?.Invoke("OrganizationsList");
	        OnPropertyChanged(nameof(VisibleTabItem_Employee));

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
		        {
			        CurrentItem.Comment = _bufer_CurrentItem_Comment;
			        EndEditingVisitorComment();
		        }
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
		    _documentScaner?.Connect();
		    Model = new NewVisitsModel();
		    IsRedactMode = true;
	    }


	    private void Extradite()
        {
            Base4ViewModel<Order> viewModel =
                new Base4ViewModel<Order>
                {
                    Model = new AddZoneModel(CurrentItem.Orders, CurrentItem.Id)
                };
            var window = new AddZone
            {
                DataContext = viewModel
            };
            viewModel.Model.OnClose += window.Handling_OnClose;
            window.ShowDialog();
        }

        private void Return()
        {
            var window = new ReturnBid()
            {
                DataContext = new ReturnBidViewModel(SelectedCard >= 0 ?
                    CurrentItem.Cards[SelectedCard] : null, CurrentItem.Id)
            };
            (window.DataContext as ReturnBidViewModel).OnClose += window.Handling_OnClose;

            window.ShowDialog();
        }

        private void OpenOrder()
        {
            if (SelectedOrder < 0)
            {
                return;
            }

            ViewManager.Instance.OpenWindow("BidsView");
        }

        private void Edit()
        {
	        _documentScaner?.Connect();
			Model = new EditVisitsModel(Set, CurrentItem);
	        IsRedactMode = true;
		}

        private void Find()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                @"VisitorsListWindViewOk", view) as BaseModelResult;
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

	            IsRedactMode = false;

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
                if (view.ParentWindow is SupRealClient.Views.VisitorsListWindView)
                    view.CloseWindow(new CancelEventArgs());
                else
                    Model = new VisitsModel(Set, ((EditVisitsModel)Model).OldVisitor);

            }

	        IsRedactMode = false;
		}

        private void AddImageSource(ImageType imageType, string name, CPerson person = null)
        {
	        if (person == null)
	        {
		        var dlg = new OpenFileDialog();
		        if (dlg.ShowDialog() == DialogResult.OK)
		        {
			        Model.AddImageSource(dlg.FileName, imageType);
			        AddImageToDocuments(name, dlg.FileName);
		        }

		        return;
	        }

	        if (Model is BaseVisitsModel baseModel)
	        {
		        if (person.Portrait != null)
		        {
			        baseModel.AddImageSource(person.Portrait, ImageType.Photo);
		        }

		        if (person.Signature != null)
		        {
			        baseModel.AddImageSource(person.Signature, ImageType.Signature);
		        }
	        }
        }

        private void RemoveImageSource(ImageType imageType, string name)
        {
            Model.RemoveImageSource(imageType);
	        RemoveImageToDocuments(name);
		}

        private void OpenDocument()
        {
            if (SelectedDocument < 0)
            {
                return;
            }

            if (EnableButton_OpenDocument_InRedactMode)
            {
                EditDocument();
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
			window._TestingNameVisitorsDocument += TestingNameVisitorsDocument;
			window.ShowDialog();
	        window._TestingNameVisitorsDocument -= TestingNameVisitorsDocument;
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
	        window._TestingNameVisitorsDocument += TestingNameVisitorsDocument;
			window.ShowDialog();
	        window._TestingNameVisitorsDocument -= TestingNameVisitorsDocument;
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

	        VisitorsDocument deleteItem = CurrentItem?.Documents?[SelectedDocument];
	        Model.RemoveDocument(SelectedDocument);
			if (deleteItem != null)
	        {
		        switch (deleteItem.Name)
		        {
					case _nameDocument_PhotoImageType:
						RemoveImageSource(ImageType.Photo, _nameDocument_PhotoImageType);
						break;
					case _nameDocument_SignatureImageType:
						RemoveImageSource(ImageType.Signature, _nameDocument_SignatureImageType);
						break;
				}
			}

        }

        internal void OpenMainDocument()
        {
            if (SelectedMainDocument < 0)
            {
                return;
            }
	    
            if (ButtonEnable)
            {
                EditMainDocument();
                return;
            }

            var window = new VisitorsMainDocumentView(
               new VisitorsMainDocumentModel(
                   CurrentItem.MainDocuments[SelectedMainDocument]), false, CurrentItem.Person);
            window.ShowDialog();
        }

        private void AddMainDocument()
        {
            var window = new VisitorsMainDocumentView(
                new VisitorsMainDocumentModel(null), true,null);
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
                    CurrentItem.MainDocuments[SelectedMainDocument]), true, CurrentItem.Person);
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

	    private void ActivateEditingVisitorComment()
	    {
		    if (!IsRedactMode)
		    {
			    EditingVisitorCommentMode = true;
			    _bufer_CurrentItem_Comment = CurrentItem.Comment;
			    CommentTextEnable = true;
			}
		    else
		    {
			    EditingVisitorCommentMode = false;
			}
		}

	    private void SavingEditedVisitorComment()
	    {
		    _bufer_CurrentItem_Comment = CurrentItem.Comment;
		    Model.Ok();

		    EndEditingVisitorComment();
	    }

		private void EndEditingVisitorComment()
	    {
			EditingVisitorCommentMode = false;
		    CommentTextEnable = false;
	    }



	    private void AddImageToDocuments(string name,  string fileName)
	    {
		    RemoveImageToDocuments(name);

			Guid id = ImagesHelper.LoadImage(fileName);
			VisitorsDocument visitorsDocument = new VisitorsDocument()
			{
				Name = name,
				TypeId = 0,
				Images = new List<Guid>() {id},
				IsChanged = true
			};

			CurrentItem.Documents.Add(visitorsDocument);
		}

	    private void RemoveImageToDocuments(string name)
	    {
		    if (CurrentItem?.Documents?.Count <= 0)
		    {
			    return;
		    }

		    VisitorsDocument deleteItem = CurrentItem?.Documents?.FirstOrDefault(item => item.Name == name);
		    CurrentItem?.Documents?.Remove(deleteItem);
	    }

	    private void Change_ButtonEnable()
	    {
		    if (Model is NewVisitsModel || Model is EditVisitsModel)
		    {
				// Здесь учитывается инверсия значения переменное "ButtonEnable" при использовании классов NewVisitsModel или EditVisitsModel
				// Поэтому когда нужно активировать кнопку ButtonEnable = false
				// Поэтому когда нужно заблокиравать кнопку ButtonEnable = true
				if (SelectedDocument < 0)
			    {
				    EnableButton_OpenDocument_InRedactMode = false;
				    return;
			    }

				#region НЕ ИСПОЛЬЗУЕТСЯ Модуль блокирования кнопки "Просмотр", если в списке прикрепленных сканов выбраны пункты "Личная фотография" или "Личная подпись"
				//   VisitorsDocument deleteItem = CurrentItem?.Documents?[SelectedDocument];
				//   if (deleteItem != null)
				//   {
				//    if (deleteItem.Name != _nameDocument_PhotoImageType && deleteItem.Name != _nameDocument_SignatureImageType)
				//    {
				//	    ButtonEnable = false;
				//	    return;
				//    }
				//	 }
				// 
				// ButtonEnable = true;
				#endregion

			    EnableButton_OpenDocument_InRedactMode = true;
			}
		}

		#region Realization events

	    private void TestingNameVisitorsDocument(object sender, CancelEventArgs e)
	    {
		    if (sender is VisitorsDocumentViewModel)
			{
				VisitorsDocumentViewModel visitorsDocumentViewModel = sender as VisitorsDocumentViewModel;
				if (Model is NewVisitsModel)
			    {
				

				    if (visitorsDocumentViewModel.Name == _nameDocument_PhotoImageType)
				    {
					    e.Cancel = false;
					    if (PhotoSource != "")
						    MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется");
					    else
						    MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
						                    " невозможно добавить, так как данное название используется только для документа, содержащий личную фотографию");
					    return;
				    }

				    if (visitorsDocumentViewModel.Name == _nameDocument_SignatureImageType)
				    {
					    e.Cancel = false;
					    if (Signature != "")
						    MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется");
					    else
						    MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
						                    " невозможно добавить, так как данное название используется только для документа, содержащий скан личной подписи");
					    return;
				    }

				    VisitorsDocument findingItem =
					    CurrentItem?.Documents?.FirstOrDefault(item => item.Name == visitorsDocumentViewModel.Name);
				    if (findingItem != null)
				    {
					    e.Cancel = false;
					    MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется");
				    }
			    }
			    else
			    {
				    if (Model is EditVisitsModel)
					{
						if (visitorsDocumentViewModel.Name == _nameDocument_PhotoImageType)
						{
								MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
								                " невозможно добавить, так как данное название используется только для документа, содержащий личную фотографию");
							return;
						}

						if (visitorsDocumentViewModel.Name == _nameDocument_SignatureImageType)
						{
								MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
								                " невозможно добавить, так как данное название используется только для документа, содержащий скан личной подписи");
							return;
						}

					}
			    }


		    }


	    }

		#endregion

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
	    bool CommentTextEnable { get; set; }
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
            CardsWrapper.CurrentTable().OnChanged += UpdateCards;
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

	    public virtual bool CommentTextEnable
	    {
		    get { return true; }
		    set { }
	    }

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

	    /// <summary>
	    /// Загрузка портрета и подписи со сканера.
	    /// </summary>
	    public void AddImageSource(byte[] image, ImageType imageType)
	    {
		    SetImageSource(ImagesHelper.GetGuidFromByteArray(image), imageType);
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

        protected virtual void UpdateCards() { }

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

	        if (!CurrentItem.IsAgree)
	        {
		        MessageBox.Show("Нет согласия на обработку персональных данных!");
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

		/// <summary>
		/// Метод для потверждения проведения сохранения данных. Различие от "Validate" в том, что если согласия на обработку персональных данных нет, запрашивается подтверждение записи.
		/// </summary>
		/// <returns></returns>
		protected bool? Validate_AndUse_IsAgree()
	    {
		    if (string.IsNullOrEmpty(CurrentItem.Family) ||
		        string.IsNullOrEmpty(CurrentItem.Name) ||
		        string.IsNullOrEmpty(CurrentItem.Patronymic) ||
		        string.IsNullOrEmpty(CurrentItem.Organization))
		    {
			    MessageBox.Show("Не все поля заполнены корректно!");
			    return false;
		    }

		    if (!CurrentItem.IsAgree)
		    {
			    DialogResult result =
				    MessageBox.Show(
					    "Нет согласия на обработку персональных данных! " + Environment.NewLine +
					    "При подтвердении сохранения будут сохранены ФИО посетителя и название организации", "Внимание",
					    MessageBoxButtons.OKCancel);

			    if (result == DialogResult.OK)
			    {
				    return null;
			    }
			    else
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

	    protected bool Validate_BaseData()
	    {
		    if (string.IsNullOrEmpty(CurrentItem.Family) ||
		        string.IsNullOrEmpty(CurrentItem.Name) ||
		        string.IsNullOrEmpty(CurrentItem.Patronymic) ||
		        string.IsNullOrEmpty(CurrentItem.Organization))
		    {
			    MessageBox.Show("Не все поля заполнены корректно!");
			    return false;
		    }

		    return true;
	    }
		
	    protected bool? Validate_IsAgreeValue()
	    {
		    if (!CurrentItem.IsAgree)
		    {
			    DialogResult result =
				    MessageBox.Show(
					    "Нет согласия на обработку персональных данных! " + Environment.NewLine +
					    "При подтвердении сохранения будут сохранены ФИО посетителя и название организации", "Внимание",
					    MessageBoxButtons.OKCancel);
			    if (result == DialogResult.OK)
			    {
				    return null;
			    }
				else
				    return false;
		    }

		    return true;
	    }

	    protected bool Validate_FormularValue()
	    {
		    if (!CurrentItem.IsNotFormular &
		        (string.IsNullOrEmpty(CurrentItem.Telephone) ||
		         string.IsNullOrEmpty(CurrentItem.Nation) ||
		         !CurrentItem.MainDocuments.Any()))
		    {
			    MessageBox.Show("Не все поля вкладки Основная заполнены!");
			    return false;
		    }

		    return true;
	    }

	    protected bool Validate_DocumentDates()
	    {
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
	    private bool _textEnable = false;
	    private bool _сommentTextEnable = false;

		public override event Action<object> OnClose;
		public int selectedIndex { get; set; }


		public override bool TextEnable
        {
            get { return _textEnable; }
			set { _textEnable = value; }
		}

        public override bool ButtonEnable
        {
            get { return false; }
	        set {  }
        }

        public override bool AccessVisibility
        {
            get { return true; }
            set { }
        }

		public override bool CommentTextEnable
		{
			get { return _сommentTextEnable; }
			set { _сommentTextEnable = value; }
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

        protected override void UpdateCards()
        {
            int index = Set.IndexOf(CurrentItem);
            if (index >= 0)
            {
                OrdersCardsToVisitor(index);
            }
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
                    OrganizationId = visitors.Field<int>("f_org_id"),
                    Organization = OrganizationsHelper.
                                   GenerateFullName(visitors.Field<int>("f_org_id"), true),
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
                            Passes = OrdElem.Field<string>("f_passes"),
                            OrderElements = new ObservableCollection<OrderElement>(
                                from row in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
                                where row.Field<int>("f_ord_id") == Ord.Field<int>("f_ord_id") &&
                                CommonHelper.NotDeleted(row)
                                select new OrderElement(
                                    (OrderType)Ord.Field<int>("f_order_type_id") == OrderType.Single)
                                {
                                    Id = row.Field<int>("f_oe_id"),
                                    OrderId = row.Field<int>("f_ord_id"),
                                    VisitorId = row.Field<int>("f_visitor_id"),
                                    OrganizationId = row.Field<int?>("f_org_id"),
                                    Position = row.Field<string>("f_position"),
                                    CatcherId = row.Field<int>("f_catcher_id"),
                                    From = row.Field<DateTime>("f_time_from"),
                                    To = row.Field<DateTime>("f_time_to"),
                                    IsDisable = row.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
                                    Passes = row.Field<string>("f_passes"),
                                    IsBlock = CommonHelper.StringToBool(VisitorsWrapper.CurrentTable().Table.AsEnumerable().
                                        Where(item => item.Field<int>("f_visitor_id") == row.Field<int>("f_visitor_id")).
                                        FirstOrDefault().Field<string>("f_persona_non_grata")),
                                    IsCardIssued = true,
                                    Reason = row.Field<string>("f_other_org"),
                                    TemplateIdList = row.Field<string>("f_oe_templates"),
                                    AreaIdList = row.Field<string>("f_oe_areas"),
                                    ScheduleId = row.Field<int>("f_schedule_id"),
                                    Schedule = row.Field<int>("f_schedule_id") == 0 ? "" :
                                        SchedulesWrapper.CurrentTable()
                                        .Table.AsEnumerable().FirstOrDefault(
                                            arg => arg.Field<int>("f_schedule_id") ==
                                            row.Field<int>("f_schedule_id"))["f_schedule_name"].ToString(),
                        })
                        });
            }
            if (Set[index].Cards == null)
            {
                Set[index].Cards = new ObservableCollection<Card2>(
                    from card in CardsWrapper.CurrentTable().Table.AsEnumerable()
                    join visit in VisitsWrapper.CurrentTable().Table.AsEnumerable()
                    on new { a = card.Field<int>("f_object_id_hi"), b = card.Field<int>("f_object_id_lo") }
                    equals new { a = visit.Field<int>("f_card_id_hi"), b = visit.Field<int>("f_card_id_lo") }
                    where visit.Field<int>("f_visitor_id") == Set[index].Id &&
                    visit.Field<string>("f_deleted") == "N"
                    select new Card2
                    {
                        Card = card.Field<string>("f_card_name"),
                        CardNumber = card.Field<int>("f_card_num").ToString(),
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
			//if (OldVisitor.Family != CurrentItem.Family)
			//{
			//	row["f_family"] = CurrentItem.Family;
			//	fullNameChanged = true;
			//}
			//if (OldVisitor.Name != CurrentItem.Name)
			//{
			//	row["f_fst_name"] = CurrentItem.Name;
			//	fullNameChanged = true;
			//}
			//if (OldVisitor.Patronymic != CurrentItem.Patronymic)
			//{
			//	row["f_sec_name"] = CurrentItem.Patronymic;
			//	fullNameChanged = true;
			//}
			//if (fullNameChanged)
			//{
			//	row["f_full_name"] = CommonHelper.CreateFullName(
			//		CurrentItem.Family, CurrentItem.Name, CurrentItem.Patronymic);
			//}
			//if (OldVisitor.OrganizationId != CurrentItem.OrganizationId)
			//	row["f_org_id"] = CurrentItem.OrganizationId >= 0 ?
			//		CurrentItem.OrganizationId : 0;
			//if (OldVisitor.Comment != CurrentItem.Comment)
				row["f_vr_text"] = CurrentItem.Comment;
			//if (OldVisitor.IsAccessDenied != CurrentItem.IsAccessDenied)
			//{
			//	row["f_persona_non_grata"] =
			//		CommonHelper.BoolToString(CurrentItem.IsAccessDenied);
			//}
			//if (OldVisitor.IsCanHaveVisitors != CurrentItem.IsCanHaveVisitors)
			//{
			//	row["f_can_have_visitors"] =
			//		CommonHelper.BoolToString(CurrentItem.IsCanHaveVisitors);
			//}
			//if (OldVisitor.IsAgree != CurrentItem.IsAgree)
			//{
			//	row["f_personal_data_agreement"] =
			//		CommonHelper.BoolToString(CurrentItem.IsAgree);
			//}
			//if (OldVisitor.AgreeToDate != CurrentItem.AgreeToDate)
			//{
			//	row["f_personal_data_last_date"] = CurrentItem.AgreeToDate;
			//}
			//if (OldVisitor.Telephone != CurrentItem.Telephone)
			//	row["f_phones"] = CurrentItem.Telephone;
			//if (OldVisitor.Nation != CurrentItem.Nation)
			//	row["f_cntr_id"] = CurrentItem.NationId >= 0 ?
			//		CurrentItem.NationId : 0;
			//if (OldVisitor.DocType != CurrentItem.DocType)
			//	row["f_doc_id"] = CurrentItem.DocumentId >= 0 ?
			//		CurrentItem.DocumentId : 0;
			//if (OldVisitor.DocSeria != CurrentItem.DocSeria)
			//	row["f_doc_seria"] = CurrentItem.DocSeria;
			//if (OldVisitor.DocNum != CurrentItem.DocNum)
			//	row["f_doc_num"] = CurrentItem.DocNum;
			//if (OldVisitor.DocDate != CurrentItem.DocDate)
			//	row["f_doc_date"] = CurrentItem.DocDate;
			//if (OldVisitor.DocCode != CurrentItem.DocCode)
			//	row["f_doc_code"] = CurrentItem.DocCode;
			//if (OldVisitor.DocPlace != CurrentItem.DocPlace)
			//	row["f_doc_org"] = CurrentItem.DocPlace;
			//if (OldVisitor.Position != CurrentItem.Position)
			//	row["f_job"] = CurrentItem.Position;
			//if (OldVisitor.IsRightSign != CurrentItem.IsRightSign)
			//{
			//	row["f_can_sign_orders"] =
			//		CommonHelper.BoolToString(CurrentItem.IsRightSign);
			//}
			//if (OldVisitor.IsAgreement != CurrentItem.IsAgreement)
			//{
			//	row["f_can_adjust_orders"] =
			//		CommonHelper.BoolToString(CurrentItem.IsAgreement);
			//}
			//if (OldVisitor.Cabinet != CurrentItem.Cabinet)
			//{
			//	row["f_cabinet_id"] = CurrentItem.CabinetId >= 0 ?
			//		CurrentItem.CabinetId : 0;
			//}
			//if (OldVisitor.Department != CurrentItem.Department)
			//{
			//	row["f_dep_id"] = CurrentItem.DepartmentId >= 0 ?
			//		CurrentItem.DepartmentId : 0;
			//}
			row.EndEdit();

			SaveAdditionalData(CurrentItem.Id);

			return true;
		}
	}

    public class NewVisitsModel : BaseVisitsModel
	{
		private bool _buttonEnable = true;
		public override event Action<object> OnClose;

        public override bool TextEnable
        {
            get { return true; }
            set { }
        }

        public override bool ButtonEnable
        {
            get { return _buttonEnable; }
	        set { _buttonEnable = value; }
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
	        ButtonEnable = true;
			Set = new ObservableCollection<EnumerationClasses.Visitor>();
            CurrentItem = new EnumerationClasses.Visitor() {AgreeToDate=DateTime.Now };
            CurrentItem.MainDocuments = new ObservableCollection<VisitorsMainDocument>();
            CurrentItem.Documents = new ObservableCollection<VisitorsDocument>();
            Set.Add(CurrentItem);
        }
        
        public override bool Ok()
        {
	        bool? validate = Validate_AndUse_IsAgree();

			if (validate!=null && !validate.Value)
            {
                return false;
            }

			if (validate == null)
	        {


				//DataRow row = VisitorsWrapper.CurrentTable().Table.NewRow();

				//row["f_rec_date_pass"] = DateTime.Now;
				//row["f_is_short_data"] = CommonHelper.BoolToString(false);
				//row["f_rec_operator_pass"] = Authorizer.AppAuthorizer.Id;
				//row["f_full_name"] = CommonHelper.CreateFullName(CurrentItem.Family,
				//	CurrentItem.Name, CurrentItem.Patronymic);

				//row["f_deleted"] = CommonHelper.BoolToString(false);
				//row["f_rec_date"] = DateTime.Now;
				//row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;

				//row["f_family"] = CurrentItem.Family;
				//row["f_fst_name"] = CurrentItem.Name;
				//row["f_sec_name"] = CurrentItem.Patronymic;


				//row["f_org_id"] = CurrentItem.OrganizationId >= 0 ?
				//	CurrentItem.OrganizationId : 0;






				//      row["f_vr_text"] = "";

				//      row["f_persona_non_grata"] =
				//       CommonHelper.BoolToString(false);
				//      row["f_can_have_visitors"] =
				//       CommonHelper.BoolToString(false);
				//      row["f_personal_data_agreement"] =
				//       CommonHelper.BoolToString(CurrentItem.IsAgree);
				//      row["f_personal_data_last_date"] = CurrentItem.AgreeToDate;
				//      row["f_birth_date"] = Convert.ToDateTime(CurrentItem.BirthDate);
				//      row["f_phones"] ="";
				//      row["f_cntr_id"] = 0;
				//      row["f_doc_id"] = 0;
				//      row["f_doc_seria"] = "";
				//      row["f_doc_num"] = "";
				//      row["f_doc_date"] = CurrentItem.DocDate;
				//      row["f_doc_code"] = CurrentItem.DocCode;
				//      row["f_doc_org"] = "";
				//      row["f_job"] = "";
				//      row["f_can_sign_orders"] =
				//       CommonHelper.BoolToString(false);
				//      row["f_can_adjust_orders"] =
				//       CommonHelper.BoolToString(false);

				//      row["f_dep_id"] = 0;
				//      row["f_cabinet_id"] = 0;
				//      VisitorsWrapper.CurrentTable().Table.Rows.Add(row);

				//      SaveAdditionalData((int)row["f_visitor_id"]);



				//OnClose?.Invoke(CurrentItem);
				//return true;


			        CurrentItem.Comment = "";
			        CurrentItem.IsAccessDenied = false;
			        CurrentItem.IsCanHaveVisitors = false;
			        CurrentItem.Telephone = "";
			        CurrentItem.NationId = 0;
			        CurrentItem.DocumentId = 0;
			        CurrentItem.DocSeria = "";
			        CurrentItem.DocNum = "";
			        CurrentItem.DocDate = DateTime.MinValue;
			        CurrentItem.DocCode = "";
			        CurrentItem.DocPlace = "";
			        CurrentItem.Position = "";
			        CurrentItem.IsRightSign = false;
			        CurrentItem.IsAgreement = false;
			        CurrentItem.CabinetId = 0;
			        CurrentItem.DepartmentId = 0;

		        for (int index = 0; index < CurrentItem.MainDocuments.Count; index++)
		        {
			        this.RemoveMainDocument(0);
				}

		        for (int index = 0; index < CurrentItem.Documents.Count; index++)
		        {
			        this.RemoveDocument(0);
		        }

		        this.RemoveImageSource(ImageType.Photo);
		        this.RemoveImageSource(ImageType.Signature);

				CurrentItem.MainDocuments.Clear();
		        CurrentItem.Documents.Clear();

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
		        row["f_org_id"] = CurrentItem.OrganizationId >= 0 ? CurrentItem.OrganizationId : 0;
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
		        row["f_cntr_id"] = CurrentItem.NationId >= 0 ? CurrentItem.NationId : 0;
		        row["f_doc_id"] = CurrentItem.DocumentId >= 0 ? CurrentItem.DocumentId : 0;
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

		        row["f_dep_id"] = CurrentItem.DepartmentId >= 0 ? CurrentItem.DepartmentId : 0;
		        row["f_cabinet_id"] = CurrentItem.CabinetId >= 0 ? CurrentItem.CabinetId : 0;
		        VisitorsWrapper.CurrentTable().Table.Rows.Add(row);

		        SaveAdditionalData((int) row["f_visitor_id"]);
		        OnClose?.Invoke(CurrentItem);
		        return true;
        }
    }

    public class EditVisitsModel : BaseVisitsModel
    {
	    private bool _buttonEnable = true;
		public EnumerationClasses.Visitor OldVisitor { get; set; }

        public override bool TextEnable
        {
            get { return true; }
            set { }
        }

        public override bool ButtonEnable
        {
            get { return _buttonEnable; }
	        set { _buttonEnable = value; }
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
		    CurrentItem = (EnumerationClasses.Visitor) visitor?.Clone();
		    OldVisitor = visitor;
	    }

	    public override bool Ok()
        {
			bool? validate = Validate_AndUse_IsAgree();

			if (validate != null && !validate.Value)
			{
				return false;
			}

	        if (validate == null)
	        {
		        CurrentItem.Comment = "";
		        CurrentItem.IsAccessDenied = false;
		        CurrentItem.IsCanHaveVisitors = false;
		        CurrentItem.Telephone = "";
		        CurrentItem.NationId = 0;
		        CurrentItem.DocumentId = 0;
		        CurrentItem.DocSeria = "";
		        CurrentItem.DocNum = "";
		        CurrentItem.DocDate = DateTime.MinValue;
		        CurrentItem.DocCode = "";
		        CurrentItem.DocPlace = "";
		        CurrentItem.Position = "";
		        CurrentItem.IsRightSign = false;
		        CurrentItem.IsAgreement = false;
		        CurrentItem.CabinetId = 0;
		        CurrentItem.DepartmentId = 0;

		        for (int index = 0; index < CurrentItem.MainDocuments.Count; index++)
		        {
			        this.RemoveMainDocument(0);
		        }

		        for (int index = 0; index < CurrentItem.Documents.Count; index++)
		        {
			        this.RemoveDocument(0);
		        }

		        this.RemoveImageSource(ImageType.Photo);
		        this.RemoveImageSource(ImageType.Signature);

				CurrentItem.MainDocuments.Clear();
		        CurrentItem.Documents.Clear();
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
