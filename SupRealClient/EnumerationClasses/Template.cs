namespace SupRealClient.EnumerationClasses
{
    public class Template : EntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; } = "1";
        public string Descript { get; set; } = "";
        public string AreaIdList { get; set; } = "";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
