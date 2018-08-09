namespace SupRealClient.EnumerationClasses
{
    public class Template : EntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; } = "1";
        public string Descript { get; set; } = "";
        public string AreaIdList { get; set; } = "";
        public int ScheduleIdHi { get; set; } = 0;
        public int ScheduleIdLo { get; set; } = 0;
        public string Schedule { get; set; } = "";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
