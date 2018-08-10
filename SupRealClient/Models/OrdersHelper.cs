using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	public static class OrdersHelper
	{
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
	}
}
