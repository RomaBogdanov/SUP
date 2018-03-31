namespace SupRealClient.Common.Data
{
    public class SearchData
    {
        public string Field { get; set; } = "";
        public string Text { get; set; } = "";
        public bool Register { get; set; }
        public bool Equal { get; set; }
        public bool StartWith { get; set; }
        public bool Contains { get; set; }
    }
}
