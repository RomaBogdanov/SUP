using SupRealClient.EnumerationClasses;
using SupRealClient.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class DeactivateCardsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action OnClose;

        public string Message { get; private set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public ObservableCollection<Card2Checked> Cards { get; set; }

        public DeactivateCardsViewModel(IEnumerable<Card2> cards)
        {
            Cards = new ObservableCollection<Card2Checked>();
            foreach (var card in cards)
            {
                Cards.Add(new Card2Checked
                {
                    CardIdHi = card.CardIdHi,
                    CardIdLo = card.CardIdLo,
                    StateId = card.StateId,
                    CardNumber = card.CardNumber
                });
            }
            this.Ok = new RelayCommand(arg => OkCommand());
            this.Cancel = new RelayCommand(arg => OnClose?.Invoke());
        }

        private void OkCommand()
        {
            foreach (var card in Cards.Where(c => c.IsChecked))
            {
                ChangeStateHelper.ChangeState(card.ToInactiveCard());
            }
            OnClose?.Invoke();
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }

    public class Card2Checked : Card2
    {
        public bool IsChecked { get; set; }

        public Card ToInactiveCard()
        {
            return new Card
            {
                CardIdHi = this.CardIdHi,
                CardIdLo = this.CardIdLo,
                StateId = (int)CardState.Inactive,
                CurdNum = int.Parse(this.CardNumber),
                ChangeDate = DateTime.Now
            };
        }
    }
}
