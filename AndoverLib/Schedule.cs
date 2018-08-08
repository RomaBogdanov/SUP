using System;
using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Schedule
    {
        [DataMember]
        public int ObjectIdHi { get; set; }

        [DataMember]
        public int ObjectIdLo { get; set; }

        [DataMember]
        public string UiName { get; set; }

        [DataMember]
        public int? OwnerIdHi { get; set; }

        [DataMember]
        public int? OwnerIdLo { get; set; }

        [DataMember]
        public int? DeviceIdHi { get; set; }

        [DataMember]
        public int? DeviceIdLo { get; set; }

        [DataMember]
        public bool? TemplateFlag { get; set; }

        [DataMember]
        public int? TemplateIdHi { get; set; }

        [DataMember]
        public int? TemplateIdLo { get; set; }

        [DataMember]
        public string ControllerName { get; set; }

        [DataMember]
        public short? AlarmGraphicPage { get; set; }

        [DataMember]
        public byte[] Value { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte[] EffectivePeriod { get; set; }

        [DataMember]
        public byte[] WeeklySchedule { get; set; }

        [DataMember]
        public byte[] ExceptionSched { get; set; }

        [DataMember]
        public byte[] PropertyRef { get; set; }

        [DataMember]
        public byte? Priority { get; set; }

        [DataMember]
        public DateTime? OccupancyTime { get; set; }

        [DataMember]
        public DateTime? UnoccupancyTime { get; set; }

        [DataMember]
        public string ActiveText { get; set; }

        [DataMember]
        public string InactiveText { get; set; }

        [DataMember]
        public short? State { get; set; }

        [DataMember]
        public byte[] CalenderRefList { get; set; }

        [DataMember]
        public byte[] WeeklySchNotes { get; set; }

        [DataMember]
        public byte[] ExcSchedNotes { get; set; }

        [DataMember]
        public byte[] ActiveValue { get; set; }

        [DataMember]
        public byte[] InactiveValue { get; set; }

        [DataMember]
        public byte[] PackagedDays { get; set; }

        [DataMember]
        public byte? DownloadFlag { get; set; }

        [DataMember]
        public DateTime? LastDownloadTime { get; set; }

        [DataMember]
        public byte? ScheduleType { get; set; }

        [DataMember]
        public int? OccTimePointHi { get; set; }

        [DataMember]
        public int? OccTimePointLo { get; set; }

        [DataMember]
        public int? UnOccTimePointHi { get; set; }

        [DataMember]
        public int? UnOccTimePointLo { get; set; }

        [DataMember]
        public byte? AutosendFlag { get; set; }

        [DataMember]
        public DateTime? AutosendTime { get; set; }

        [DataMember]
        public string UnavailableAttributes { get; set; }

        [DataMember]
        public byte[] SpecialEventName { get; set; }

        [DataMember]
        public byte? TimeScale { get; set; }

        [DataMember]
        public byte[] ScheduleDefault { get; set; }

        [DataMember]
        public short? DefaultDataType { get; set; }

        [DataMember]
        public bool? OutOfService { get; set; }

        [DataMember]
        public bool? ApplyMidnightValue { get; set; }

        [DataMember]
        public byte[] DefaultMidnightValue { get; set; }

        [DataMember]
        public bool? ClearPastEvents { get; set; }

	[DataMember]
	public string Path { get; set; }
	}
}
