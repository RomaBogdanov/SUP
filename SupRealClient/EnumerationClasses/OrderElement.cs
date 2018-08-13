using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;
using SupRealClient.Models;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Models.Helpers;

namespace SupRealClient.EnumerationClasses
{
	/// <summary>
	/// Описание посетителя по заявке.
	/// </summary>
	public class OrderElement : IdEntity, INotifyPropertyChanged, ICloneable
	{
		private int _orderId;
		private int _visitorId;
		private int _organizationId;
		private int _catcherId;
        private int _scheduleId;

		private string _visitor = "";
		private string _position = "";
		private string _reason;
		private DateTime _from = DateTime.MinValue;
		private DateTime _to = DateTime.MinValue;
		private string _templateIdList = "";
		private string _areaIdList = "";
		private string _schedule = "";

		private bool _isBlock;
		private CardState _cardState = CardState.Inactive;
		private string _cardStateString;

		private const int DefaultFromHour = 9;
		private const int DefaultToHour = 18;

		/// <summary>
		/// Конструктор элемента заявки
		/// </summary>
		/// <param name="isTimeEditable">Может ли пользователь изменять время заявки</param>
		public OrderElement(bool isTimeEditable)
		{
			IsDateTimeDisplayed = isTimeEditable;
		}

		/// <summary>
		/// Конструктор элемента заявки
		/// </summary>
		/// <param name="isTimeEditable">Может ли пользователь изменять время заявки</param>
		/// <param name="from">Начало действия</param>
		/// <param name="to">Конец действия</param>
		public OrderElement(bool isTimeEditable, DateTime from, DateTime to) : this(isTimeEditable)
		{
			if (isTimeEditable)
			{
				From = new DateTime(from.Year, from.Month, from.Day, DefaultFromHour, 0, 0);
				To = new DateTime(to.Year, to.Month, to.Day, DefaultToHour, 0, 0);
			}
			else
			{
				From = new DateTime(from.Year, from.Month, from.Day, 0, 00, 00);
				To = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);
			}
		}

		/// <summary>
		/// Уникальный номер заявки
		/// </summary>
		public int OrderId
		{
			get => _orderId;
			set
			{
				_orderId = value;
				if (_orderId == 0)
				{
					return;
				}
				DataRow row = OrdersWrapper.CurrentTable().Table.Rows.Find(_orderId);
				if (row == null)
				{
					return;
				}
				OrderType = (OrderType)row.Field<int>("f_order_type_id");
				OrderName = OrdersHelper.GetOrderNumber(row.Field<int>("f_ord_id"));
			}
		}

		public string OrderName { get; set; }
		public OrderType OrderType
		{
			get;
			set;
		}

