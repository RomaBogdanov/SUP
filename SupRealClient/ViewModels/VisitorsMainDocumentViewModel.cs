using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
        private string comment = "";

        private ObservableCollection<string> images =
            new ObservableCollection<string>();
        private List<Guid> imageCache = new List<Guid>();
        private int selectedImage = -1;

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

        public ObservableCollection<string> Images
        {
            get { return images; }
            set
            {
                if (value != null)
                {
                    images = value;
                    OnPropertyChanged("Images");
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

        public ICommand AddImageCommand { get; set; }
        public ICommand RemoveImageCommand { get; set; }

        public ICommand DocumentsCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }

        public VisitorsMainDocumentViewModel()
        {
        }

        public void SetModel(VisitorsMainDocumentModel model)
        {
            this.model = model;
            Caption = model.Data.Id == 0 ? "Добавление документа" :
                "Редактирование документа";

            this.Seria = model.Data.Seria;
            this.Num = model.Data.Num;
            this.Date = model.Data.Date;
            this.DateTo = model.Data.DateTo;
            this.Org = model.Data.Org;
            this.Code = model.Data.Code;
            this.Comment = model.Data.Comment;
            documentId = model.Data.TypeId;
            DocType = (string)DocumentsWrapper.CurrentTable().Table
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
            Images = new ObservableCollection<string>(imageCache.Select(i =>
                ImagesHelper.GetImagePath(i)));

            this.Ok = new RelayCommand(arg => OkExecute());
            this.Cancel = new RelayCommand(arg => this.model.Cancel());

            DocumentsCommand = new RelayCommand(arg => DocumentsListModel());
            ClearCommand = new RelayCommand(arg => Clear());

            AddImageCommand = new RelayCommand(arg => AddImage());
            RemoveImageCommand = new RelayCommand(arg => RemoveImage());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void AddImage()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Guid id = ImagesHelper.LoadImage(dlg.FileName);
                Images.Add(ImagesHelper.GetImagePath(id));
                imageCache.Add(id);
            }
        }

        private void RemoveImage()
        {
            if (SelectedImage < 0)
            {
                return;
            }
            int selected = SelectedImage;
            Images.RemoveAt(selected);
            imageCache.RemoveAt(selected);
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
                    Comment = Comment,
                    Images = imageCache,
                    IsChanged = true
                });
        }

        private void DocumentsListModel()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4DocumentsWindView", null) as BaseModelResult;
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
