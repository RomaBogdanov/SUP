using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Container
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
        public string ContainerType { get; set; }

        [DataMember]
        public int? DefaultControllerIdHi { get; set; }

        [DataMember]
        public int? DefaultControllerIdLo { get; set; }

        [DataMember]
        public int? ContainerCreateRuleIdHi { get; set; }

        [DataMember]
        public int? ContainerCreateRuleIdLo { get; set; }

        [DataMember]
        public int? VideoLayoutIdHi { get; set; }

        [DataMember]
        public int? VideoLayoutIdLo { get; set; }

        [DataMember]
        public string InfCameraPoint1 { get; set; }

        [DataMember]
        public string InfCameraPoint2 { get; set; }

        [DataMember]
        public string InfCameraPoint3 { get; set; }

        [DataMember]
        public string InfCameraPoint4 { get; set; }

        [DataMember]
        public string InfCameraPoint5 { get; set; }

        [DataMember]
        public string InfCameraPoint6 { get; set; }

        [DataMember]
        public string InfCameraPoint7 { get; set; }

        [DataMember]
        public string InfCameraPoint8 { get; set; }

        [DataMember]
        public string InfCameraPoint9 { get; set; }

        [DataMember]
        public string InfCameraPoint10 { get; set; }

        [DataMember]
        public string InfCameraPoint11 { get; set; }

        [DataMember]
        public string InfCameraPoint12 { get; set; }

        [DataMember]
        public string InfCameraPoint13 { get; set; }

        [DataMember]
        public string InfCameraPoint14 { get; set; }

        [DataMember]
        public string InfCameraPoint15 { get; set; }

        [DataMember]
        public string InfCameraPoint16 { get; set; }
    }
}