		/// <summary>
		/// Посетитель по id. Автоматически задает свойство полного имени посетителя.
		/// </summary>
		public int VisitorId
		{
			get => _visitorId;
			set
			{
				_visitorId = value;
				DataRow row = VisitorsWrapper.CurrentTable().Table
					.AsEnumerable().FirstOrDefault(arg =>
						arg.Field<int>("f_visitor_id") == _visitorId);
				if (row != null)
				{
					Visitor = row["f_full_name"].ToString();
					VisitorMainPosition = row["f_job"].ToString();
					SetupCardState();
				}
				else
				{
					MessageBox.Show("Поле посетителя ссылается на несуществующего в базе посетителя по id = " + _visitorId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		/// <summary>
		/// Полное имя посетителя
		/// </summary>
		public string Visitor
		{
			get => _visitor;
			private set
			{
				_visitor = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Организация по id. Автоматически задает свойство названия организации
		/// </summary>
		public int? OrganizationId
		{
			get => _organizationId;
			set
			{
				_organizationId = value ?? 0;
				Organization = OrganizationsHelper.GenerateFullName(_organizationId, true);
			}
		}

		/// <summary>
		/// Название организации
		/// </summary>
		public string Organization { get; private set; } = "";

		/// <summary>
		/// Основная должность сотрудника
		/// </summary>
		public string VisitorMainPosition { get; set; } = "";

		/// <summary>
		/// Должность сотрудника в заявке
		/// </summary>
		public string Position
		{
			get => _position;
			set
			{
				_position = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Сопровождающий по id. Автоматически задает полное имя сопровождающего
		/// </summary>
		public int CatcherId
		{
			get => _catcherId;
			set
			{
				_catcherId = value;
				Catcher = GetStringFromDb(VisitorsWrapper.CurrentTable().Table, "f_visitor_id", _catcherId, "f_full_name");
			}
		}

		/// <summary>
		/// Полное имя сопровождающего
		/// </summary>
		public string Catcher { get; private set; } = "";

		/// <summary>
		/// Оснвование. Использование дял заявок на основании
		/// </summary>
		public string Reason
		{
			get => _reason;
			set
			{
				_reason = value;
				OnPropertyChanged();
			}
		}


		#region Dates

		/// <summary>
		/// Заблокировано ли для изменений время
		/// </summary>
		public bool IsDateTimeDisplayed { get; private set; } = true;

		/// <summary>
		/// Строка даты начала действия элемента заявки в корректном формате: с учетом часов, если они важны для заявки
		/// </summary>
		public string FromDateString => OrderType == OrderType.Single ? From.ToString("dd.MM.yyyy HH:mm:ss") : From.ToString("dd.MM.yyyy");

		/// <summary>
		/// Строка даты начала действия элемента заявки в корректном формате: с учетом часов, если они важны для заявки
		/// </summary>
		public string ToDateString => OrderType == OrderType.Single ? To.ToString("dd.MM.yyyy HH:mm:ss") : To.ToString("dd.MM.yyyy");


		public DateTime From
		{
			get => _from;
			set
			{
				_from = value;
				OnPropertyChanged();
			}
		}



		public DateTime FromDate
		{
			get => From.Date;
			set
			{
				From = new DateTime(value.Year, value.Month, value.Day, From.Hour, From.Minute, From.Second);
				OnPropertyChanged();
			}
		}

		public DateTime FromTime
		{
			get => From;
			set
			{
				From = new DateTime(From.Year, From.Month, From.Day, value.Hour, value.Minute, value.Second);
				OnPropertyChanged();
			}
		}

		public DateTime To
		{
			get => _to;
			set
			{
				_to = value;
				OnPropertyChanged();
			}
		}

		private DateTime _recDate = DateTime.MinValue;

		public DateTime ToDate
		{
			get => To.Date;
			set
			{
				To = new DateTime(value.Year, value.Month, value.Day, To.Hour, To.Minute, To.Second);
				OnPropertyChanged();
			}
		}

		public DateTime ToTime
		{
			get => To;
			set
			{
				To = new DateTime(To.Year, To.Month, To.Day, value.Hour, value.Minute, value.Second);
				OnPropertyChanged();
			}
		}

		public DateTime RecDate
		{
			get { return _recDate; }
			set
			{
				_recDate = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Passes

		public string TemplateIdList
		{
			get { return _templateIdList; }
			set
			{
				_templateIdList = value;

				StringBuilder st = new StringBuilder();
				var allTemplates = new ObservableCollection<Template>(
					from templates in TemplatesWrapper.CurrentTable().Table.AsEnumerable()
					where templates.Field<int>("f_template_id") != 0 &&
					      CommonHelper.NotDeleted(templates)
					select new Template
					{
						Id = templates.Field<int>("f_template_id"),
						Name = templates.Field<string>("f_template_name"),
						Descript = templates.Field<string>("f_template_description"),
					});

				for ( int i = 0; i < allTemplates.Count; i++)
				{
					if (_templateIdList.StartsWith(allTemplates[i].Id.ToString()) ||
					    _templateIdList.Contains(";" + allTemplates[i].Id))
					{
						st.Append(allTemplates[i].Name + ", ");
					}
				}

				if (st.Length == 0)
				{
					TemplateStringList = "Нет";
				}
				else
				{
					st.Length -= 2;
					TemplateStringList = st.ToString();
				}

				OnPropertyChanged(nameof(TemplateStringList));

				OnPropertyChanged();
			}
		}

		public string AreaIdList
		{
			get { return _areaIdList; }
			set
			{
				_areaIdList = value;

				StringBuilder st = new StringBuilder();
				var allAreas = new ObservableCollection<Area>(
					from areas in AreasWrapper.CurrentTable().Table.AsEnumerable()
					where areas.Field<int>("f_area_id") != 0 &&
					      CommonHelper.NotDeleted(areas)
					select new Area
					{
						Id = areas.Field<int>("f_area_id"),
						Name = areas.Field<string>("f_area_name"),
						Descript = areas.Field<string>("f_area_descript"),
						ObjectIdHi = areas.Field<int>("f_object_id_hi"),
						ObjectIdLo = areas.Field<int>("f_object_id_lo"),
					});

				for (int i = 0; i < allAreas.Count; i++)
				{
					string checkString1 = allAreas[i].ObjectIdHi + "," + allAreas[i].ObjectIdLo;
					if (_areaIdList.StartsWith(checkString1) ||
					    _areaIdList.Contains(";" + checkString1))
					{
						st.Append(allAreas[i].Name + ", ");
					}
				}

				if (st.Length == 0)
				{
					AreaStringList = "Нет";
				}
				else
				{
					st.Length -=2;
					AreaStringList = st.ToString();
				}
				OnPropertyChanged(nameof(AreaStringList));

				OnPropertyChanged();
			}
		}

		public string TemplateStringList
		{
			get;
			private set;
		}

		public string AreaStringList { get; private set; }

		private ObservableCollection<Template> _templates = new ObservableCollection<Template>();
		public ObservableCollection<Template> Templates
		{
			get => _templates;
			set
			{
				_templates = value;
				SetupPassesString();
			}
		}

		private ObservableCollection<Area> _areas = new ObservableCollection<Area>();

		public ObservableCollection<Area> Areas
		{
			get => _areas;
			set
			{
				_areas = value;
				SetupPassesString();
			}
		}

		private string _passes = OrderElementsHelper.NoPassesString;


		private void SetupPassesString()
		{
			string st = "";
			foreach (var area in Templates)
			{
				st += area.Name + ", ";
			}

			if (st.Length - 2 >= 0)
			{
				st = st.Remove(st.Length - 2);
			}

			if ((Templates == null || Templates.Count < 1) && (Areas != null && Areas.Count > 0))
			{
				st = OrderElementsHelper.OnlyZonesPassesString;
			}
			if ((Areas != null && Areas.Count > 0) && (Templates != null && Templates.Count > 0))
			{
				st += " " + OrderElementsHelper.BothPassesString;
			}
			if ((Areas == null || Areas.Count < 1) && (Templates == null || Templates.Count < 1))
			{
				st = OrderElementsHelper.NoPassesString;
			}

			Passes = st;
		}

		public string Passes
		{
			get => _passes;
			set
			{
				_passes = value;
				OnPropertyChanged();
			}
		}


		#endregion

		#region Block

		/// <summary>
		/// Показывает, что элемент не активен.
		/// </summary>
		public bool IsDisable { get; set; }

		public bool IsBlock
		{
			get => _isBlock;
			set
			{
				_isBlock = value;
				OnPropertyChanged();
			}
		}
		private string blockingNote;

		public string BlockingNote
		{
			get => blockingNote;
			set
			{
				blockingNote = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region CardState

		/// <summary>
		/// Определяет актуальное состояяние пропуска для этого элемента заявки
		/// </summary>
		public void SetupCardState()
		{
			IEnumerable<DataRow> allVisits = VisitsWrapper.CurrentTable().Table.AsEnumerable().Where(arg => 
				arg.Field<int>("f_visitor_id") == _visitorId &&
				arg.Field<int>("f_order_id") == _orderId);
			if (!allVisits.Any())
			{
				CardState = CardState.Inactive;
				return;
			}

			IEnumerable<DataRow> notDeletedVisits = allVisits.Where(arg => arg.Field<string>("f_deleted").ToUpper() != "Y").ToList();

			if (!notDeletedVisits.Any())
			{
				CardState = CardState.Returnded;
				return;
			}

			List<int> cardIdLo = new List<int>();
			List<int> cardIdHi = new List<int>();
			foreach (DataRow dataRow in notDeletedVisits)
			{
				cardIdHi.Add(dataRow.Field<int>("f_card_id_hi"));
				cardIdLo.Add(dataRow.Field<int>("f_card_id_lo"));
			}

			CardState newCardState = CardState.Inactive;

			for (int i=0; i< cardIdLo.Count; i++)
			{
				DataRow row = CardsWrapper.CurrentTable().Table.AsEnumerable().FirstOrDefault(arg =>
					arg.Field<int>("f_object_id_lo") == cardIdLo[i] &&
					arg.Field<int>("f_object_id_hi") == cardIdHi[i]);
				if (row == null)
				{
					continue;
				}

				CardState thisCardState = (CardState) row.Field<int>("f_state_id");
				if (thisCardState == CardState.Issued)
				{
					newCardState = CardState.Issued;
					break;
				}

				if (thisCardState == CardState.Lost)
				{
					if (newCardState == CardState.Inactive)
					{
						newCardState = CardState.Lost;
					}
					continue;
				}

				if (thisCardState == CardState.Active)
				{
					newCardState = CardState.Active;
				}
			}

			CardState = newCardState;
		}

		public CardState CardState
		{
			get => _cardState;
			set
			{
				_cardState = value;
				CardStateString = CardsHelper.CardStateToSting(_cardState);
				OnPropertyChanged();
			}
		}

		public string CardStateString
		{
			get => _cardStateString;
			private set
			{
				_cardStateString = value;
				OnPropertyChanged();
			}
		}

		private bool _isCardIssued;

		public bool IsCardIssued
		{
			get { return _isCardIssued; }
			set
			{
				_isCardIssued = value;
				OnPropertyChanged();
			}
		}

		#endregion

        public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        /// <summary>
        /// Название расписания
        /// </summary>
        public string Schedule
        {
            get => _schedule;
            set
            {
                _schedule = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Расписание по id. Автоматически задает свойство названия расписания.
		/// </summary>
		public int ScheduleId
        {
            get { return _scheduleId; }
            set
            {
                _scheduleId = value;
	            Schedule = GetStringFromDb(SchedulesWrapper.CurrentTable().Table, "f_schedule_id", _scheduleId,
		            "f_schedule_name");
            }
        }

		private string GetStringFromDb(DataTable table, string idString, int id, string targetString)
		{
			DataRow row = table.AsEnumerable().FirstOrDefault(arg =>
					arg.Field<int>(idString) == id);
			if (row != null)
			{
				return row[targetString].ToString();
			}
			else
			{
				MessageBox.Show("Поле ссылается на несуществующую в базе строчку по id = " + id, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return "";
			}
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}