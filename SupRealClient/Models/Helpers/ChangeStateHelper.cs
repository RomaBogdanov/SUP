using SupClientConnectionLib;
using SupContract;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace SupRealClient.Models.Helpers
{
    public static class ChangeStateHelper
    {
        public static bool CanChangeState(CardState oldState, CardState newState)
        {
            switch (oldState)
            {
                case CardState.Active:
                    return newState == CardState.Inactive;
                case CardState.Inactive:
                    return newState == CardState.Active;
                case CardState.Issued:
                    return newState == CardState.Active ||
                        newState == CardState.Inactive ||
                        newState == CardState.Lost;
                case CardState.Lost:
                    return newState == CardState.Active ||
                         newState == CardState.Inactive;
            }
            return false;
        }

        public static int ChangeState(Card data)
        {
            CardsExtWrapper cards = CardsExtWrapper.CurrentTable();
            DataRow row = null;
            foreach (DataRow r in cards.Table.Rows)
            {
                if (r.Field<int>("f_object_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_object_id_lo") == data.CardIdLo)
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
            {
                row = cards.Table.NewRow();
                row["f_object_id_hi"] = data.CardIdHi;
                row["f_object_id_lo"] = data.CardIdLo;
                row["f_state_id"] = data.StateId;
                row["f_create_date"] = data.CreateDate;
                row["f_comment"] = "";
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_lost_date"] = data.Lost ?? DateTime.MinValue;
                row["f_last_visit_id"] = 0;
                row["f_deleted"] = "N";
                cards.Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_state_id"] = data.StateId;
                row["f_lost_date"] = data.Lost ?? DateTime.MinValue;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_deleted"] = "N";
                row.EndEdit();
            }

            if (data.StateId == (int)CardState.Active)
            {
                ReturnCard(data);
            }

            // TODO - временно закомментировано
            // К0гда понадобятся данные таблицы vis_card_area, раскомментировать
            /*foreach (DataRow r in CardAreaWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<string>("f_deleted") == "N" &&
                    r.Field<int>("f_card_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_card_id_lo") == data.CardIdLo)
                {
                    r.BeginEdit();
                    r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    r["f_rec_date"] = DateTime.Now;
                    r["f_deleted"] = "Y";
                    r.EndEdit();
                }
            }*/

            // TODO - здесь в Andover выгружается пропуск с пустым списком областей доступа


            var cardId = CardsExtWrapper.CurrentTable().Table.AsEnumerable().Where(x =>
		        x.Field<int>("f_object_id_hi") == data.CardIdHi && x.Field<int>("f_object_id_lo") == data.CardIdLo).First().Field<int>("f_card_id");
	        var cardName = CardsWrapper.CurrentTable().Table.AsEnumerable().Where(x => x.Field<int>("f_card_id") == cardId)
		        .First().Field<string>("f_card_name");

			var data1 = new AndoverExportData
			{
				Card = cardName,
				SchedulesFromSameCAreaSchedules = null,
				IsExtradition = false
			};

			var clientConnector = ClientConnectorFactory.CurrentConnector;

			//смена курсора
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

			if (clientConnector.ExportToAndover(data1).Success??false)
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				MessageBox.Show("Выгрузка в Andover прошла успешно!", "Информация",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				MessageBox.Show("Ошибка при выгрузке в Andover!", "Ошибка",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}

            return data.StateId;
        }

        private static void ReturnCard(Card data)
        {
            foreach (DataRow r in VisitsWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<string>("f_deleted") == "N" &&
                    r.Field<int>("f_card_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_card_id_lo") == data.CardIdLo)
                {
                    r.BeginEdit();
                    r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    r["f_rec_date"] = DateTime.Now;
                    r["f_deleted"] = "Y";
                    r.EndEdit();
                }
            }
        }
    }
}
