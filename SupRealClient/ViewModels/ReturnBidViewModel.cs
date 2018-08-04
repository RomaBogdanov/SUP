using SupRealClient.EnumerationClasses;

namespace SupRealClient.ViewModels
{
    public class ReturnBidViewModel : ViewModelBase
    {
        private Card2 card;
        private string number;

        public ReturnBidViewModel(Card2 card)
        {
            this.card = card;
            this.number = card != null ? card.Card : "";
        }

        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }
    }
}
