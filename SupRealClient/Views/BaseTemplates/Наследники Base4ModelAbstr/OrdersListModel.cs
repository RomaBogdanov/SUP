using System;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
using System.Linq;
using SupRealClient.Common;
using System.Collections.Generic;

namespace SupRealClient.Views
{
	public class OrdersListModel<T> : Base4ModelAbstr<T>
		where T : EnumerationClasses.Order, new()
	{
		public OrdersListModel()
		{
			OrdersWrapper.CurrentTable().OnChanged += Query;
			Query();
			Begin();
		}

		#region BtnHandlers

		public override void Add()
		{
			ViewManager.Instance.OpenWindow("BidsView", this.Parent);
		}

		public override void Farther()
		{
			SetAt(searchResult.Next());
		}


		public override void Update()
		{
			if (CurrentItem != null)
			{
				ViewManager.Instance.OpenWindow("BidsView", this.Parent);
			}
		}

		#endregion

		protected override BaseModelResult GetResult()
		{
			return new OrdersModelResult()
			{
				Name = CurrentItem.RegNumber,
				IsDisable = CurrentItem.IsDisable,
				From = CurrentItem.From,
				To = CurrentItem.To,
				Agree = CurrentItem.Agree,
				Signed = CurrentItem.Signed,
				Notes = CurrentItem.Note
			};
		}

		protected override void DoQuery()
		{
			Set = new ObservableCollection<T>(
				from orders in OrdersWrapper.CurrentTable().Table.AsEnumerable()
				where orders.Field<int>("f_ord_id") != 0 &&
				      CommonHelper.NotDeleted(orders)
				select new T
				{
					RegNumber = orders.Field<int>("f_reg_number").ToString(),
					IsDisable = orders.Field<string>("f_disabled") == "Y",
					From = orders.Field<DateTime>("f_date_from"),
					To = orders.Field<DateTime>("f_date_to"),
					TypeId = orders.Field<int>("f_order_type_id"),
					Signed = VisitorsWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg => arg.Field<int>("f_visitor_id") == orders.Field<int>("f_signed_by"))["f_full_name"].ToString(),
					Agree = VisitorsWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg => arg.Field<int>("f_visitor_id") == orders.Field<int>("f_adjusted_with"))["f_full_name"].ToString(),
					Note = orders.Field<string>("f_notes")
				}
			);
		}

		public override long GetId(int index)
		{
			return Rows[index].Field<int>("f_ord_id");
		}

		public override IDictionary<string, string> GetFields()
		{
			return new Dictionary<string, string>
			{
				{"RegNumber", "Номер заявки"},
				{"IsDisable", "Неактивна"},
				{"From", "Дата начала"},
				{"To", "Дата окончания"},
				{"Signed", "Подписано"},
				{"Agree", "Согласовано"},
				{"Note", "Примечание"},
			};
		}

		protected override DataTable Table
		{
			get { return OrdersWrapper.CurrentTable().Table; }
		}
	}
}