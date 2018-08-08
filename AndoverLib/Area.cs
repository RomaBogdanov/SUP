using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Area
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
        public byte? DeletePending { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? KnownOccupCount { get; set; }

        [DataMember]
        public short? State { get; set; }

        [DataMember]
        public byte? ForceLock { get; set; }

	[DataMember]
	public string Path { get; set; }
	}
}
