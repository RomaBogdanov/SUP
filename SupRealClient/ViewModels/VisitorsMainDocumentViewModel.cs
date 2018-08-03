using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media.Imaging;
using RegulaLib;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;

namespace SupRealClient.ViewModels
{
    public class VisitorsMainDocumentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private VisitorsMainDocumentModel model;

        private int documentId = -1;
        private string docType = "";
        private string seria = "";
        private string num = "";
        private DateTime date;
        private DateTime dateTo;
        private string org = "";
        private string code = "";
        private DateTime birthDate;
        private string comment = "";

	 private string image = "";

	private List<Guid> imageCache = new List<Guid>();
        private int selectedImage = -1;

        public bool Editable { get; private set; }
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
                    date = value;
                    OnPropertyChanged("Date");
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
                    dateTo = value;
                    OnPropertyChanged("DateTo");
                }
            }
        }

        public string Org
        {
            get { return org; }
            set
            {
                if (value != null)
                {
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

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value != null)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
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
			get { return image; }
			set
			{
				image = value;
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

        public VisitorsMainDocumentViewModel(bool editable)
        {
            Editable = editable;
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
		    }
		    else
		    {
			    imageCache = DocumentsHelper.CacheImages(model.Data.Id);
		    }
		    
		    if (person?.PagesScanList != null)
		    {
			    foreach (var page in person.PagesScanList)
			    {
				    imageCache.Add(ImagesHelper.GetGuidFromByteArray(page));
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
	    }

	    protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

	    private void AddImage()
	    {
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var guid = ImagesHelper.LoadImage(dlg.FileName);
				imageCache.Add(guid);
				SetImage(imageCache.Count - 1);
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
            Image = ImagesHelper.GetImagePath(imageCache[selectedImage]);
        }

        private void OkExecute()
        {
            if (DocType == null || DocType == "" ||
                Num == null || Num == "" ||
                Date == null || Date == DateTime.MinValue ||
                Org == null || Org == "")
            {
                MessageBox.Show("Не все поля заполнены!");
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

        private void DocumentsListModel()
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
        }

        private void Clear()
        {
            documentId = -1;
            DocType = "";

            OnPropertyChanged("DocType");
        }
    }
}
