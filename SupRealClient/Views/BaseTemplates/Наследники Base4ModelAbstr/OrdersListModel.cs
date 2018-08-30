using System;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
using System.Linq;
using SupRealClient.Common;
using System.Collections.Generic;
using System.Windows;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.ViewModels;

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
				// Такой же код есть в VisitsView.xaml.cs todo: избавиться от повторения кода
				var bidsModel = new BidsModel();
				bidsModel.CurrentOrder = CurrentItem;
				var typeId = CurrentItem.TypeId - 1;

				var viewModel = new BidsViewModel()
				{
					BidsModel = bidsModel,
					CurrentOrder = CurrentItem,
					SelectedIndex = typeId
				};

				switch (typeId)
				{
					case 0:
						viewModel.CurrentSingleOrder = CurrentItem;
						break;
					case 1:
						viewModel.CurrentTemporaryOrder = CurrentItem;
						break;
					case 2:
						viewModel.CurrentVirtueOrder = CurrentItem;
						break;
				}

				var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
				ViewManager.Instance.OpenWindow("BidsView", viewModel, owner as IWindow);
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

		public override void Watch()
		{
			if (CurrentItem != null)
			{
				// Такой же код есть в VisitsView.xaml.cs todo: избавиться от повторения кода
				var bidsModel = new BidsModel();
				bidsModel.CurrentOrder = CurrentItem;
				var typeId = CurrentItem.TypeId - 1;

				var viewModel = new BidsViewModel()
				{
					BidsModel = bidsModel,
					CurrentOrder = CurrentItem,
					SelectedIndex = typeId
				};

				switch (typeId)
				{
					case 0:
						viewModel.CurrentSingleOrder = CurrentItem;
						break;
					case 1:
						viewModel.CurrentTemporaryOrder = CurrentItem;
						break;
					case 2:
						viewModel.CurrentVirtueOrder = CurrentItem;
						break;
				}

				var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
				ViewManager.Instance.OpenWindow("BidsView", viewModel, owner as IWindow);
			}
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
					Number = orders.Field<int>("f_reg_number"),
					TypeId = orders.Field<int>("f_order_type_id"),
					OrderDate = orders.Field<DateTime>("f_ord_date"),
					From = orders.Field<DateTime>("f_date_from"),
					To = orders.Field<DateTime>("f_date_to"),
					SignedId = orders.Field<int>("f_signed_by"),
					Barcode = orders.Field<string>("f_barcode"),
					ImageId = orders.Field<int>("f_image_id"),
					RecDate = orders.Field<DateTime>("f_rec_date"),
					RecOperatorID = orders.Field<int>("f_rec_operator"),
					NewRecDate = orders.Field<DateTime?>("f_new_rec_date"),
					NewRecOperatorID = orders.Field<int>("f_new_rec_operator"),
					AgreeId = orders.Field<int>("f_adjusted_with"),
					Note = orders.Field<string>("f_notes"),
					IsDisable = orders.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
					OrderElements = new ObservableCollection<OrderElement>(
						from row in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
						where row.Field<int>("f_ord_id") == orders.Field<int>("f_ord_id") &&
							  CommonHelper.NotDeleted(row)
						select new OrderElement((OrderType)orders.Field<int>("f_order_type_id") == OrderType.Single)
						{
							Id = row.Field<int>("f_oe_id"),
							OrderId = row.Field<int>("f_ord_id"),
							VisitorId = row.Field<int>("f_visitor_id"),
							OrganizationId = row.Field<int?>("f_org_id"),
							Position = row.Field<string>("f_position"),
							CatcherId = row.Field<int>("f_catcher_id"),
							From = row.Field<DateTime>("f_time_from"),
							To = row.Field<DateTime>("f_time_to"),
							Passes = row.Field<string>("f_passes"),
							IsDisable = row.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
							IsBlock = CommonHelper.StringToBool(VisitorsWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(item => item.Field<int>("f_visitor_id") == row.Field<int>("f_visitor_id"))?.Field<string>("f_persona_non_grata")),
							IsCardIssued = true,
							Reason = row.Field<string>("f_other_org"),
							TemplateIdList = row.Field<string>("f_oe_templates"),
							AreaIdList = row.Field<string>("f_oe_areas"),
							ScheduleId = row.Field<int>("f_schedule_id"),
							Schedule = row.Field<int>("f_schedule_id") == 0 ? "" :
								SchedulesWrapper.CurrentTable()
								.Table.AsEnumerable().FirstOrDefault(
								arg => arg.Field<int>("f_schedule_id") ==
								row.Field<int>("f_schedule_id"))?.Field<string>("f_schedule_name"),
						})
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