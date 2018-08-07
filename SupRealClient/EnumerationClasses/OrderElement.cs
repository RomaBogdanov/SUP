using System;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;

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
			get { return orderId; }
			set
			{
				orderId = value;
				// todo: пока неактуальный код, но без необходимости не удалять.
				//		создавался для использования из под формы посетители, но пока вроде
				//		функционал реализуется средствами формы.
				/*DataRow row = OrdersWrapper.CurrentTable().Table.Rows.Find(orderId);
				string type = SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
				    .FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
				    (int)row["f_order_type_id"])["f_order_text"].ToString();
				int number = row.Field<int>("f_reg_number");
				type = type.Length > 0 ? type : " ";
				Order = number.ToString() + type[0];*/
			}
		}

		public string Order { get; set; }

		/// <summary>
		/// Посетитель по id. Автоматически задает свойство полного имени посетителя.
		/// </summary>
		public int VisitorId
		{
			get { return visitorId; }
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
				DataRow row = OrganizationsWrapper.CurrentTable().Table
					.AsEnumerable().FirstOrDefault(arg =>
						arg.Field<int>("f_org_id") == organizationId);
				if (row != null)
				{
					Organization = row["f_org_name"]?.ToString();
				}
				else
				{
					Organization = "";
				}
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
			get { return catcherId; }
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
					Catcher = "";
				}
				
			}
		}

		/// <summary>
		/// Полное имя сопровождающего
		/// </summary>
		public string Catcher { get; private set; } = "";

		/// <summary>
		/// Заблокировано ли для изменений время
		/// </summary>
		public bool IsDateTimeDisplayed { get; private set; } = true;

		private DateTime from = DateTime.MinValue;

		public DateTime From
		{
			get { return from; }
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

		private string _passes = NoPassesString;
		public const string OnlyZonesPassesString= "Назначены зоны доступа";
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
			private set
			{
				_passes = value;
				OnPropertyChanged();
			}
		}

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

        private string _reason;

        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
                OnPropertyChanged();
            }
        }

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
                Schedule = row["f_schedule_name"].ToString();
            }
        }

        public string TemplateIdList { get; set; } = "";

        public string AreaIdList { get; set; } = "";

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

			if (string.IsNullOrEmpty(Passes))
			{
				errorMessage = "Необходимо выбрать хотя бы один проход.";
				return false;
			}

			if (From.TimeOfDay > To.TimeOfDay)
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