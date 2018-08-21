using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.Models.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;
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
        private DateTime lostDate;

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

        public DateTime LostDate
        {
            get { return lostDate; }
            set
            {
                lostDate = value;
                OnPropertyChanged("LostDate");
            }
        }

        public ICommand Remove { get; set; }
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
            this.LostDate = DateTime.Now;
            SetState();

            this.Remove = new RelayCommand(arg => RemoveCommand());
            this.Ok = new RelayCommand(arg => OkCommand());
            this.Cancel = new RelayCommand(arg => this.model.Cancel(this.stateId));
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void RemoveCommand()
        {
            if (MessageBox.Show("Удалить пропуск?", "Внимание",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            this.model.Remove(model.Data.CardIdHi, model.Data.CardIdLo);
        }

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
                CurdNum = curdNum,
                Lost = StateLost ? lostDate : DateTime.MinValue
            });
        }

        private void SetState()
        {
            var state = (CardState)stateId;
            switch (state)
            {
                case CardState.Active:
                    StateActive = true;
                    break;
                case CardState.Inactive:
                    StateInactive = true;
                    break;
                case CardState.Issued:
                    StateIssued = true;
                    break;
                case CardState.Lost:
                    StateLost = true;
                    break;
            }
            StateActiveEnabled =
                ChangeStateHelper.CanChangeState(state, CardState.Active);
            StateInactiveEnabled =
                ChangeStateHelper.CanChangeState(state, CardState.Inactive);
            StateIssuedEnabled =
                ChangeStateHelper.CanChangeState(state, CardState.Issued);
            StateLostEnabled =
                ChangeStateHelper.CanChangeState(state, CardState.Lost);
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
