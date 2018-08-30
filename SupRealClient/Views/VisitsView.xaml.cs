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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Models.Helpers;
using SupRealClient.TabsSingleton;
using SupRealClient.ViewModels;
using SupRealClient.Views.Visitor;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Text.RegularExpressions;
using log4net;
using Application = System.Windows.Application;

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
				var visit = (Visit)sender;

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
		private int selectedOrderElement = -1;
		private int selectedOrder = -1;
		public  const string _nameDocument_PhotoImageType = "Личная фотография";
		public const string _nameDocument_SignatureImageType = "Личная подпись";
		private bool _visibleButtonOpenDocument = false;
		private bool _enableButtonOpenDocument = false;
		private bool _enableButtonOpenMainDocument = false;
		private bool _isRedactMode = false;
		private bool _editingVisitorCommentMode = false;
		private string _bufer_CurrentItem_Comment = "";


		public event Action<string> MoveNextFocusingElement;
		public event Action<bool> RedactModeEvent;

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
				OnPropertyChanged(nameof(VisibleTabItem_Employee));
				OnPropertyChanged(nameof(CommentText));
				OnPropertyChanged(nameof(CommentTextEnable));
				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(Family));
				OnPropertyChanged(nameof(Patronymic));
			}
		}

	    public string Name
	    {
		    get { return CurrentItem?.Name; }
		    set
		    {
			    if (CurrentItem != null)
				    if (string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
					    CurrentItem.Name = value;
			    OnPropertyChanged(nameof(Name));
			}
		}

		public string Family
		{
		    get { return CurrentItem?.Family; }
		    set
		    {
			    if (CurrentItem != null)
				    if (string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
					    CurrentItem.Family = value;
				OnPropertyChanged(nameof(Family));
		    }
		}

		public string Patronymic
		{
		    get { return CurrentItem?.Patronymic; }
		    set
		    {
			    if (CurrentItem != null)
				    if (string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
					    CurrentItem.Patronymic = value;
				OnPropertyChanged(nameof(Patronymic));
			}
		}

	    public string BirthDate
	    {
		    get { return CurrentItem?.BirthDate; }
		    set
			{
				if (CurrentItem != null)
					CurrentItem.BirthDate = value;
				OnPropertyChanged(nameof(BirthDate));
			}
		}
		/// <summary>
		/// Объект со списком свойств Enable для кнопок
		/// </summary>
		public VisitorsEnableOrVisible VisitorsEnable
        {
            get { return Model?.VisitorsEnable; }
            set
            {
				if(Model!=null)
					Model.VisitorsEnable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Объект со списком свойтсв Visible для кнопок
        /// </summary>
        public VisitorsEnableOrVisible VisitorsVisible
        {
            get { return Model?.VisitorsVisible; }
            set
			{
				if (Model != null)
					Model.VisitorsVisible = value;
                OnPropertyChanged();
            }
        }

        public bool TextEnable
        {
            get {
				return Model.TextEnable; }
            set
            {
                Model.TextEnable = value;
                OnPropertyChanged();
            }
        }

		public string CommentText
		{
			get { return Model?.CurrentItem?.Comment; }
			set
			{
				if(Model!=null && Model.CurrentItem!=null)
					Model.CurrentItem.Comment = value;
				OnPropertyChanged(nameof(CommentText));
			}
		}

		public bool CommentTextEnable
		{
			get { return Model.CommentTextEnable; }
			set
			{
				Model.CommentTextEnable = value;
				OnPropertyChanged(nameof(CommentTextEnable));
				OnPropertyChanged(nameof(CommentText));
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
				Update_Fields();

				OnPropertyChanged(nameof(IsRedactMode));
				OnPropertyChanged(nameof(VisibleTabItem_Employee));
				OnPropertyChanged(nameof(IsRedactMode_Inverce));
				OnPropertyChanged(nameof(OpeningButtons_ToRedactComments));
				OnPropertyChanged(nameof(IsNotFormular));
				EditingVisitorCommentMode = false;

				RedactModeEvent?.Invoke(_isRedactMode);
			}
		}

		public bool IsRedactMode_Inverce
		{
			get { return !_isRedactMode; }
		}

	    public bool OpeningButtons_ToRedactComments
	    {
		    get { return IsRedactMode_Inverce && CurrentItem!=null && CurrentItem.IsAgree; }
	    }

		//public bool VisibleButton_OpenDocument
		//{
		//	get { return CurrentItem?.Documents.Count > 0; }
		//}

		//   public bool VisibleButton_OpenMainDocument
		//   {
		//    get { return CurrentItem?.MainDocuments.Count > 0; }
		//   }

		//EnableButton_OpenDocument_InRedactMode
		public bool EnableButton_OpenDocument
		{
			//get => true;
			get { return CurrentItem?.Documents.Count > 0 && SelectedDocument >= 0; }
			//get { return _enableButtonOpenDocument; }
			//set
			//{
			// _enableButtonOpenDocument = value;
			// OnPropertyChanged(nameof(EnableButton_OpenDocument));
			//}
		}

		//EnableButton_OpenDocument_InRedactMode
		public bool EnableButton_OpenMainDocument
		{
			//get => true;
			get { return CurrentItem?.MainDocuments.Count > 0 && SelectedMainDocument >= 0; }
			//  get { return _enableButtonOpenMainDocument; }
			//  set
			//  {
			//_enableButtonOpenMainDocument = value;
			//   OnPropertyChanged(nameof(EnableButton_OpenMainDocument));
			//  }
		}
		public bool EnableList_Document
		{
			//get => true;
			get { return CurrentItem?.Documents.Count > 0; }
			//get { return _enableButtonOpenDocument; }
			//set
			//{
			// _enableButtonOpenDocument = value;
			// OnPropertyChanged(nameof(EnableButton_OpenDocument));
			//}
		}

		//EnableButton_OpenDocument_InRedactMode
		public bool EnableList_MainDocument
		{
			//get => true;
			get { return CurrentItem?.MainDocuments.Count > 0; }
			//  get { return _enableButtonOpenMainDocument; }
			//  set
			//  {
			//_enableButtonOpenMainDocument = value;
			//   OnPropertyChanged(nameof(EnableButton_OpenMainDocument));
			//  }
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


					OnPropertyChanged(nameof(Name));
					OnPropertyChanged(nameof(Family));
					OnPropertyChanged(nameof(Patronymic));
					OnPropertyChanged(nameof(CurrentItem));
					OnPropertyChanged(nameof(PhotoSource));
					OnPropertyChanged(nameof(Signature));
					OnPropertyChanged(nameof(BirthDate));
					OnPropertyChanged(nameof(VisibleTabItem_Employee));
					OnPropertyChanged(nameof(CommentTextEnable));
					OnPropertyChanged(nameof(CommentText));
					Update_Fields();

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
				Update_Fields();

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
				Update_Fields();

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

		public int SelectedOrderElement
		{
			get
			{
				return selectedOrderElement;
			}
			set
			{
				selectedOrderElement = value;
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
				//if (!IsRedactMode)
				// return true;
				//else 
			    return CurrentItem?.OrganizationIsBasic ?? true;
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

		public bool IsNotFormular
		{
			get => CurrentItem != null && CurrentItem.IsNotFormular;
			set
			{
				if (CurrentItem != null)
					CurrentItem.IsNotFormular = value;
				OnPropertyChanged(nameof(IsNotFormular));
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
		internal readonly CDocumentScaner DocumentScaner = CDocumentScaner.GetInstance();

		public ChildWinSet WinSet { get; set; }

		public VisitsViewModel(IWindow view)
		{
			WinSet = new ChildWinSet() { Left = 0 };
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
			RemoveImageSourceCommand =
				new RelayCommand(arg => RemoveImageSource(ImageType.Photo, _nameDocument_PhotoImageType));
			AddSignatureCommand =
				new RelayCommand(arg => AddImageSource(ImageType.Signature, _nameDocument_SignatureImageType));
			RemoveSignatureCommand =
				new RelayCommand(arg => RemoveImageSource(ImageType.Signature, _nameDocument_SignatureImageType));
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

			DocumentScaner.KillRegulaProc();
			DocumentScaner.ScanFinished += Scaner_ScanFinished;
		}

		~VisitsViewModel()
		{
			DocumentScanerDispose();
		}

		internal void DocumentScanerDispose()
		{
			DocumentScanerRemoveSubscription();
			DocumentScaner?.Dispose();
		}

		internal void DocumentScanerRemoveSubscription()
		{
			if (DocumentScaner != null)
			{
				DocumentScaner.ScanFinished -= Scaner_ScanFinished;
			}
		}

		private void Refresh()
		{
			if (CurrentItem != null)
			{
				int id = CurrentItem.Id;

				Model = new VisitsModel();
				Model.CurrentItem = Model.Find(id);
				CurrentItem = Model.CurrentItem;
			}
			else
			{
				Model = new VisitsModel();
				CurrentItem = Model.CurrentItem;
			}

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
					CommonTextFieldsSelectView textFieldsSelectWindow = null;
					(view as Window).Invoke(() =>
					{
						textFieldsSelectWindow = new CommonTextFieldsSelectView();
						textFieldsSelectWindow?.ShowDialog();
					});

					if (textFieldsSelectWindow?.Result ?? false)
					{
						var fields = EFields.None;

						(view as Window)?.Invoke(() => { fields = textFieldsSelectWindow.FieldsForSubstitution; });

						FillCurrentItemFieldsFromScan(e.Person, fields.ToString().ToLower());
						AddPortraitAndSignatureFromScan(e.Person, fields.ToString().ToLower());
						AddMainDocumentFromScan(e.Person);
						AddDocument(e.Person);
						OnPropertyChanged(nameof(CurrentItem));
					}
				}
			}
		}

		private void AddPortraitAndSignatureFromScan(CPerson person, string fields)
		{
			AddImageSource(ImageType.Photo, null, person, fields.Contains("portrait"));
		}

		private void AddDocument(CPerson person)
		{
			//document
			var visitorDocument = new VisitorsDocument
			{
				Name =
					$"{CommonHelper.GetDocumentTypeInRussian(person?.DocumentType?.Value)} " +
					$"- Серия {person?.DocumentSeria?.Value}, № {person?.DocumentNumber?.Value}," +
					$" Дата выдачи {person?.DocumentDeliveryDate?.Value}",
				TypeId = 0,
				Images = GetScansByDocNumber(person, person?.DocumentNumber?.Value),
				IsChanged = true
			};

			//проверка на наличие документа в списке документов CurrentItem
			for (var index = 0; index < CurrentItem.Documents.Count; index++)
			{
				if (string.Equals(CurrentItem.Documents[index].Name.Trim().ToLower(),
					visitorDocument.Name.Trim().ToLower()))
				{
						(view as Window)?.Invoke(() =>
						{
								CurrentItem.Documents[index] = visitorDocument;
							
						});
					return;
				}
			}

			(view as Window)?.Invoke(() =>
			{
				if (!CurrentItem.Documents.Contains(visitorDocument))
				{
					Model.AddDocument(visitorDocument);
				}
			});
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
				Org = person.DocumentDeliveryPlace?.Value,
				Images = GetScansByDocNumber(person, person.DocumentNumber?.Value)
			};

			var documentType = CommonHelper.GetDocumentTypeInRussian(person.DocumentType?.Value);
			if (!string.IsNullOrEmpty(documentType))
			{
				ClientConnector connector = ClientConnector.CurrentConnector;
				var typeId = connector.GetDocumentTypeId(documentType, Authorizer.AppAuthorizer.Id);

				if (typeId!=0)
				{
					document.TypeId = typeId;
					document.Type = documentType;
				}
			}
			
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
					
					if (MessageBox.Show("Данный документ был ранее добавлен. Обновить данные?","Предупреждение", 
						MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
					{
						var i = index;
						(view as Window)?.Invoke(() =>
						{
							CurrentItem.MainDocuments[i] = document;
						});
					}
					
				}
			}

			if (!isContains)
			{
				(view as Window)?.Invoke(() =>
				{
					// Model.AddMainDocument(document);
					//костыль
					var window = new VisitorsMainDocumentView(
						new VisitorsMainDocumentModel(
							document), true, CurrentItem.Person);
					window.ShowDialog();
					var editDocument = window.WindowResult as VisitorsMainDocument;

					if (editDocument == null)
					{
						return;
					}
					Model.AddMainDocument(editDocument);
				});
			}
	    }
		
		/// <summary>
		/// Сканы документа по номеру.
		/// </summary>
		private List<Guid> GetScansByDocNumber(CPerson person, string number)
		{
			if (person == null)
			{
				return new List<Guid>();
			}

			var result = new List<Guid>();
			if (person?.PagesScanHash != null && person.PagesScanHash.Count != 0)
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
		private void FillCurrentItemFieldsFromScan(CPerson person, string fields)
		{
			if (fields.Contains("surname"))
			{
				Family = person.Surname?.Value;
			}

			if (fields.Contains("name"))
			{
				Name = person.Name?.Value;
			}

			if (fields.Contains("patronymic"))
			{
				Patronymic = person.Patronymic?.Value;
			}

			if (fields.Contains("birthdate"))
			{
				BirthDate = person.DateOfBirth?.Value;
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
			    "MainOrganisationStructureViewOk", view) as BaseModelResult;
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
                "Base4OrganizationsLargeWindViewOk", view) as BaseModelResult;
			if (result == null)
			{
				return;
			}
			CurrentItem.OrganizationId = result.Id;
			CurrentItem.Organization = OrganizationsHelper.
			    GenerateFullName(result.Id, true);
			CurrentItem.OrganizationIsBasic = OrganizationsHelper.GetBasicParametr(result.Id, true);



			OnPropertyChanged("CurrentItem");
			MoveNextFocusingElement?.Invoke("OrganizationsList");
			OnPropertyChanged(nameof(VisibleTabItem_Employee));

		}

		private void CountyList()
		{
			//var result = ViewManager.Instance.OpenWindowModal(
			//    "Base4NationsWindViewOk", view) as BaseModelResult;
			var result = ViewManager.Instance.OpenWindowModal(
	"Base4СitizenshipWindViewOk", view) as BaseModelResult;
			
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
					CurrentItem.OrganizationIsBasic = true;
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

			OnPropertyChanged(nameof(VisibleTabItem_Employee));
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
			if (DocumentScaner!=null)
			{
				DocumentScanerRemoveSubscription();
				DocumentScaner.ScanFinished += Scaner_ScanFinished;
			}

			DocumentScaner?.Connect();
			Model = new NewVisitsModel();
			IsRedactMode = true;
		}


        private void Extradite()
        {
            if (CurrentItem.Cards.Any(c => c.StateId == (int)CardState.Lost))
            {
                MessageBox.Show("У посетителя есть утерянные пропуска", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int result = 1;
            if (CurrentItem.Cards.Any(c => c.StateId == (int)CardState.Issued))
            {
                var data = new IssuedCardsMessageBoxViewModel(CurrentItem.Family);
                var view = new IssuedCardsMessageBoxView(data);
                view.ShowDialog();
                result = data.Result;
            }
            if (result == 0)
            {
                return;
            }
            if (result == 2)
            {
                var data = new DeactivateCardsViewModel(
                    CurrentItem.Cards.Where(c => c.StateId == (int)CardState.Issued).ToList());
                var view = new DeactivateCardsView(data);
                view.ShowDialog();
            }

            Base4ViewModel<Order> viewModel =
                new AddZoneViewModel
                {
                    Model = new AddZoneModel(CurrentItem.Orders, CurrentItem.Id)
                };
            var window = new AddZone
            {
                DataContext = viewModel
            };
            viewModel.Model.OnClose += window.Handling_OnClose;
            window.ShowDialog();
            Refresh();
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
			Refresh();
		}

		private void OpenOrder()
		{
			if (SelectedOrder < 0 || CurrentItem == null)
			{
				return;
			}

			// Такой же код есть в OrdersListModel.xaml.cs todo: избавиться от повторения кода
			var bidsModel = new BidsModel();
			bidsModel.CurrentOrder = CurrentItem.Orders[SelectedOrder];
			var typeId = CurrentItem.Orders[SelectedOrder].TypeId - 1;

			var viewModel = new BidsViewModel()
			{
				BidsModel = bidsModel,
				CurrentOrder = CurrentItem.Orders[SelectedOrder],
				SelectedIndex = typeId
			};

			switch (typeId)
			{
				case 0:
					viewModel.CurrentSingleOrder = CurrentItem.Orders[SelectedOrder];
					break;
				case 1:
					viewModel.CurrentTemporaryOrder = CurrentItem.Orders[SelectedOrder];
					break;
				case 2:
					viewModel.CurrentVirtueOrder = CurrentItem.Orders[SelectedOrder];
					break;
			}

			var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
			ViewManager.Instance.OpenWindow("BidsView", viewModel, owner as IWindow);
		}

		private void Edit()
		{
			if (DocumentScaner != null)
			{
				DocumentScanerRemoveSubscription();
				DocumentScaner.ScanFinished += Scaner_ScanFinished;
			}

			DocumentScaner?.Connect();
			int indexEditingVisit = -1;
			if (model is VisitsModel)
				indexEditingVisit = (model as VisitsModel).selectedIndex;
			Model = new EditVisitsModel(Set, CurrentItem);
			(Model as EditVisitsModel).IndexEditingVisit = indexEditingVisit;
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
			string bufer_Family = CommonHelper.Check_FamilyNamePatronymic(CurrentItem.Family);
			string bufer_Name = CommonHelper.Check_FamilyNamePatronymic(CurrentItem.Name);
			string bufer_Patronymic = CommonHelper.Check_FamilyNamePatronymic(CurrentItem.Patronymic);

			CurrentItem.Position = VisitorsHelper.TestingPositionAnReturnCorrect(CurrentItem.Position);
			string bufer_Position = CommonHelper.Check_Position(CurrentItem.Position);

			bool error_Family = !string.IsNullOrEmpty(CurrentItem.Family) && bufer_Family != Regex.Replace(CurrentItem.Family, @"\s+", " ");
			bool error_Name = !string.IsNullOrEmpty(CurrentItem.Name) && bufer_Name != Regex.Replace(CurrentItem.Name, @"\s+", " ");
			bool error_Patronymic = !string.IsNullOrEmpty(CurrentItem.Patronymic) && bufer_Patronymic != Regex.Replace(CurrentItem.Patronymic, @"\s+", " ");
			bool error_Position = !string.IsNullOrEmpty(CurrentItem.Position) && bufer_Position != Regex.Replace(CurrentItem.Position, @"\s+", " ");
			

			if (error_Family || error_Name || error_Patronymic || error_Position)
			{
				StringBuilder stringBuilder = new StringBuilder();

				if (error_Family)
				{
					stringBuilder.Append("\"Фамилия\"" + Environment.NewLine);
				}

				if (error_Name)
				{
					stringBuilder.Append("\"Имя\"" + Environment.NewLine);
				}

				if (error_Patronymic)
				{
					stringBuilder.Append("\"Отчество\"" + Environment.NewLine);
				}

				if (error_Position)
				{
					stringBuilder.Append("\"Должность\"" + Environment.NewLine);
				}

				if (MessageBox.Show("В следующих полях текст содержит ошибки." + Environment.NewLine + stringBuilder + "Провести автокорректировку текста?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
				{
					CurrentItem.Family = bufer_Family;
					CurrentItem.Name = bufer_Name;
					CurrentItem.Patronymic = bufer_Patronymic;

					CurrentItem.Position = bufer_Position;
				}
				else
					return;

			}

			
			if (string.IsNullOrWhiteSpace(CurrentItem.Position) || string.IsNullOrEmpty(CurrentItem.Position) || CurrentItem.Position == "")
				CurrentItem.Position = "-";

			if (Model.Ok())
			{
				if (view.ParentWindow is VisitorsListWindView)
					view.CloseWindow(new CancelEventArgs());
				else
				{
					bool flag_GoEnd = false;
					int indexEditingVisit = -1;
					if (model is EditVisitsModel)
						indexEditingVisit = (model as EditVisitsModel).IndexEditingVisit;
					else
						flag_GoEnd = true;


					if (!flag_GoEnd && indexEditingVisit >= 0)
					{
						Model = new VisitsModel(indexEditingVisit);
						//CurrentItem = (Model as VisitsModel).GoingTo(indexEditingVisit);
					}
					else
					{
						if (flag_GoEnd)
						{
							Model = new VisitsModel(true);
						}
						else
						{
							Model = new VisitsModel();
						}
					}
					//Model = new VisitsModel();
				}

				IsRedactMode = false;

			}
			else
			{
				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(Family));
				OnPropertyChanged(nameof(Patronymic));
				OnPropertyChanged(nameof(CurrentItem));
				OnPropertyChanged(nameof(PhotoSource));
				OnPropertyChanged(nameof(Signature));
				OnPropertyChanged(nameof(BirthDate));
				OnPropertyChanged(nameof(VisibleTabItem_Employee));
				OnPropertyChanged(nameof(CommentTextEnable));
				OnPropertyChanged(nameof(CommentText));
				Update_Fields();


			}
			DocumentScanerDispose();
		}

		public void Cancel()
		{
			if (System.Windows.MessageBox.Show("Вы уверены, что хотите отменить изменения?", "Внимание",
				    MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
			{
				return;
			}

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
			DocumentScanerDispose();
		}

		private void AddImageSource(ImageType imageType, string name, CPerson person = null, bool isPortraitForSubstit = false)
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
				if (person.Portrait != null && isPortraitForSubstit)
				{
					string fileName = baseModel.AddImageSource(person.Portrait, ImageType.Photo);
					(view as Window)?.Invoke(() =>
					{
						AddImageToDocuments(_nameDocument_PhotoImageType, fileName);
					});
					
				}

				if (person.Signature != null)
				{
					string fileName = baseModel.AddImageSource(person.Signature, ImageType.Signature);
					(view as Window)?.Invoke(() =>
					{
						AddImageToDocuments(_nameDocument_SignatureImageType, fileName);
					});
				}
			}
			Update_Fields();
		}

		private void RemoveImageSource(ImageType imageType, string name)
		{
			Model.RemoveImageSource(imageType);
			RemoveImageToDocuments(name);

			Update_Fields();
		}

		private void OpenDocument()
		{
			if (SelectedDocument < 0)
			{
				return;
			}

			if (IsRedactMode && CurrentItem.Documents[SelectedDocument].IsCanAddChanges)
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

			Update_Fields();
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

			Update_Fields();
		}

		private void RemoveDocument()
		{
			if (SelectedDocument < 0)
			{
				return;
			}

			VisitorsDocument deleteItem = CurrentItem?.Documents?[SelectedDocument];
			Model.RemoveDocument(SelectedDocument);

			Update_Fields();

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

		public void OpenMainDocument()
		{
			if (SelectedMainDocument < 0)
			{
				return;
			}

			if (IsRedactMode)
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
			    new VisitorsMainDocumentModel(null), true, null);
			window.ShowDialog();
			var document = window.WindowResult as VisitorsMainDocument;

			if (document == null)
			{
				return;
			}

			Model.AddMainDocument(document);
			if (CurrentItem.MainDocuments.Count > 0)
			{
				var pasportMainDocument =
					CurrentItem.MainDocuments.FirstOrDefault(item => item.Type == BaseVisitsModel.NameTypeDocument_Pasport);
				if (pasportMainDocument != null)
					BirthDate = pasportMainDocument.BirthDate.ToString();
				else
				{
					pasportMainDocument = CurrentItem.MainDocuments.First();
					BirthDate = pasportMainDocument.BirthDate.ToString();
				}

			}

			Generate_VisitorDocument(document.DocumentName, document.Images, false);


			Update_Fields();
		}

		private void EditMainDocument()
		{
			if (SelectedMainDocument < 0)
			{
				return;
			}

			string editingMainDocument = CurrentItem.MainDocuments[SelectedMainDocument].DocumentName;

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

			Remove_VisitorDocument(editingMainDocument);
			Generate_VisitorDocument(document.DocumentName, document.Images, false);


			Update_Fields();

			if (CurrentItem.MainDocuments.Count > 0)
			{
				var pasportMainDocument =
					CurrentItem.MainDocuments.FirstOrDefault(item => item.Type == BaseVisitsModel.NameTypeDocument_Pasport);
				if (pasportMainDocument != null)
					BirthDate = pasportMainDocument.BirthDate.ToString();
				else
				{
					pasportMainDocument = CurrentItem.MainDocuments.First();
					BirthDate = pasportMainDocument.BirthDate.ToString();
				}

			}

		}

		private void RemoveMainDocument()
		{
			if (SelectedMainDocument < 0)
			{
				return;
			}

			if (SelectedMainDocument < CurrentItem.MainDocuments.Count)
				Remove_VisitorDocument(CurrentItem.MainDocuments[SelectedMainDocument].DocumentName);

			Model.RemoveMainDocument(SelectedMainDocument);


			Update_Fields();

			if (CurrentItem.MainDocuments.Count > 0)
			{
				var pasportMainDocument =
					CurrentItem.MainDocuments.FirstOrDefault(item => item.Type == BaseVisitsModel.NameTypeDocument_Pasport);
				if (pasportMainDocument != null)
					BirthDate = pasportMainDocument.BirthDate.ToString();
				else
				{
					pasportMainDocument = CurrentItem.MainDocuments.First();
					BirthDate = pasportMainDocument.BirthDate.ToString();
				}

			}
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

			OnPropertyChanged(nameof(CommentText));
		}

		private void SavingEditedVisitorComment()
		{
			_bufer_CurrentItem_Comment = CurrentItem.Comment;
			if (Model.Ok())
			{
				if(Model is VisitsModel)
					CurrentItem = Model?.CurrentItem;
			}

			EndEditingVisitorComment();
		}

		private void EndEditingVisitorComment()
		{
			EditingVisitorCommentMode = false;
			CommentTextEnable = false;

			OnPropertyChanged(nameof(CommentText));
		}



		private void AddImageToDocuments(string name, string fileName)
		{
				RemoveImageToDocuments(name);

				Guid id = ImagesHelper.LoadImage(fileName);
				Generate_VisitorDocument(name, new List<Guid>() { id });
				
			
		}

		private void RemoveImageToDocuments(string name)
		{
			if (CurrentItem?.Documents?.Count <= 0)
			{
				return;
			}

			Remove_VisitorDocument(name);
		}

		private void Change_ButtonEnable()
		{
			//   if (Model is NewVisitsModel || Model is EditVisitsModel)
			//   {
			//	// Здесь учитывается инверсия значения переменное "ButtonEnable" при использовании классов NewVisitsModel или EditVisitsModel
			//	// Поэтому когда нужно активировать кнопку ButtonEnable = false
			//	// Поэтому когда нужно заблокиравать кнопку ButtonEnable = true
			//	if (SelectedDocument < 0)
			//    {
			//	    EnableButton_OpenDocument = false;
			//	    return;
			//    }

			//	#region НЕ ИСПОЛЬЗУЕТСЯ Модуль блокирования кнопки "Просмотр", если в списке прикрепленных сканов выбраны пункты "Личная фотография" или "Личная подпись"
			//	//   VisitorsDocument deleteItem = CurrentItem?.Documents?[SelectedDocument];
			//	//   if (deleteItem != null)
			//	//   {
			//	//    if (deleteItem.Name != _nameDocument_PhotoImageType && deleteItem.Name != _nameDocument_SignatureImageType)
			//	//    {
			//	//	    ButtonEnable = false;
			//	//	    return;
			//	//    }
			//	//	 }
			//	// 
			//	// ButtonEnable = true;
			//	#endregion

			//    EnableButton_OpenDocument = true;
			//	return; 
			//}
			//   EnableButton_OpenDocument = false;
		}

		private void Generate_VisitorDocument(string name, List<Guid> listGuids, bool canAddChange = true)
		{
			VisitorsDocument visitorsDocument = new VisitorsDocument()
			{
				Name = name,
				TypeId = 0,
				Images = new List<Guid>(listGuids),
				IsChanged = true,
				IsCanAddChanges = canAddChange
			};
			(view as Window)?.Dispatcher.Invoke(() => {

				Model.AddDocument(visitorsDocument);
			});
		}

		private void Remove_VisitorDocument(string name)
		{
			VisitorsDocument deleteItem = CurrentItem?.Documents?.FirstOrDefault(item => item.Name == name);
			int? index = CurrentItem?.Documents?.IndexOf(deleteItem);
			if (index != null && index >= 0 && deleteItem != null)
				Model.RemoveDocument(index.Value);
		}

		private void Update_Fields()
		{

			OnPropertyChanged(nameof(EnableButton_OpenDocument));
			OnPropertyChanged(nameof(EnableButton_OpenMainDocument));
			OnPropertyChanged(nameof(EnableList_Document));
			OnPropertyChanged(nameof(EnableList_MainDocument));
		}

		#region Realization events

		private void TestingNameVisitorsDocument(object sender, CancelEventArgs e)
		{
			if (sender is VisitorsDocumentViewModel)
			{
				VisitorsDocumentViewModel visitorsDocumentViewModel = sender as VisitorsDocumentViewModel;
				if ((sender as VisitorsDocumentViewModel).Model.Data.Id == -1)
				{


					if (visitorsDocumentViewModel.Name == _nameDocument_PhotoImageType)
					{
						e.Cancel = false;
						if (PhotoSource != "")
							MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						else
							MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
									" невозможно добавить, так как данное название используется только для документа, содержащий личную фотографию", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					if (visitorsDocumentViewModel.Name == _nameDocument_SignatureImageType)
					{
						e.Cancel = false;
						if (Signature != "")
							MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						else
							MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
									" невозможно добавить, так как данное название используется только для документа, содержащий скан личной подписи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					VisitorsDocument findingItem =
						CurrentItem?.Documents?.FirstOrDefault(item => item.Name == visitorsDocumentViewModel.Name);
					if (findingItem != null)
					{
						e.Cancel = false;
						MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" + " уже имеется", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					if (visitorsDocumentViewModel.Name == _nameDocument_PhotoImageType)
					{
						MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
								" невозможно добавить, так как данное название используется только для документа, содержащий личную фотографию", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					if (visitorsDocumentViewModel.Name == _nameDocument_SignatureImageType)
					{
						MessageBox.Show("Документ с названием " + "\"" + visitorsDocumentViewModel.Name + "\"" +
								" невозможно добавить, так как данное название используется только для документа, содержащий скан личной подписи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
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
		public const string NameTypeDocument_Pasport = "Паспорт";

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
			OrderElementsWrapper.CurrentTable().OnChanged += UpdateOrderElements;
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
		public string AddImageSource(byte[] image, ImageType imageType)
		{
			Guid guidImage = ImagesHelper.GetGuidFromByteArray(image);
			string fileName = ImagesHelper.GetImagePath(guidImage);
			SetImageSource(guidImage, imageType);
			return fileName;
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
		protected virtual void UpdateOrderElements() { }

		protected bool Validate()
		{
			if (!Validate_BaseData())
			{
				return false;
			}

			if (!CurrentItem.IsAgree)
			{
				MessageBox.Show("Нет согласия на обработку персональных данных!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (!CurrentItem.IsNotFormular &
		(string.IsNullOrEmpty(CurrentItem.Telephone) ||
		string.IsNullOrEmpty(CurrentItem.Nation) ||
		!CurrentItem.MainDocuments.Any()))
			{
				MessageBox.Show("Не все поля вкладки Основная заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (!ValidateDocumentDates() &&
			    MessageBox.Show("В документах разные даты рождения. Все равно сохранить?",
			    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
			if (!Validate_BaseData())
			{
				return false;
			}

			if (!CurrentItem.IsAgree)
			{
				DialogResult result =
					MessageBox.Show(
						"Нет согласия на обработку персональных данных! " + Environment.NewLine +
						"При подтвердении сохранения будут сохранены ФИО посетителя и название организации, а остальные данные будут удалены!", "Внимание",
						MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

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
				MessageBox.Show("Не все поля вкладки Основная заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (!ValidateDocumentDates() &&
			    MessageBox.Show("В документах разные даты рождения. Все равно сохранить?",
				    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return false;
			}

			return true;
		}

		protected bool Validate_BaseData()
		{
			if (string.IsNullOrEmpty(CurrentItem.Family) ||
			    string.IsNullOrWhiteSpace(CurrentItem.Family) ||

				    string.IsNullOrEmpty(CurrentItem.Name) ||
			    string.IsNullOrWhiteSpace(CurrentItem.Name) ||

				    string.IsNullOrEmpty(CurrentItem.Organization) ||
			    string.IsNullOrWhiteSpace(CurrentItem.Organization))
			{
				StringBuilder stringBuilder = new StringBuilder();


				if (string.IsNullOrEmpty(CurrentItem.Family) ||
				string.IsNullOrWhiteSpace(CurrentItem.Family))
				{
					stringBuilder.Append("• Фамилия" + Environment.NewLine);
				}

				if (string.IsNullOrEmpty(CurrentItem.Name) ||
				    string.IsNullOrWhiteSpace(CurrentItem.Name))
				{
					stringBuilder.Append("• Имя" + Environment.NewLine);
				}

				//if (string.IsNullOrEmpty(CurrentItem.Patronymic) ||
				//    string.IsNullOrWhiteSpace(CurrentItem.Patronymic))
				//{
				// stringBuilder.Append("• Отчество" + Environment.NewLine);
				//}

				if (string.IsNullOrEmpty(CurrentItem.Organization) ||
				    string.IsNullOrWhiteSpace(CurrentItem.Organization))
				{
					stringBuilder.Append("• Организация" + Environment.NewLine);
				}

				string generatedText = stringBuilder.ToString();

				MessageBox.Show("Следующие поля заполнены не корректно:" + Environment.NewLine + generatedText, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
						    "При подтвердении сохранения будут сохранены ФИО посетителя и название организации, а остальные данные будут удалены!", "Внимание",
						MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
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
				MessageBox.Show("Не все поля вкладки Основная заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			return true;
		}

		protected bool Validate_DocumentDates()
		{
			if (!ValidateDocumentDates() &&
			    MessageBox.Show("В документах разные даты рождения. Все равно сохранить?",
				    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
			DateTime birthDate;
			//DateTime? birthDate = null;
			//foreach (var document in visitor.MainDocuments)
			//{
			//	if (document.BirthDate > DateTime.MinValue)
			//	{
			//		if (!birthDate.HasValue)
			//		{
			//			birthDate = document.BirthDate;
			//		}
			//		else
			//		{
			//			if (!document.BirthDate.Equals(birthDate))
			//			{
			//				return "В документах указаны разные даты рождения";
			//			}
			//		}
			//	}
			//}


			if (visitor.MainDocuments.Count > 0)
			{

				var pasportMainDocument =
					visitor.MainDocuments.FirstOrDefault(item => item.Type == BaseVisitsModel.NameTypeDocument_Pasport);
				if (pasportMainDocument != null)
					birthDate = pasportMainDocument.BirthDate;
				else
				{
					pasportMainDocument = visitor.MainDocuments.First();
					birthDate = pasportMainDocument.BirthDate;
				}
			}
			else
			{
				//return "В документах указаны разные даты рождения";
				return "";
			}

			return birthDate.ToShortDateString();
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
		public int selectedIndex { get; set; } = 0;


		public override bool TextEnable
		{
			get { return _textEnable; }
			set { _textEnable = value; }
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

		protected override void UpdateOrderElements()
		{
			int index = Set.IndexOf(CurrentItem);
			if (index >= 0)
			{
				OrderElementsToVisitors(index);
			}
		}

		public VisitsModel(int loadingVisitorIndex)
		{
			visitorsEnable =
				new VisitorsEnableOrVisible
				{
					AcceptButtonEnable = false,
					CancelButtonEnable = false
				};
			VisitorsWrapper.CurrentTable().OnChanged += Query;
			OrganizationsWrapper.CurrentTable().OnChanged += Query;
			Query(loadingVisitorIndex);

			//GoingTo(loadingVisitorIndex);
		}

		public VisitsModel(bool moveToEnd)
		{
			visitorsEnable =
				new VisitorsEnableOrVisible
				{
					AcceptButtonEnable = false,
					CancelButtonEnable = false
				};
			VisitorsWrapper.CurrentTable().OnChanged += Query;
			OrganizationsWrapper.CurrentTable().OnChanged += Query;
			Query(moveToEnd);


		}

		private void LoadingSets()
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
					 IsNotFormular = CommonHelper.StringToBool(visitors.Field<string>("f_no_formular")),
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
					 OrganizationIsBasic = OrganizationsHelper.GetBasicParametr(visitors.Field<int>("f_org_id"), true)

				 });

			visitorsEnable.EditButtonEnable = Set != null && Set.Count > 0 ? true : false;
		}

		private void Query()
		{
			LoadingSets();

			if (Set.Count > 0)
			{
				if (selectedIndex >= Set.Count)
					selectedIndex = 0;

				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
				DocumentsToVisitor(selectedIndex);
				CurrentItem = Set[selectedIndex];
			}
		}

		private void Query(int visitorIndex)
		{
			LoadingSets();

			if (Set.Count > 0)
			{
				if (visitorIndex >= Set.Count)
					selectedIndex = 0;
				else
					selectedIndex = visitorIndex;

				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
				DocumentsToVisitor(selectedIndex);
				CurrentItem = Set[selectedIndex];
			}
		}

		private void Query(bool moveToEnd)
		{
			LoadingSets();

			if (Set.Count > 0)
			{
				if (moveToEnd)
					selectedIndex = Set.Count - 1;

				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
				DocumentsToVisitor(selectedIndex);
				CurrentItem = Set[selectedIndex];
			}
		}

		public override EnumerationClasses.Visitor Begin()
		{
			if (Set.Count > 0)
			{
				selectedIndex = 0;
				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
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
				OrderElementsToVisitors(selectedIndex);
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
				OrderElementsToVisitors(selectedIndex);
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
					OrderElementsToVisitors(selectedIndex);
					DocumentsToVisitor(selectedIndex);
					CurrentItem = Set[selectedIndex];
					break;
				}
			}
			return CurrentItem;
		}

		public EnumerationClasses.Visitor GoingTo(int index)
		{
			if (index < Set.Count)
			{
				selectedIndex = index;
				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
				DocumentsToVisitor(selectedIndex);
				CurrentItem = Set[selectedIndex];
			}

			return CurrentItem;
		}

		public override EnumerationClasses.Visitor Next()
		{
			if (Set.Count > 0 && selectedIndex < Set.Count - 1)
			{
				selectedIndex++;
				OrdersCardsToVisitor(selectedIndex);
				OrderElementsToVisitors(selectedIndex);
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

		private void OrderElementsToVisitors(int index)
		{
			if (Set[index].OrderElements == null)
			{
				Set[index].OrderElements = new ObservableCollection<OrderElement>(
					from row in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
					where (row.Field<int>("f_visitor_id") == (index + 1) && row.Field<string>("f_deleted") != "Y")
					select new OrderElement(false)
					{
						Id = row.Field<int>("f_oe_id"),
						OrderId = row.Field<int>("f_ord_id"),
						VisitorId = row.Field<int>("f_visitor_id"),
						OrganizationId = row.Field<int?>("f_org_id"),
						Position = row.Field<string>("f_position"),
						CatcherId = row.Field<int>("f_catcher_id"),
						From = row.Field<DateTime>("f_time_from"),
						To = row.Field<DateTime>("f_time_to"),
						Passes = row.Field<string>("f_passes"),
						IsDisable = row.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
						IsBlock = CommonHelper.StringToBool(VisitorsWrapper.CurrentTable().Table.AsEnumerable()
							.Where(item => item.Field<int>("f_visitor_id") == row.Field<int>("f_visitor_id"))?.FirstOrDefault()?
							.Field<string>("f_persona_non_grata")),
						IsCardIssued = true,
						Reason = row.Field<string>("f_other_org"),
						TemplateIdList = row.Field<string>("f_oe_templates"),
						AreaIdList = row.Field<string>("f_oe_areas"),
						ScheduleId = row.Field<int>("f_schedule_id"),
						Schedule = row.Field<int>("f_schedule_id") == 0
							? ""
							: SchedulesWrapper.CurrentTable()
								.Table.AsEnumerable().FirstOrDefault(
									arg => arg.Field<int>("f_schedule_id") ==
									       row.Field<int>("f_schedule_id"))["f_schedule_name"].ToString(),
					});
			}
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
					where OrdElem.Field<int>("f_visitor_id") == Set[index].Id &&
					CommonHelper.NotDeleted(OrdElem)
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
						NewRecDate = Ord.Field<DateTime>("f_new_rec_date"),
						TypeId = Ord.Field<int>("f_order_type_id"),
				//OrderType = SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg => arg.Field<int>("f_order_type_id") == Ord.Field<int>("f_order_type_id"))?.Field<string>("f_order_text"),
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
							IsBlock = CommonHelper.StringToBool(VisitorsWrapper.CurrentTable().Table.AsEnumerable().
							Where(item => item.Field<int>("f_visitor_id") == row.Field<int>("f_visitor_id"))?.
							FirstOrDefault()?.Field<string>("f_persona_non_grata")),
							IsCardIssued = true,
							Reason = row.Field<string>("f_other_org"),
							TemplateIdList = row.Field<string>("f_oe_templates"),
							AreaIdList = row.Field<string>("f_oe_areas"),
							ScheduleId = row.Field<int>("f_schedule_id"),
							Schedule = row.Field<int>("f_schedule_id") == 0 ? "" :
							SchedulesWrapper.CurrentTable()
							.Table.AsEnumerable().FirstOrDefault(
							    arg => arg.Field<int>("f_schedule_id") ==
							    row.Field<int>("f_schedule_id"))?.Field<string>("f_schedule_name"),
						})
					});
			}
			if (Set[index].Cards == null)
			{
				var cardsExt = new List<Card2>(
				    from ce in CardsExtWrapper.CurrentTable().Table.AsEnumerable()
				    where ce.Field<int>("f_card_id") != 0 &&
				    CommonHelper.NotDeleted(ce)
				    select new Card2
				    {
					    CardIdHi = ce.Field<int>("f_object_id_hi"),
					    CardIdLo = ce.Field<int>("f_object_id_lo"),
					    StateId = ce.Field<int>("f_state_id")
				    });

				Set[index].Cards = new ObservableCollection<Card2>(
				    from card in CardsWrapper.CurrentTable().Table.AsEnumerable()
				    join visit in VisitsWrapper.CurrentTable().Table.AsEnumerable()
				    on new { a = card.Field<int>("f_object_id_hi"), b = card.Field<int>("f_object_id_lo") }
				    equals new { a = visit.Field<int>("f_card_id_hi"), b = visit.Field<int>("f_card_id_lo") }
				    where visit.Field<int>("f_visitor_id") == Set[index].Id &&
				    visit.Field<string>("f_deleted") == "N"
				    select new Card2
				    {
					    CardIdHi = card.Field<int>("f_object_id_hi"),
					    CardIdLo = card.Field<int>("f_object_id_lo"),
					    Card = card.Field<string>("f_card_name"),
					    CardNumber = card.Field<int>("f_card_num").ToString(),
					    From = visit.Field<DateTime>("f_date_from"),
					    To = visit.Field<DateTime>("f_date_to"),
					    Change = visit.Field<DateTime>("f_rec_date"),
					    Operator = UsersWrapper.CurrentTable().Table
					    .AsEnumerable().FirstOrDefault(arg =>
					    arg.Field<int>("f_user_id") ==
					    visit.Field<int>("f_rec_operator"))?["f_user"].ToString(),
					    OrderNum = OrdersHelper.GetOrderNumber(visit.Field<string>("f_orders")),
					    Comment = visit.Field<string>("f_visit_text"),
					    Orders = visit.Field<string>("f_orders"),
					    StateId = 1
				    });

				foreach (var c in Set[index].Cards)
				{
					Card2 ce = cardsExt.FirstOrDefault(r =>
					    r.CardIdHi == c.CardIdHi && r.CardIdLo == c.CardIdLo);
					if (ce != null)
					{
						c.StateId = ce.StateId;
					}
				}
				foreach (var ce in cardsExt)
				{
					var row = Set[index].Cards.FirstOrDefault(r =>
					    r.CardIdHi == ce.CardIdHi && r.CardIdLo == ce.CardIdLo);
					if (row != null)
					{
						row.StateId = ce.StateId;
					}
				}
			}
			foreach (var order in Set[index].Orders)
			{
				order.CardState = CardsHelper.CardStateToSting(CardState.Inactive);
				foreach (OrderElement element in order.OrderElements)
				{
					if (element.VisitorId == Set[index].Id)
					{
						element.SetupCardState();
						order.CardState = element.CardStateString;
					}
				}
				//foreach (var card in Set[index].Cards)
				//{
				//    if (card.OrderId == order.Id ||
				//        AndoverEntityListHelper.StringToEntityIds(card.Orders).Contains(order.Id))
				//    {
				//        order.CardState = CommonHelper.CardStateToSting((CardState) card.StateId);
				//        break;
				//    }
				//}
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
						IsCanAddChanges= VisitorsDocument.Detecting_CanAddChanges(Set[index].MainDocuments, documents.Field<string>("f_doc_name"))
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

		public override bool CommentTextEnable
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
			CurrentItem = new EnumerationClasses.Visitor() { AgreeToDate = DateTime.Now };
			CurrentItem.MainDocuments = new ObservableCollection<VisitorsMainDocument>();
			CurrentItem.Documents = new ObservableCollection<VisitorsDocument>();
			Set.Add(CurrentItem);
		}

		public override bool Ok()
		{
            Logger.Log.Debug($"Попытка добавления посетителя");
            bool? validate = Validate_AndUse_IsAgree();

			if (validate != null && !validate.Value)
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
			row["f_no_formular"] = CommonHelper.BoolToString(CurrentItem.IsNotFormular);


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

			SaveAdditionalData((int)row["f_visitor_id"]);
			OnClose?.Invoke(CurrentItem);
			return true;
		}
	}

	public class EditVisitsModel : BaseVisitsModel
	{
		private bool _buttonEnable = true;
		public EnumerationClasses.Visitor OldVisitor { get; set; }

		public int IndexEditingVisit { get; set; } = -1;

		public override bool TextEnable
		{
			get { return true; }
			set { }
		}

		public override bool CommentTextEnable
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
			CurrentItem = (EnumerationClasses.Visitor)visitor?.Clone();
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
			if (OldVisitor.IsNotFormular != CurrentItem.IsNotFormular)
			{
				row["f_no_formular"] = CommonHelper.BoolToString(CurrentItem.IsNotFormular);
			}





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
