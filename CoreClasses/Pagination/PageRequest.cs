namespace IEduZimAPI.CoreClasses
{
    public class PageRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public bool Desc { get; set; }
        public bool Active { get; set; }
        public string AdditionalData{ get; set; }
    }
}