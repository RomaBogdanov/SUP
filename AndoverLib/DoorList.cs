using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class DoorList
    {
        [DataMember]
        public int ObjectIdHi { get; set; }

        [DataMember]
        public int ObjectIdLo { get; set; }

        [DataMember]
        public int DoorIdHi { get; set; }

        [DataMember]
        public int DoorIdLo { get; set; }

        [DataMember]
        public int AreaIdHi { get; set; }

        [DataMember]
        public int AreaIdLo { get; set; }

        [DataMember]
        public int? DeviceIdHi { get; set; }

        [DataMember]
        public int? DeviceIdLo { get; set; }

        [DataMember]
        public int? NetworkIdHi { get; set; }

        [DataMember]
        public int? NetworkIdLo { get; set; }
    }
}
