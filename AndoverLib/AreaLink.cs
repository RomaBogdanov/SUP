using System;
using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class AreaLink
    {
        [DataMember]
        public int ObjectIdHi { get; set; }

        [DataMember]
        public int ObjectIdLo { get; set; }

        [DataMember]
        public int AreaIdHi { get; set; }

        [DataMember]
        public int AreaIdLo { get; set; }

        [DataMember]
        public int PersonIdHi { get; set; }

        [DataMember]
        public int PersonIdLo { get; set; }

        [DataMember]
        public bool? Preload { get; set; }

        [DataMember]
        public int? SchedIdHi { get; set; }

        [DataMember]
        public int? SchedIdLo { get; set; }

        [DataMember]
        public short? State { get; set; }

        [DataMember]
        public DateTime? TimeEntered { get; set; }

        [DataMember]
        public bool? DistPending { get; set; }

        [DataMember]
        public bool? DeletePending { get; set; }

        [DataMember]
        public DateTime? DistTime { get; set; }

        [DataMember]
        public byte? TemplateFlag { get; set; }

        [DataMember]
        public byte? ClearanceLevel { get; set; }
    }
}
