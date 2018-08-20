using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class ChangeStateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ChangeStateModel model;
        private string name;
        private string state;
        private int stateId;
        private int curdNum;
        private bool stateActive;
        private bool stateActiveEnabled;
        private bool stateInactive;
        private bool stateInactiveEnabled;
        private bool stateIssued;
        private bool stateIssuedEnabled;
        private bool stateLost;
        private bool stateLostEnabled;

        public string Name
        {
            get { return name; }
            set
            {
                if (value != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
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

        public bool StateActive
        {
            get { return stateActive; }
            set
            {
                stateActive = value;
                OnPropertyChanged("StateActive");
            }
        }

        public bool StateActiveEnabled
        {
            get { return stateActiveEnabled; }
            set
            {
                stateActiveEnabled = value;
                OnPropertyChanged("StateActiveEnabled");
            }
        }

        public bool StateInactive
        {
            get { return stateInactive; }
            set
            {
                stateInactive = value;
                OnPropertyChanged("StateInactive");
            }
        }

        public bool StateInactiveEnabled
        {
            get { return stateInactiveEnabled; }
            set
            {
                stateInactiveEnabled = value;
                OnPropertyChanged("StateInactiveEnabled");
            }
        }

        public bool StateIssued
        {
            get { return stateIssued; }
            set
            {
                stateIssued = value;
                OnPropertyChanged("StateIssued");
            }
        }

        public bool StateIssuedEnabled
        {
            get { return stateIssuedEnabled; }
            set
            {
                stateIssuedEnabled = value;
                OnPropertyChanged("StateIssuedEnabled");
            }
        }

        public bool StateLost
        {
            get { return stateLost; }
            set
            {
                stateLost = value;
                OnPropertyChanged("StateLost");
            }
        }

        public bool StateLostEnabled
        {
            get { return stateLostEnabled; }
            set
            {
                stateLostEnabled = value;
                OnPropertyChanged("StateLostEnabled");
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public ChangeStateViewModel() { }

        public void SetModel(ChangeStateModel model)
        {
            this.model = model;
            this.State = model.Data.State;
            this.stateId = model.Data.StateId;
            this.Name = model.Data.Name;
            this.curdNum = model.Data.CurdNum;
            SetState();

            this.Ok = new RelayCommand(arg => OkCommand());
            this.Cancel = new RelayCommand(arg => this.model.Cancel(this.stateId));
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void OkCommand()
        {
            int newStateId = GetState();
            if (this.stateId == newStateId)
            {
                this.model.Cancel(newStateId);
                return;
            }
            this.model.Ok(new Card
            {
                CardIdHi = model.Data.CardIdHi,
                CardIdLo = model.Data.CardIdLo,
                StateId = newStateId,
                CurdNum = curdNum
            });
        }

        private void SetState()
        {
            var state = (CardState)stateId;
            switch (state)
            {
                case CardState.Active:
                    StateActive = true;
                    StateActiveEnabled = false;
                    StateInactiveEnabled = true;
                    StateIssuedEnabled = false;
                    StateLostEnabled = false;
                    break;
                case CardState.Inactive:
                    StateInactive = true;
                    StateActiveEnabled = true;
                    StateInactiveEnabled = false;
                    StateIssuedEnabled = false;
                    StateLostEnabled = false;
                    break;
                case CardState.Issued:
                    StateIssued = true;
                    StateActiveEnabled = true;
                    StateInactiveEnabled = true;
                    StateIssuedEnabled = false;
                    StateLostEnabled = true;
                    break;
                case CardState.Lost:
                    StateLost = true;
                    StateActiveEnabled = true;
                    StateInactiveEnabled = true;
                    StateIssuedEnabled = false;
                    StateLostEnabled = false;
                    break;
            }
        }

        private int GetState()
        {
            if (StateInactive)
            {
                return (int)CardState.Inactive;
            }
            if (StateIssued)
            {
                return (int)CardState.Issued;
            }
            if (StateLost)
            {
                return (int)CardState.Lost;
            }

            return (int)CardState.Active;
        }
    }
}
