using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AndoverLib
{
	[DataContract]
	public class CAreaScheduleLib
	{
		[DataMember]
		public string AreaName { get; set; }

		[DataMember]
		public string ScheduleName { get; set; }

		[DataMember]
		public int AreaIdHi { get; set; }

		[DataMember]
		public int AreaIdLo { get; set; }

		[DataMember]
		public int ScheduleId { get; set; }

		[DataMember]
		public IEnumerable<string> SchedulesFromSameCAreaSchedules { get; set; }

		[DataMember]
		public int SelectedItemIndex { get; set; }

		[DataMember]
		public object TestString { get; set; }

	}
}
