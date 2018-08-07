using System;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
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
				Id = CurrentItem.Id,
				Name = CurrentItem.RegNumber,
				From = CurrentItem.From,
				To = CurrentItem.To,
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
					Id = orders.Field<int>("f_ord_id"),
					RegNumber = orders.Field<int>("f_reg_number").ToString(),
					From = orders.Field<DateTime>("f_date_from"),
					To = orders.Field<DateTime>("f_date_to"),
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
				{"From", "Дата начала"},
				{"To", "Дата окончания"},
				{"Note", "Примечание"},
			};
		}

		protected override DataTable Table
		{
			get { return OrdersWrapper.CurrentTable().Table; }
		}
	}
}