using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using RegulaLib;
using SupRealClient.Common;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;
using SupRealClient.Common.Data;

namespace SupRealClient.ViewModels
{
    public class VisitorsMainDocumentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
	    public event Action<string> _MoveNextFocusingElement;

		private VisitorsMainDocumentModel model;

        private int documentId = -1;
        private string docType = "";
        private string seria = "";
        private string num = "";
        private DateTime date=DateTime.Now;
        private DateTime dateTo= DateTime.Now;
		private string org = "";
        private string code = "";
        private DateTime birthDate= DateTime.Now;

	    private bool _date_Correct;
	    private bool _dateTo_Correct;
	    private bool _birthDate_Correct;

		private string comment = "";
	    private string _image ;
	    private List<Guid> _imageCache = new List<Guid>();
	    private ObservableCollection<string> _images=new ObservableCollection<string>();
	    private bool _editable = false;

		public event Action _TestDatePickerEvent;
	    public event Action<string> _DateNotCorrect;


		public List<Guid> imageCache
	    {
		    get { return _imageCache; }
		    set
		    {
			    _imageCache = value;
				OnPropertyChanged(nameof(imageCache));
		    }
	    }

	    public ObservableCollection<string> Images
	    {
		    get { return _images; }
		    set
		    {
			    _images = value;
			    OnPropertyChanged(nameof(Images));
		    }
	    }


		private int selectedImage = -1;

	    public bool Editable
	    {
		    get { return _editable; }
		    set
		    {
			    _editable = value;
				OnPropertyChanged(nameof(Editable));
		    }
	    }

		public CPerson Person { get; private set; }

		public string Caption { get; private set; }

        public string DocType
        {
            get { return docType; }
            set
            {
                if (value != null)
                {
						docType = value;
					OnPropertyChanged("DocType");
                }
            }
        }

        public string Seria
        {
            get { return seria; }
            set
            {
                if (value != null)
                {
	                seria = value;
					OnPropertyChanged("Seria");
                }
            }
        }

