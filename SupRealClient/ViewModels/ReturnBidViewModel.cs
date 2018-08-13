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

namespace SupRealClient.ViewModels
{
	public class ReturnBidViewModel : ViewModelBase
	{
		public event Action OnClose;

		private Card2 card;
		private string number;
		private int visitorId;

		public ICommand OpenCardsCommand { get; set; }
		public ICommand ReturnCardCommand { get; set; }
		public ICommand CancelCommand { get; set; }

		public ReturnBidViewModel(Card2 card, int visitorId)
		{
			this.card = card;
			this.number = card != null ? card.CardNumber : "";
			this.visitorId = visitorId;

			this.OpenCardsCommand = new RelayCommand(arg => OpenCards());
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

		private void ReturnCard()
		{
			DataRow row = null;
			var cardName = "";
			foreach (DataRow r in CardsWrapper.CurrentTable().Table.Rows)
			{
				if (r.Field<int>("f_card_num").ToString() == Number)
				{
					row = r;
					cardName = r.Field<string>("f_card_name");
					break;
				}
			}

			if (row == null)
			{
				MessageBox.Show("Пропуск не найден!", "Ошибка",
					MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

            DataRow row1 = CardsExtWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg =>
                    arg.Field<int>("f_object_id_lo") == row.Field<int>("f_object_id_lo") &&
                    arg.Field<int>("f_object_id_hi") == row.Field<int>("f_object_id_hi"));
            if (row1 == null)
            {
                MessageBox.Show("Пропуск не найден!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (row1.Field<int>("f_state_id") != 3)
            {
                MessageBox.Show("Пропуск не выдан!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            row1.BeginEdit();
            row1["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row1["f_rec_date"] = DateTime.Now;
            row1["f_state_id"] = 1;
            row1.EndEdit();

            foreach (DataRow r in VisitsWrapper.CurrentTable().Table.Rows)
			{
				if ( //r.Field<int>("f_visitor_id") == visitorId &&
					r.Field<string>("f_deleted") == "N" &&
					r.Field<int>("f_card_id_hi") == row.Field<int>("f_object_id_hi") &&
					r.Field<int>("f_card_id_lo") == row.Field<int>("f_object_id_lo"))
				{
					r.BeginEdit();
					r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
					r["f_rec_date"] = DateTime.Now;
					r["f_deleted"] = "Y";
					r.EndEdit();
				}
			}

			foreach (DataRow r in CardAreaWrapper.CurrentTable().Table.Rows)
			{
				if (r.Field<string>("f_deleted") == "N" &&
				    r.Field<int>("f_card_id_hi") == row.Field<int>("f_object_id_hi") &&
				    r.Field<int>("f_card_id_lo") == row.Field<int>("f_object_id_lo"))
				{
					r.BeginEdit();
					r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
					r["f_rec_date"] = DateTime.Now;
					r["f_deleted"] = "Y";
					r.EndEdit();
				}
			}

			// TODO - здесь выгрузить в Andover
			// Предположительно понадобятся поля:
			// - row["f_card_num"] 
			// список областей доступа и список расписаний  - ПУСТЫЕ!!!

			var data = new AndoverExportData
			{
				Card = cardName,
				SchedulesFromSameCAreaSchedules = null,
				IsExtradition = false
			};

			var clientConnector = ClientConnector.CurrentConnector;

			//смена курсора
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

			if (clientConnector.ExportToAndover(data).Success??false)
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				System.Windows.MessageBox.Show("Возврат пропуска прошел успешно!", "Информация",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				System.Windows.MessageBox.Show("Ошибка при возврате пропуска!", "Ошибка",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}

			OnClose?.Invoke();
		}

		private void Cancel()
		{
			OnClose?.Invoke();
		}
	}
}
