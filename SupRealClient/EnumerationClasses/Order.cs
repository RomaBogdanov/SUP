﻿using System;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using SupRealClient.TabsSingleton;

namespace SupRealClient.EnumerationClasses
{
	public class Order : IdEntity
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="autoCreateFirstElement">Создать ли автоматически первый элемент заявки</param>
		public Order(bool autoCreateFirstElement = false)
		{
			OrderElements = new ObservableCollection<OrderElement>();

			if (autoCreateFirstElement)
			{
				OrderElements.Add(new OrderElement(false));
			}

			OnChangeId += Order_OnChangeId;
		}

		private void Order_OnChangeId()
		{
			foreach (OrderElement orderElement in OrderElements)
			{
				orderElement.OrderId = Id;
			}
		}

		private void OrderElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems)
					{
						((OrderElement) item).OrderId = Id;
						AddedOrderElements.Add((OrderElement) item);
					}

					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems)
					{
						DeletedOrderElements.Add((OrderElement) item);
					}

					// перемещаем в список удалённых.
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					throw new Exception("Unexpected Case");
			}
		}

		private int organizationId;
		private int agreeId;

		/// <summary>
		/// Первый элемент заявки
		/// Если лист элементов пуст, возвращает null
		/// </summary>
		public OrderElement FirstOrderElement
		{
			get
			{
				if (OrderElements.Count >= 1)
				{
					return OrderElements[0];
				}

				return null;
			}
		}

		/// <summary>
		/// Номер заявки.
		/// </summary>
		public int Number { get; set; } = 0;

		/// <summary>
		/// Номер типа заявки.
		/// </summary>
		public int TypeId { get; set; } = 0;

		/// <summary>
		/// Название типа заявки.
		/// </summary>
		public string Type
		{
			get
			{
				return SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
					                       TypeId)["f_order_text"].ToString();
			}
			set { }
		}

		/// <summary>
		/// Выводимый пользователю и показываемый ему номер заявки.
		/// </summary>
		public string RegNumber
		{
			get
			{
				if (Type.Length == 0)
				{
					return Number.ToString();
				}

				return Number + "-" + Type[0];
			}
			set
			{
				string num = Regex.Match(value, (@"^\d*")).Value;
				if (num != "") Number = Convert.ToInt32(num);

			}
		}

		/// <summary>
		/// Дата подачи заявки.
		/// </summary>
		public DateTime OrderDate { get; set; }

		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public DateTime RecDate { get; set; }
		public int RecOperatorID { get; set; }
		public string RecOperator
		{
			get
			{
				return UsersWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_user_id") ==
					                       RecOperatorID)["f_user"].ToString();
			}
			set { }
		}

		public DateTime? NewRecDate { get; set; }
		public int NewRecOperatorID { get; set; }
		public string NewRecOperator
		{
			get
			{
				return UsersWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_user_id") ==
					                       NewRecOperatorID)["f_user"].ToString();
			}
			set { }
		}
		public string Barcode { get; set; }
		public int CatcherId { get; set; } = 0; // Id провожающего
		public int ImageId { get; set; } // id исходника скана заявки
		public string Catcher { get; set; } = ""; // провожающий

		public string OrderType { get; set; } = ""; // тип заявки
		public string Passes { get; set; } = "";

		private int signedId;

		/// <summary>
		/// Id подписывающего лица
		/// </summary>
		public int SignedId
		{
			get { return signedId; }
			set
			{
				signedId = value;
				Signed = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
					                       signedId)["f_full_name"].ToString();
			}
		}

		public string Signed { get; set; } = ""; // Полное имя подписывающего

		public int AgreeId // Id согласовавшего
		{
			get { return agreeId; }
			set
			{
				agreeId = value;
				Agree = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
					                       agreeId)["f_visitor_id"].ToString();
			}
		}

		public string Agree { get; set; } = ""; // Полное имя согласовавшего
		public string Note { get; set; } = ""; // Примечание

		public string Organization { get; set; } = "";

		/// <summary>
		/// Показывает, что заявка не активна.
		/// </summary>
		public bool IsDisable { get; set; }

		/// <summary>
		/// Специализированое свойство предназначенное для того, чтобы помещать,
		/// что эту заявку надо выбрать. Изначально вставлено, чтобы удобно было
		/// работать с назначениями заявок, на которые надо ориентироваться при
		/// выдаче пропуска.
		/// </summary>
		public bool IsChecked { get; set; }

		private ObservableCollection<OrderElement> orderElements;

		/// <summary>
		/// Список привязанных к заявке посетителей.
		/// </summary>
		public ObservableCollection<OrderElement> OrderElements
		{
			get { return orderElements; }
			set
			{
				orderElements = value;
				orderElements.CollectionChanged += OrderElements_CollectionChanged;
			}
		} /* =
            new ObservableCollection<OrderElement>();*/

		/// <summary>
		/// Список элементов под удаление.
		/// </summary>
		public ObservableCollection<OrderElement> DeletedOrderElements { get; set; } =
			new ObservableCollection<OrderElement>();

		public ObservableCollection<OrderElement> AddedOrderElements { get; set; } =
			new ObservableCollection<OrderElement>();

		public bool IsOrderDataCorrect(OrderType orderType,out string errorMessage)
		{
			if (OrderElements.Count < 1)
			{
				errorMessage = "Заявка не содержит ни одного элемента.";
				return false;
			}

			if (orderType != EnumerationClasses.OrderType.Single && From > To)
			{
				errorMessage = "Неверные даты. Дата начала заявки раньше даты конца заявки.";
				return false;
			}

			for (int i = 0; i < OrderElements.Count; i++)
			{
				if (orderType == EnumerationClasses.OrderType.Virtue)
				{
					if (string.IsNullOrEmpty(OrderElements[i].Reason))
					{
						errorMessage = "Отсутсвует обоснование.";
						return false;
					}
				}

				if (!OrderElements[i].IsOrderElementDataCorrect(out errorMessage))
				{
					if (i > 0)
					{
						errorMessage = "Элемент заявки " + (i + 1) + ": " + errorMessage;
					}
					return false;
				}
			}

			errorMessage = null;
			return true;
		}
	}
}