        public string Num
        {
            get { return num; }
            set
            {
                if (value != null)
                {
						num = value;
                    OnPropertyChanged("Num");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != null)
				{
					if (Editable)
					{
						if (value.Date <= DateTime.Now.Date)
						{
							date = value;
							Date_Correct = true;
						}
						else
						{
							Date_Correct = false;
							_DateNotCorrect?.Invoke(nameof(Date));
							MessageBox.Show("Введенная дата выдачи документа неверна, так как введенная дата будет в будущем.", "Внимание",
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

						}
					}
					else
					{
						date = value;
					}

					OnPropertyChanged(nameof(Date));
                }
            }
        }

        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                if (value != null)
				{
					if (Editable)
					{
						if (value.Date >= DateTime.Now.Date)
						{
							dateTo = value;
							DateTo_Correct = true;
						}
						else
						{
							DateTo_Correct = false;
							_DateNotCorrect?.Invoke(nameof(DateTo));
							MessageBox.Show("Введенная дата действия документа неверна, так как введенная дата уже прошла.", "Внимание",
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
					else
					{
						dateTo = value;
					}

					OnPropertyChanged(nameof(DateTo));
                }
            }
        }

	    public DateTime BirthDate
	    {
		    get { return birthDate; }
		    set
		    {
			    if (value != null)
			    {
				    if (Editable)
				    {
					    if (value.Date <= DateTime.Now.Date)
					    {
						    birthDate = value;
						    BirthDate_Correct = true;
					    }
					    else
					    {
						    BirthDate_Correct = false;
						    _DateNotCorrect?.Invoke(nameof(BirthDate));
						    MessageBox.Show("Введенная дата рождения неверна, так как введенная дата будет в будущем.",
							    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					    }
				    }
				    else
				    {
						birthDate = value;
					}

				    OnPropertyChanged(nameof(BirthDate));
			    }
		    }
	    }

	    public bool Date_Correct
	    {
		    get { return _date_Correct; }
		    set
		    {
			    _date_Correct = value;
				OnPropertyChanged(nameof(Date_Correct));
		    }
	    }

	    public bool DateTo_Correct
		{
		    get { return _dateTo_Correct; }
		    set
		    {
			    _dateTo_Correct = value;
				    OnPropertyChanged(nameof(DateTo_Correct));
		    }
	    }

	    public bool BirthDate_Correct
		{
		    get { return _birthDate_Correct; }
		    set
		    {
				    _birthDate_Correct = value;
				    OnPropertyChanged(nameof(BirthDate_Correct));
		    }
	    }

		public string Org
        {
            get { return org; }
            set
            {
                if (value != null)
                {
	                if (!string.IsNullOrWhiteSpace(value))
						org = value;
                    OnPropertyChanged("Org");
                }
            }
        }

        public string Code
        {
            get { return code; }
            set
            {
                if (value != null)
                {
                    code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != null)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public int SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

		public string Image
		{
			get { return _image; }
			set
			{
				_image = value;
				OnPropertyChanged("Image");
			}
		}
	   

	 public ICommand AddImageCommand { get; set; }
        public ICommand RemoveImageCommand { get; set; }

        public ICommand DocumentsCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }
		public ICommand OpenDocumentImagesCommand { get; set; }


		public VisitorsMainDocumentViewModel(bool editable)
        {
			//Изменение
			Editable = editable;
			//Editable = true;
		}

	    public void SetModel(VisitorsMainDocumentModel model, CPerson person)
	    {
		    this.model = model;
		    Caption = !Editable
			    ? "Просмотр документа"
			    : (model.Data.Id == -1 ? "Добавление документа" : "Редактирование документа");

		    this.Seria = model.Data.Seria;
		    this.Num = model.Data.Num;
		    this.Date = model.Data.Date;
		    this.DateTo = model.Data.DateTo;
		    this.Org = model.Data.Org;
		    //Org = person?.DocumentDeliveryPlace?.Value;
		this.Code = model.Data.Code;
		    this.Comment = model.Data.Comment;
		    this.BirthDate = model.Data.BirthDate;
		    documentId = model.Data.TypeId;
		    DocType = (string) DocumentsWrapper.CurrentTable().Table
			    .AsEnumerable().FirstOrDefault(arg =>
				    arg.Field<int>("f_doc_id") == documentId)?["f_doc_name"];

		    if (model.Data.Images.Any())
		    {
			    imageCache = model.Data.Images;

			    foreach (var imageGuid in imageCache)
			    {
					Images.Add(ImagesHelper.GetImagePath(imageGuid));
				}
		    }
		    else
		    {
			    imageCache = DocumentsHelper.CacheImages(model.Data.Id);

			    foreach (var imageGuid in imageCache)
			    {
				    Images.Add(ImagesHelper.GetImagePath(imageGuid));
			    }
			}
		    
		    if (imageCache.Any())
		    {
			    SetImage(0);
		    }

		    this.Ok = new RelayCommand(arg => OkExecute());
		    this.Cancel = new RelayCommand(arg => this.model.Cancel());

		    DocumentsCommand = new RelayCommand(arg => DocumentsListModel());
		    ClearCommand = new RelayCommand(arg => Clear());

		    AddImageCommand = new RelayCommand(arg => AddImage());
		    RemoveImageCommand = new RelayCommand(arg => RemoveImage());

		    PrevCommand = new RelayCommand(arg => Prev());
		    NextCommand = new RelayCommand(arg => Next());

		    OpenDocumentImagesCommand = new RelayCommand(arg => OpeningDocumentImages());

	    }

	    protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

	    private void AddImage()
	    {
			var dlg = new ImageOpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var guid = ImagesHelper.LoadImage(dlg.FileName);
				imageCache.Add(guid);
				SetImage(imageCache.Count - 1);
				Images.Add(ImagesHelper.GetImagePath(guid));

				OnPropertyChanged(nameof(imageCache));
			}
		}

	    private void RemoveImage()
	    {
		    if (SelectedImage < 0)
		    {
			    return;
		    }

		    int selected = SelectedImage;
			imageCache.RemoveAt(selected);
		    Images.RemoveAt(selected);
			SetImage(selected <= imageCache.Count - 1 ? selected : selected - 1);
		}

	    private void Prev()
	    {
		    if (selectedImage > 0)
		    {
			    SetImage(selectedImage - 1);
		    }
	    }

	    private void Next()
        {
            if (selectedImage < imageCache.Count - 1)
            {
                SetImage(selectedImage + 1);
            }
        }

        private void SetImage(int index)
        {
            selectedImage = index;
	        if (index < 0)
	        {
		        return;
	        }
			//Image = ImagesHelper.GetImagePath(imageCache[selectedImage]);
	        Image = ImagesHelper.GetImagePath( imageCache[selectedImage]);
        }

        private void OkExecute()
        {
	        seria = CommonHelper.Check_SeriaCode(Seria);
	        code = CommonHelper.Check_NumberDocument(Code);
	        num = CommonHelper.Check_NumberDocument(Num);

			StringBuilder stringBuilder = new StringBuilder();
			if (DocType == null || DocType == "" || string.IsNullOrWhiteSpace(DocType) ||
				Num == null || Num == "" || string.IsNullOrWhiteSpace(Num) ||
                Date == null || Date == DateTime.MinValue)
            {
	            


	            if (DocType == null || DocType == "" || string.IsNullOrWhiteSpace(DocType))
	            {
		            stringBuilder.Append("• Тип документа" + Environment.NewLine);
	            }

	            if (Num == null || Num == "" || string.IsNullOrWhiteSpace(Num))
	            {
		            stringBuilder.Append("• Номер документа" + Environment.NewLine);
	            }

	            if (Date == null || Date == DateTime.MinValue )
	            {
		            stringBuilder.Append("• Дата выдачи" + Environment.NewLine);
	            }

	            string generatedText = stringBuilder.ToString();
				MessageBox.Show("Следующие поля заполнены не корректно:" + Environment.NewLine + generatedText, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

	            OnPropertyChanged(nameof(Seria));
	            OnPropertyChanged(nameof(Code));
				OnPropertyChanged(nameof(Num));

				return;
            }

	        _TestDatePickerEvent?.Invoke();


			if (!Date_Correct ||
	            !DateTo_Correct ||
	            !BirthDate_Correct)
			{
				stringBuilder.Clear();

				if (!Date_Correct)
		        {
			        stringBuilder.Append("• Дата выдачи" + Environment.NewLine);
		        }

		        if (!DateTo_Correct)
		        {
			        stringBuilder.Append("• Действителен до" + Environment.NewLine);
		        }

		        if (!BirthDate_Correct)
		        {
			        stringBuilder.Append("• Дата рождения" + Environment.NewLine);
		        }
				string generatedText = stringBuilder.ToString();
				MessageBox.Show("Следующие поля с датами заполнены не корректно:" + Environment.NewLine + generatedText, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		        return;
	        }

			this.model.Ok(
                new VisitorsMainDocument
                {
                    Type = DocType,
                    TypeId = documentId,
                    Seria = Seria,
                    Num = Num,
                    Org = Org,
                    Code = Code,
                    Date = Date,
                    DateTo = DateTo,
                    BirthDate = BirthDate,
                    Comment = Comment,
                    Images = imageCache,
                    IsChanged = true
                });
        }

	    private void OpeningDocumentImages()
	    {
		    if (selectedImage >= 0 && selectedImage< imageCache.Count)
		    {
			    DocumentImageView documentImageView = new DocumentImageView();
			    documentImageView.DocumentImage = ImagesHelper.GetImagePath(imageCache[selectedImage]);
			    documentImageView.ShowDialog();
		    }

	    }


	    public void DocumentsListModel()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4DocumentsWindViewOk", null) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            documentId = result.Id;
            DocType = result.Name;
            OnPropertyChanged("DocType");

	        _MoveNextFocusingElement?.Invoke("SetDocumentType");
		}

        private void Clear()
        {
            documentId = -1;
            DocType = "";

            OnPropertyChanged("DocType");
        }
    }
}
