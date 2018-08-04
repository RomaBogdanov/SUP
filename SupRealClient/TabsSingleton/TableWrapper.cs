using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using SupClientConnectionLib;
using SupRealClient.Common;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.TabsSingleton
{
	class TableWrapper : IDisposable
	{
		protected DataTable table;
		protected ClientConnector connector;
		protected static List<TableWrapper> wrappers = new List<TableWrapper>();
		public event Action OnChanged;

		public DataTable Table
		{
			get { return this.table; }
		}

		public ClientConnector Connector
		{
			get { return this.connector; }
		}

		protected TableWrapper()
		{
			this.connector = new ClientConnector();
			this.connector.OnInsert += Connector_OnInsert;
			this.connector.OnUpdate += Connector_OnUpdate;
			this.connector.OnDelete += Connector_OnDelete;
		}

		public virtual void Dispose()
		{
			this.connector.OnInsert -= Connector_OnInsert;
			this.connector.OnUpdate -= Connector_OnUpdate;
			this.connector.OnDelete -= Connector_OnDelete;
			if (this.table != null)
			{
				this.table.RowChanged -= Table_RowChanged;
				this.table.RowDeleting -= Table_RowDeleting;
			}
		}

		public static void Reset()
		{
			foreach (var wrapper in wrappers)
			{
				wrapper.Dispose();
			}

			wrappers.Clear();
		}

		/// <summary>
		/// Добавляет строку в таблицу, при этом дописывает
		/// обязательные для заполнения поля. Желательно использовать
		/// данную конструкцию, а не стандарт
		/// OrdersWrapper.CurrentTable().Table.Rows.Add(row);
		/// </summary>
		/// <param name="row"></param>
		public virtual void AddRow(DataRow row)
		{
			if (table != null)
			{
				StandartCols(row);
				table.Rows.Add(row);
			}
		}

		/// <summary>
		/// Добавляет строку, но уже в виде объекта.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		public virtual void AddRow<T>(T obj) where T : IdEntity
		{
		}

		/// <summary>
		/// Редактирование строки.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		public virtual void UpdateRow<T>(T obj) where T : IdEntity
		{
		}

		/// <summary>
		/// Стандартная процедура получения коллекции объектов соответствующих
		/// таблице.
		/// </summary>
		/// <returns></returns>
		public virtual object StandartQuery()
		{
			return null;
		}

		/// <summary>
		/// Работает со стандартными колонками любой таблицы БД:
		/// удаляется ли строка, время последнего изменения, изменивший
		/// оператор.
		/// </summary>
		/// <param name="row"></param>
		protected void StandartCols(DataRow row)
		{
			row["f_deleted"] = 'N';
			row["f_rec_date"] = DateTime.Now;
			row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
		}

		/// <summary>
		/// Работает со стандартными колонками. При этом можно задавать
		/// статус строки - удалена или нет.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="entity"></param>
		protected void StandartCols(DataRow row, IdEntity entity)
		{
			row["f_deleted"] = entity.IsDeleted == false ? "N" : "Y";
			row["f_rec_date"] = DateTime.Now;
			row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
		}

		protected void Subscribe()
		{
			this.table.RowChanged += Table_RowChanged;
			this.table.RowDeleting += Table_RowDeleting;
			this.table.RowDeleted += Table_RowDeleted;
		}

		private void Connector_OnInsert(string tableName, object[] objs)
		{
			if (tableName == table.TableName && !this.table.Rows.Contains(objs[0]))
			{
				try
				{
					table.Rows.Add(objs);
					this.OnChanged();
				}
				catch
				{

				}
			}
		}

		private void Connector_OnUpdate(string tableName, int rowNumber, object[] objs)
		{
			if (tableName == table.TableName)
			{
				DataRow row = table.Rows.Find(objs[0]);
				if (row != null)
				{
					int l = row.ItemArray.Length;
					for (int i = 0; i < l; i++)
					{
						if (row.Table.Columns[i].DataType == typeof(int))
						{
							objs[i] = objs[i].ToString() == "" ? 0 : objs[i];
						}
					}

					row.ItemArray = objs;
					table.AcceptChanges();
					this.OnChanged?.Invoke();
				}
			}
		}

		private void Connector_OnDelete(string tableName, object[] objs)
		{
			if (tableName == table.TableName)
			{
				DataRow row = table.Rows.Find(objs[0]);
				if (row != null)
				{
					table.Rows.Remove(row);
					this.OnChanged();
				}
			}
		}

		private void Table_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			this.table.AcceptChanges();
			this.OnChanged();
		}

		private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			this.connector.DeleteRow(e.Row.ItemArray);
			//System.Threading.Thread.Sleep(300);
			/*DataTable dt = (DataTable)sender;
			this.connector.DeleteRow(e.Row.ItemArray);
			dt.AcceptChanges();*/
			//throw new NotImplementedException();
		}

		private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			// TODO: перевести взаимодействие таблиц с сервером в табличные обёртки. 
			DataTable dt;
			switch (e.Action)
			{
				case DataRowAction.Nothing:
					break;
				case DataRowAction.Delete:
					this.OnChanged();
					break;
				case DataRowAction.Change:
					dt = (DataTable) sender;
					int i = dt.Rows.IndexOf(e.Row);
					this.OnChanged?.Invoke();
					this.Connector.UpdateRow(e.Row.ItemArray, i);
					break;
				case DataRowAction.Rollback:
					break;
				case DataRowAction.Commit:
					break;
				case DataRowAction.Add:
					dt = (DataTable) sender;
					this.OnChanged?.Invoke();
					this.Connector.InsertRow(e.Row.ItemArray);
					break;
				case DataRowAction.ChangeOriginal:
					break;
				case DataRowAction.ChangeCurrentAndOriginal:
					break;
				default:
					break;
			}
		}
	}

	partial class OrdersWrapper
	{
		public override void AddRow<T>(T obj)
		{
			Order order = obj as Order;
			if (order == null) return;
			//base.AddRow(obj);
			DataRow row = this.table.NewRow();
			order.Id = (int) row["f_ord_id"];
			row["f_reg_number"] = order.Number;
			row["f_order_type_id"] = order.TypeId;
			row["f_ord_date"] = order.OrderDate == null ? DateTime.MinValue : order.OrderDate;
			row["f_date_from"] = order.From == null ? DateTime.MinValue : order.From;
			row["f_date_to"] = order.To == null ? DateTime.MinValue : order.To;
			row["f_signed_by"] = order.SignedId;
			row["f_rec_date"] = order.RecDate;
			row["f_image_id"] = order.ImageId;
			row["f_rec_operator"] = order.RecOperatorID;
			row["f_new_rec_date"] = order.NewRecDate;
			row["f_new_rec_operator"] = order.NewRecOperatorID;
			row["f_barcode"] = order.Barcode;
			row["f_adjusted_with"] = order.AgreeId;
			row["f_notes"] = order.Note;
			row["f_disabled"] = order.IsDisable ? "Y" : "N";
			row["f_temp_posted"] = ""; // todo: непонятное поле
			AddRow(row);
			foreach (OrderElement orderElement in order.OrderElements)
			{
				OrderElementsWrapper.CurrentTable().AddRow(orderElement);
			}
		}

		public override void UpdateRow<T>(T obj)
		{
			Order order = obj as Order;
			if (order == null) return;
			base.UpdateRow(obj);

			DataRow row = OrdersWrapper.CurrentTable()
				.Table.Rows.Find(order.Id);
			row.BeginEdit();
			row["f_reg_number"] = order.Number;
			row["f_order_type_id"] = order.TypeId;
			row["f_ord_date"] = DateTime.MinValue; //todo: доработать
			row["f_date_from"] = order.From == null ? DateTime.MinValue : order.From;
			row["f_date_to"] = order.To == null ? DateTime.MinValue : order.To;
			row["f_signed_by"] = order.SignedId;
			row["f_rec_date"] = order.RecDate;
			row["f_image_id"] = order.ImageId;
			row["f_rec_operator"] = order.RecOperatorID;
			row["f_new_rec_date"] = order.NewRecDate;
			row["f_new_rec_operator"] = order.NewRecOperatorID;
			row["f_barcode"] = order.Barcode;
			row["f_adjusted_with"] = order.AgreeId;
			row["f_notes"] = order.Note;
			row["f_disabled"] = "N"; // todo: судя по всему, поле показывает, что заявка неактивна.
			row["f_temp_posted"] = ""; // todo: непонятное поле
			StandartCols(row);
			row.EndEdit();
			// todo: реализовать процедуру изменения привязанных к заявке элементов.
			// todo: добавление элементов заявки
			foreach (OrderElement item in order.AddedOrderElements)
			{
				//item.OrderId = order.Id;
				OrderElementsWrapper.CurrentTable().AddRow(item);
			}

			// todo: удаление элементов заявки
			order.DeletedOrderElements.Select(arg => arg.IsDeleted = true);
			foreach (var item in order.DeletedOrderElements)
			{
				OrderElementsWrapper.CurrentTable().UpdateRow(item);
			}

			// todo: редактирование элементов заявки
			foreach (var item in order.OrderElements)
			{
				if (!(order.AddedOrderElements.Contains(item) ||
				      order.DeletedOrderElements.Contains(item)))
				{
					OrderElementsWrapper.CurrentTable().UpdateRow(item);
				}
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>возвращает перечисление вида: ObservableCollection&lt;Order&gt;</returns>
		public override object StandartQuery()
		{
			ObservableCollection<Order> orders = new ObservableCollection<Order>(
				from ords in OrdersWrapper.CurrentTable().Table.AsEnumerable()
				where ords.Field<int>("f_ord_id") != 0 & ords.Field<int>("f_order_type_id") != 0 &&
				      CommonHelper.NotDeleted(ords)
				select new Order
				{
					Id = ords.Field<int>("f_ord_id"),
					Number = ords.Field<int>("f_reg_number"),
					TypeId = ords.Field<int>("f_order_type_id"),
					OrderDate = ords.Field<DateTime>("f_ord_date"),
					From = ords.Field<DateTime>("f_date_from"),
					To = ords.Field<DateTime>("f_date_to"),
					SignedId = ords.Field<int>("f_signed_by"),
					RecDate = ords.Field<DateTime>("f_rec_date"),
					Barcode = ords.Field<string>("f_barcode"),
					ImageId = ords.Field<int>("f_image_id"),
					RecOperatorID = ords.Field<int>("f_rec_operator"),
					NewRecDate = ords.Field<DateTime?>("f_new_rec_date"),
					NewRecOperatorID = ords.Field<int>("f_new_rec_operator"),
					AgreeId = ords.Field<int>("f_adjusted_with"),
					Note = ords.Field<string>("f_notes"),
					IsDisable = ords.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
					OrderElements = new ObservableCollection<OrderElement>(
						from row in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
						where row.Field<int>("f_ord_id") == ords.Field<int>("f_ord_id") &&
						      CommonHelper.NotDeleted(row)
						select new OrderElement((OrderType) ords.Field<int>("f_order_type_id") == OrderType.Single)
						{
							Id = row.Field<int>("f_oe_id"),
							OrderId = row.Field<int>("f_ord_id"),
							VisitorId = row.Field<int>("f_visitor_id"),
							OrganizationId = row.Field<int?>("f_org_id"),
							Position = row.Field<string>("f_position"),
							CatcherId = row.Field<int>("f_catcher_id"),
							From = row.Field<DateTime>("f_time_from"),
							To = row.Field<DateTime>("f_time_to"),
							Reason =  row.Field<string>("f_other_org"),
							IsDisable = row.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
							Passes = row.Field<string>("f_passes")
						})
				});
			return orders;
		}
	}

	partial class OrderElementsWrapper
	{
		public override void AddRow<T>(T obj)
		{
			OrderElement orderElement = obj as OrderElement;
			if (orderElement == null) return;
			base.AddRow(obj);
			DataRow row = this.table.NewRow();
			row["f_ord_id"] = orderElement.OrderId;
			row["f_visitor_id"] = orderElement.VisitorId;
			row["f_catcher_id"] = orderElement.CatcherId;
			row["f_time_from"] = orderElement.From;
			row["f_time_to"] = orderElement.To;
			row["f_passes"] = orderElement.Passes;
			row["f_disabled"] = orderElement.IsDisable ? "Y" : "N";
			row["f_not_remaind"] = "N"; //todo: разобраться
			row["f_full_role"] = "N"; //todo: разобраться
			row["f_other_org"] = orderElement.Reason; //todo: изменить название столбца в БД
			row["f_org_id"] = orderElement.OrganizationId;
			row["f_position"] = orderElement.Position;
			AddRow(row);
			foreach (Area relAreas in orderElement.Areas)
			{
				AreaOrderElement aoe = new AreaOrderElement
				{
					OrderElementId =
						(int) row["f_oe_id"],
					AreaIdHi = relAreas.ObjectIdHi,
					AreaIdLo = relAreas.ObjectIdLo
				};
				AreaOrderElementWrapper.CurrentTable().AddRow(aoe);
			}
		}

		public override void UpdateRow<T>(T obj)
		{
			OrderElement orderElement = obj as OrderElement;
			if (orderElement == null) return;
			base.UpdateRow(obj);

			DataRow row = OrderElementsWrapper.CurrentTable()
				.Table.Rows.Find(orderElement.Id);
			row.BeginEdit();
			row["f_ord_id"] = orderElement.OrderId;
			row["f_visitor_id"] = orderElement.VisitorId;
			row["f_catcher_id"] = orderElement.CatcherId;
			row["f_time_from"] = orderElement.From;
			row["f_time_to"] = orderElement.To;
			row["f_passes"] = orderElement.Passes;
			row["f_disabled"] = orderElement.IsDisable ? "Y" : "N";
			row["f_not_remaind"] = "N"; //todo: разобраться
			row["f_full_role"] = "N"; //todo: разобраться
			row["f_other_org"] = orderElement.Reason; //todo: изменить название столбца в БД
			row["f_org_id"] = orderElement.OrganizationId;
			row["f_position"] = orderElement.Position;
			StandartCols(row, orderElement);
			row.EndEdit();
		}

		public override object StandartQuery()
		{
			ObservableCollection<OrderElement> orderElements =
				new ObservableCollection<OrderElement>(
					from ordEls in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
					join order in OrdersWrapper.CurrentTable().Table.AsEnumerable() on ordEls.Field<int>("f_ord_id") equals
						order.Field<int>("f_ord_id")
					where ordEls.Field<int>("f_ord_id") != 0 && CommonHelper.NotDeleted(ordEls)
					select new OrderElement((OrderType) (order.Field<int>("f_order_type_id")) == OrderType.Single)
					{
						Id = ordEls.Field<int>("f_oe_id"),
						OrderId = ordEls.Field<int>("f_ord_id"),
						VisitorId = ordEls.Field<int>("f_visitor_id"),
						OrganizationId = ordEls.Field<int>("f_org_id"),
						Position = ordEls.Field<string>("f_position"),
						CatcherId = ordEls.Field<int>("f_catcher_id"),
						From = ordEls.Field<DateTime>("f_time_from"),
						To = ordEls.Field<DateTime>("f_time_to"),
						Reason = ordEls.Field<string>("f_other_org"),
						IsDisable = ordEls.Field<string>("f_disabled").ToUpper() == "Y" ? true : false,
						Passes = ordEls.Field<string>("f_passes")
					});

			return orderElements;
		}
	}

	partial class CardsWrapper
	{
		public override void AddRow<T>(T obj)
		{
			Card card = obj as Card;
			if (card == null) return;
			DataRow row = CurrentTable().Table.NewRow();
			row["f_card_num"] = card.CurdNum;
			row["f_create_date"] = card.CreateDate;
			row["f_state_id"] = 1;
			row["f_card_text"] = card.NumMAFW;
			row["f_comment"] = card.Comment;
			row["f_lost_date"] = DateTime.MinValue;
			row["f_last_visit_id"] = 0;
			StandartCols(row);
			CurrentTable().Table.Rows.Add(row);
		}
	}

	partial class AreaOrderElementWrapper
	{
		public override void AddRow<T>(T obj)
		{
			AreaOrderElement aoe = obj as AreaOrderElement;
			if (aoe == null)
			{
				return;
			}

			DataRow row = CurrentTable().Table.NewRow();
			row["f_oe_id"] = aoe.OrderElementId;
			row["f_area_id_hi"] = aoe.AreaIdHi;
			row["f_area_id_lo"] = aoe.AreaIdLo;
			StandartCols(row);
			CurrentTable().Table.Rows.Add(row);
		}
	}
}