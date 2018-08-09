using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AndoverLib
{
	[DataContract]
	public class PersonInfo
	{
		[DataMember] public string UiName { get; set; }

		[DataMember] public string Path { get; set; }

		[DataMember] public string Alias { get; set; }

		[DataMember] public string FirstName { get; set; }

		[DataMember] public string LastName { get; set; }

		[DataMember] public string CardNum { get; set; }

		[DataMember] public List<string> Containers { get; set; }

		[DataMember] public List<string> Areas { get; set; }

		[DataMember] public List<string> Schedules { get; set; }

		[DataMember] public bool CreateFolder { get; set; }

		[DataMember] public List<CAreaScheduleLib> AreaScheduleList { get; set; }

	}
}
