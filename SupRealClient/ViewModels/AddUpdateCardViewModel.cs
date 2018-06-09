using SupRealClient.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.ViewModels
{
    public class AddUpdateCardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAddUpdateCardModel model;
        private int curdNum;
        private string state = "Активен";
        private DateTime createDate = DateTime.Now;
        private int numMAFW;
        private string comment;

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// Пропуск.
        /// </summary>
        public int CurdNum
        {
            get { return curdNum; }
            set
            {
                curdNum = value;
                OnPropertyChanged("CurdNum");
            }
        }

        /// <summary>
        /// Текущее состояние.
        /// </summary>
        public string State
        {
            get { return state; }
            set
            {
                if (value != null)
                {
                    state = value;
                    OnPropertyChanged("State");
                }
            }
        }

        /// <summary>
        /// Внесен в БД.
        /// </summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                if (value != null)
                {
                    createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }

        /// <summary>
        /// Номер пропуска в MultiAccess.
        /// </summary>
        public int NumMAFW
        {
            get { return numMAFW; }
            set
            {
                numMAFW = value;
                OnPropertyChanged("NumMAFW");
            }
        }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public ICommand ChangeState { get; set; }

        public AddUpdateCardViewModel() { }


        public void SetModel(IAddUpdateCardModel addItem1Model)
        {
            this.model = addItem1Model;
            this.Caption = addItem1Model is AddCardModel ? "Добавление пропуска" :
                           addItem1Model is UpdateCardModel ? "Редактирование пропуска" :
                           string.Empty;
            this.CurdNum = model.Data.CurdNum;
            this.CreateDate = model.Data.CreateDate;
            this.NumMAFW = model.Data.NumMAFW;
            this.Comment = model.Data.Comment;

            this.Ok = new RelayCommand(arg => this.model.Ok(new Card
            {
                CurdNum = CurdNum,
                CreateDate = CreateDate,
                NumMAFW = NumMAFW,
                Comment = Comment
            }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
            this.ChangeState = new RelayCommand(arg => this.model.ChangeState());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
