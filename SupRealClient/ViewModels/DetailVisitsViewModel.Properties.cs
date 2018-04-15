using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models;
using SupRealClient.Models.OrganizationStructure;

namespace SupRealClient.ViewModels
{
    public partial class DetailVisitsViewModel
    {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private int _id;

        public Human Human
        {
            get { return _human; }
            set
            {
                _human = value;
                OnPropertyChanged();
            }
        }
        private Human _human;

        public string Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
                OnPropertyChanged();
            }
        }
        private string _organization;

        public Pass Pass
        {
            get { return _pass; }
            set
            {
                _pass = value;
                OnPropertyChanged();
            }
        }
        private Pass _pass;

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }
        private DateTime _startTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value; 
                OnPropertyChanged();
            }
        }
        private DateTime _endTime;

        public DateTime PeriodFrom
        {
            get { return _periodFrom; }
            set
            {
                _periodFrom = value;
                OnPropertyChanged();
            }
        }
        private DateTime _periodFrom;

        public DateTime PeriodTo
        {
            get { return _periodTo; }
            set
            {
                _periodTo = value;
                OnPropertyChanged();
            }
        }
        private DateTime _periodTo;

        public string RealBidId
        {
            get { return _realBidId; }
            set
            {
                _realBidId = value; 
                OnPropertyChanged();
            }
        }
        private string _realBidId;

        public string RemovedBidId
        {
            get { return _removedBidId; }
            set
            {
                _removedBidId = value;
                OnPropertyChanged();
            }
        }
        private string _removedBidId;

        public bool Activ
        {
            get { return _activ; }
            set
            {
                _activ = value;
                OnPropertyChanged();
            }
        }
        private bool _activ;

        public DateTime EditDate
        {
            get { return _editDate; }
            set
            {
                _editDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _editDate;

        public Human Operator
        {
            get { return _operator; }
            set
            {
                _operator = value; 
                OnPropertyChanged();
            }
        }
        private Human _operator;

        public int Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }
        private int _key;
    }
}
