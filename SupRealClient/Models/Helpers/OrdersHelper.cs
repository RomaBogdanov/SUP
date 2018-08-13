using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models.Helpers
{
	/// <summary>
	/// Класс общих статических функций для работы с классом заявок
	/// </summary>
	public static class OrdersHelper
	{
		/// <summary>
		/// Возвращает строку номера заявки в принятом формате
		/// При ошибке возвращает null
		/// </summary>
		/// <param name="orderId">Id заявки</param>
		/// <returns></returns>
		public static string GetOrderNumber(int orderId)
		{
			var order = OrdersWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg => arg.Field<int>("f_ord_id") == orderId);
			if (order == null)
			{
				return null;
			}

			var orderType = SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
				.FirstOrDefault(arg => arg.Field<int>("f_order_type_id") == order.Field<int>("f_order_type_id"));
			if (orderType == null)
			{
				return order.Field<string>("f_reg_number");
			}
			string type = GetOrderTypeString(orderType.Field<string>("f_order_text"));
			return order.Field<DateTime>("f_new_rec_date").Year % 100 + "-" + order.Field<int>("f_reg_number") +  (string.IsNullOrEmpty(type) ? "" : ("-" + type[0]));
		}

		/// <summary>
		/// Костыль, связанный с тем, что в БД третьим типом заявок является бессрочная заявка, хотя от нее уже отказались.
		/// Поскольку изменять базу данных на данный момент рискованно из-за возможности испортить какой-либо функционал, временно будет использоваться эта функция
		/// </summary>
		/// <param name="dbString"></param>
		/// <returns></returns>
		public static string GetOrderTypeString(string dbString)
		{
			if (dbString == "Бессрочная")
			{
				return "На основании";
			}

			return dbString;
		}

		public static bool IsOrderDataCorrect(this Order order, OrderType orderType, out string errorMessage)
		{
			if (orderType != OrderType.Single && order.From > order.To)
			{
				errorMessage = "Неверные даты. Дата начала заявки раньше даты конца заявки.";
				return false;
			}

			for (int i = 0; i < order.OrderElements.Count; i++)
			{
				if (orderType == OrderType.Virtue)
				{
					if (string.IsNullOrEmpty(order.OrderElements[i].Reason))
					{
						errorMessage = "Отсутсвует основание.";
						return false;
					}
				}

				if (!order.OrderElements[i].IsOrderElementDataCorrect(out errorMessage, orderType == OrderType.Virtue))
				{
					if (orderType != OrderType.Virtue)
					{
						errorMessage = "Ошибка в элементе заявки " + (i + 1) + ": " + errorMessage;
					}
					return false;
				}
			}

			errorMessage = null;
			return true;
		}
	}
}
