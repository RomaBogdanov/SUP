using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupContract;
using System.Collections.Generic;
using SupRealClient.Models.Helpers;

namespace SupRealClient.ViewModels
{
	public class ReturnBidViewModel : ViewModelBase
	{
		public event Action OnClose;

		private Card2 card;
		private string number;
		private int visitorId;
        private DateTime lostDate;

        public ICommand OpenCardsCommand { get; set; }
		public ICommand DeactivateCardCommand { get; set; }
        public ICommand LostCardCommand { get; set; }
        public ICommand ReturnCardCommand { get; set; }
        public ICommand CancelCommand { get; set; }

		public ReturnBidViewModel(Card2 card, int visitorId)
		{
			this.card = card;
			this.number = card != null ? card.CardNumber : "";
			this.visitorId = visitorId;
            this.LostDate = DateTime.Now;

            this.OpenCardsCommand = new RelayCommand(arg => OpenCards());
			this.DeactivateCardCommand = new RelayCommand(arg => DeactivateCard());
            this.LostCardCommand = new RelayCommand(arg => LostCard());
            this.ReturnCardCommand = new RelayCommand(arg => ReturnCard());
            this.CancelCommand = new RelayCommand(arg => Cancel());
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

        public DateTime LostDate
        {
            get { return lostDate; }
            set
            {
                lostDate = value;
                OnPropertyChanged();
            }
        }

        private void OpenCards()
		{
			Base4CardsWindView wind = new Base4CardsWindView(Visibility.Visible);
			((Base4ViewModel<Card>)wind.base4.DataContext).Model =
				new CardsIssuedListModel<Card>();
			((Base4ViewModel<Card>)wind.base4.DataContext).Model.OnClose += wind.Handling_OnClose2;
			wind.ShowDialog();
			var result = wind.WindowResult as BaseModelResult;
			if (result != null)
			{
				DataRow row = CardsWrapper.CurrentTable().Table.Rows.Find(result.Id);
				if (row != null)
				{
					Number = row.Field<int>("f_card_num").ToString();
				}
			}
		}

        private void DeactivateCard()
        {
            KeyValuePair<DataRow, DataRow> rows = FindCard();

            if (rows.Key == null || rows.Value == null)
            {
                MessageBox.Show("Пропуск не найден!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ChangeStateHelper.CanChangeState(
                (CardState)rows.Value.Field<int>("f_state_id"), CardState.Inactive))
            {
                MessageBox.Show("Невозможно деактивировать данный пропуск!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ChangeStateHelper.ChangeState(new Card
            {
                CardIdHi = rows.Key.Field<int>("f_object_id_hi"),
                CardIdLo = rows.Key.Field<int>("f_object_id_lo"),
                StateId = (int)CardState.Inactive,
                Name = rows.Key.Field<string>("f_card_name"),
                CreateDate = DateTime.Now
            });

            OnClose?.Invoke();
        }

        private void LostCard()
        {
            KeyValuePair<DataRow, DataRow> rows = FindCard();

            if (rows.Key == null || rows.Value == null)
            {
                MessageBox.Show("Пропуск не найден!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ChangeStateHelper.CanChangeState(
                (CardState)rows.Value.Field<int>("f_state_id"), CardState.Lost))
            {
                MessageBox.Show("Невозможно утерять данный пропуск!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ChangeStateHelper.ChangeState(new Card
            {
                CardIdHi = rows.Key.Field<int>("f_object_id_hi"),
                CardIdLo = rows.Key.Field<int>("f_object_id_lo"),
                StateId = (int)CardState.Lost,
                Name = rows.Key.Field<string>("f_card_name"),
                CreateDate = DateTime.Now,
                Lost = LostDate
            });

            OnClose?.Invoke();
        }

        private void ReturnCard()
		{
            KeyValuePair<DataRow, DataRow> rows = FindCard();
			
			if (rows.Key == null || rows.Value == null)
			{
				MessageBox.Show("Пропуск не найден!", "Ошибка",
					MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

            if (rows.Value.Field<int>("f_state_id") != (int)CardState.Issued)
            {
                MessageBox.Show("Пропуск не выдан!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ChangeStateHelper.ChangeState(new Card
            {
                CardIdHi = rows.Key.Field<int>("f_object_id_hi"),
                CardIdLo = rows.Key.Field<int>("f_object_id_lo"),
                StateId = (int)CardState.Active,
                Name = rows.Key.Field<string>("f_card_name"),
                CreateDate = DateTime.Now
            });

			OnClose?.Invoke();
		}

        private KeyValuePair<DataRow, DataRow> FindCard()
        {
            DataRow row = null;
            foreach (DataRow r in CardsWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_card_num").ToString() == Number)
                {
                    row = r;
                    break;
                }
            }

            if (row == null)
            {
                return new KeyValuePair<DataRow, DataRow>(null, null);
            }

            DataRow row1 = CardsExtWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg =>
                    arg.Field<int>("f_object_id_lo") == row.Field<int>("f_object_id_lo") &&
                    arg.Field<int>("f_object_id_hi") == row.Field<int>("f_object_id_hi"));

            return new KeyValuePair<DataRow, DataRow>(row, row1);
        }

        private void Cancel()
		{
			OnClose?.Invoke();
		}
	}
}
