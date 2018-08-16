using System.Runtime.Serialization;

namespace AndoverLib
{
	[DataContract]
	public class CAndoverAgentCallback
	{
		[DataMember]
		public bool? Success { get; set; }

		[DataMember]
		public bool? IsExtradition { get; set; }

		[DataMember]
		public bool? ExtraditionSuccess { get; set; }
	}
}
