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

namespace SupRealClient.EnumerationClasses
{
	/// <summary>
	/// Описание посетителя по заявке.
	/// </summary>
	public class OrderElement : IdEntity, INotifyPropertyChanged, ICloneable
	{
		private int orderId;
		private int visitorId;
		private int organizationId;
		private int catcherId;
        private int scheduleId;

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
		/// Задать границы времени для OrderElement 
		/// </summary>
		/// <param name="isTimeEditable">Доступно ли пользователю редактирование времени</param>
		/// <param name="from">Начало действия</param>
		/// <param name="to">Конец действия</param>
		public void SetAndBlockDates(bool isTimeEditable, DateTime from, DateTime to)
		{

		}

		/// <summary>
		/// Уникальный номер заявки
		/// </summary>
		public int OrderId
		{
			get => orderId;
			set
			{
				orderId = value;
				if (orderId == 0)
				{
					return;
				}
				DataRow row = OrdersWrapper.CurrentTable().Table.Rows.Find(orderId);
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
			get => visitorId;
			set
			{
				visitorId = value;
				DataRow row = VisitorsWrapper.CurrentTable().Table
					.AsEnumerable().FirstOrDefault(arg =>
						arg.Field<int>("f_visitor_id") == visitorId);
				if (row != null)
				{
					Visitor = row["f_full_name"].ToString();
					VisitorMainPosition = row["f_job"].ToString();
					SetupCardState();
				}
				else
				{
					MessageBox.Show("Поле посетителя ссылается на несуществующего в базе посетителя по id = " + visitorId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private string visitor = "";

		/// <summary>
		/// Полное имя посетителя
		/// </summary>
		public string Visitor
		{
			get => visitor;
			private set
			{
				visitor = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Организация по id. Автоматически задает свойство названия организации
		/// </summary>
		public int? OrganizationId
		{
			get => organizationId;
			set
			{
				organizationId = value ?? 0;
				Organization = OrganizationsHelper.GenerateFullName(organizationId, true);
				//DataRow row = OrganizationsWrapper.CurrentTable().Table
				//	.AsEnumerable().FirstOrDefault(arg =>
				//		arg.Field<int>("f_org_id") == organizationId);
				//if (row != null)
				//{
				//	Organization = row["f_org_name"]?.ToString();
				//}
				//else
				//{
				//	MessageBox.Show("Поле организации ссылается на несуществующую в базе организацию по id = " + organizationId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				//	Organization = "";
				//}
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

		private string _position = "";

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
			get => catcherId;
			set
			{
				catcherId = value;
				DataRow row = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
					.FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
					                       catcherId);
				if (row != null)
				{
					Catcher = row["f_full_name"]?.ToString();
				}
				else
				{
					MessageBox.Show("Поле принимающего лица ссылается на несуществующего в базе посетителя по id = " + catcherId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					Catcher = "";
				}
				
			}
		}

		/// <summary>
		/// Полное имя сопровождающего
		/// </summary>
		public string Catcher { get; private set; } = "";


		private string _reason;

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

		public string FromDateString => OrderType == OrderType.Single ? From.ToString("dd.MM.yyyy HH:mm:ss") : From.ToString("dd.MM.yyyy");

		public string ToDateString => OrderType == OrderType.Single ? To.ToString("dd.MM.yyyy HH:mm:ss") : To.ToString("dd.MM.yyyy");

		private DateTime from = DateTime.MinValue;

		public DateTime From
		{
			get => from;
			set
			{
				from = value;
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


		private DateTime to = DateTime.MinValue;

		public DateTime To
		{
			get => to;
			set
			{
				to = value;
				OnPropertyChanged();
			}
		}

		private DateTime recDate = DateTime.MinValue;

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
			get { return recDate; }
			set
			{
				recDate = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Passes

		private string templateIdList = "";
		public string TemplateIdList
		{
			get { return templateIdList; }
			set
			{
				templateIdList = value;

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
					if (templateIdList.StartsWith(allTemplates[i].Id.ToString()) ||
					    templateIdList.Contains(";" + allTemplates[i].Id))
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

		private string areaIdList = "";
		public string AreaIdList
		{
			get { return areaIdList; }
			set
			{
				areaIdList = value;

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
					if (areaIdList.StartsWith(checkString1) ||
					    areaIdList.Contains(";" + checkString1))
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

		private string _passes = NoPassesString;
		public const string OnlyZonesPassesString = "Назначены зоны доступа";
		public const string BothPassesString = "+ зоны доступа";
		public const string NoPassesString = "Доступ не назначен";

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
				st = OnlyZonesPassesString;
			}
			if ((Areas != null && Areas.Count > 0) && (Templates != null && Templates.Count > 0))
			{
				st += " " + BothPassesString;
			}
			if ((Areas == null || Areas.Count < 1) && (Templates == null || Templates.Count < 1))
			{
				st = NoPassesString;
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

		private bool _isBlock;

		public bool IsBlock
		{
			get { return _isBlock; }
			set
			{
				_isBlock = value;
				OnPropertyChanged();
			}
		}
		private string blockingNote;

		public string BlockingNote
		{
			get { return blockingNote; }
			set
			{
				blockingNote = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region CardState

		private CardState _cardState = CardState.Inactive;
		private string _cardStateString;

		public void SetupCardState()
		{
			IEnumerable<DataRow> allVisits = VisitsWrapper.CurrentTable().Table.AsEnumerable().Where(arg => 
				arg.Field<int>("f_visitor_id") == visitorId &&
				arg.Field<int>("f_order_id") == orderId);
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
				CardStateString = CommonHelper.CardStateToSting(_cardState);
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

		public object Clone()
		{
			return this.MemberwiseClone();
		}


		private string schedule = "";

        /// <summary>
        /// Название расписания
        /// </summary>
        public string Schedule
        {
            get => schedule;
            set
            {
                schedule = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Расписание по id. Автоматически задает свойство названия расписания.
		/// </summary>
		public int ScheduleId
        {
            get { return scheduleId; }
            set
            {
                scheduleId = value;
                DataRow row = SchedulesWrapper.CurrentTable().Table
                    .AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_schedule_id") == scheduleId);
	            if (row != null)
	            {
		            Schedule = row["f_schedule_name"].ToString();
				}
	            else
	            {
					MessageBox.Show("Поле расписания ссылается на несуществующее в базе расписание по id = " + scheduleId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		            Catcher = "";
				}
            }
        }

		/// <summary>
		/// Проверяет корректность и полноту данных элемента заявки
		/// </summary>
		/// <param name="errorMessage">Если данные некорректные, содержит в себе строку ошибки</param>
		/// <param name="isVirtueOrder"></param>
		/// <returns></returns>
        public bool IsOrderElementDataCorrect(out string errorMessage, bool isVirtueOrder = false)
        {
            if (VisitorId == 0)
            {
                errorMessage = "Не выбран посетитель.";
                return false;
            }

            if (OrganizationId == 0)
            {
                errorMessage = "Не выбрана организация.";
                return false;
            }

            if (string.IsNullOrEmpty(Position))
            {
                errorMessage = "Не указана должность.";
                return false;
            }

	        if (!isVirtueOrder && string.IsNullOrEmpty(Catcher))
	        {
				errorMessage = "Не указано принимающее лицо.";
		        return false;
			}

			if (string.IsNullOrEmpty(Passes) || Passes == NoPassesString)
			{
				errorMessage = "Необходимо назначить доступ. Поле \"Проходы\".";
				return false;
			}

			if (From > To)
            {
                errorMessage = " \"Время от\" не может быть позже, чем \"Время до\"";
                return false;
            }

            if (!CommonHelper.IsPositionCorrect(Position))
            {
                errorMessage = "Неверно введена должность.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}