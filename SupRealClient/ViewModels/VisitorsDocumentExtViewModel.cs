using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class VisitorsDocumentExtViewModel : VisitorsDocumentViewModel
    {
        private string seria = "";
        private string num = "";
        private DateTime date;
        private string org = "";
        private string code = "";
        private int documentId = -1;
        private string docType = "";

        public ICommand DocumentsCommand { get; set; }
        public ICommand ClearCommand { get; set; }

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

        public VisitorsDocumentExtViewModel()
        {
        }

        protected override void SetModel()
        {
            this.Name = model.Data.Name;
            this.Seria = model.Data.Seria;
            this.Num = model.Data.Num;
            this.Date = model.Data.Date;
            this.Org = model.Data.Org;
            this.Code = model.Data.Code;
            documentId = model.Data.TypeId;
            DocType = (string)DocumentsWrapper.CurrentTable().Table
                .AsEnumerable().FirstOrDefault(arg =>
                arg.Field<int>("f_doc_id") == documentId)?["f_doc_name"];
            this.Ok = new RelayCommand(arg => OkExecute());
            DocumentsCommand = new RelayCommand(arg => DocumentsListModel());
            ClearCommand = new RelayCommand(arg => Clear());
        }

        private void OkExecute()
        {
            if (DocType == null || DocType == "" ||
                Name == null || Name == "" ||
                Seria == null || Seria == "" ||
                Num == null || Num == "" ||
                Date == null || Date == DateTime.MinValue ||
                Org == null || Org == "" ||
                Code == null || Code == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            this.model.Ok(
                new VisitorsDocument
                {
                    Name = Name,
                    TypeId = documentId,
                    Seria = Seria,
                    Num = Num,
                    Org = Org,
                    Code = Code,
                    Date = Date,
                    Images = new List<Guid>(),
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
