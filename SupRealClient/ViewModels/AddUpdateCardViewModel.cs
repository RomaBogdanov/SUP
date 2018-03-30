﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient
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

        public int CurdNum
        {
            get { return curdNum; }
            set
            {
                curdNum = value;
                OnPropertyChanged("CurdNum");
            }
        }

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

        public int NumMAFW
        {
            get { return numMAFW; }
            set
            {
                numMAFW = value;
                OnPropertyChanged("NumMAFW");
            }
        }

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
            this.model.ViewModel = this;
            this.Ok = new RelayCommand(arg => this.model.Ok());
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
            this.ChangeState = new RelayCommand(arg => this.model.ChangeState());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}