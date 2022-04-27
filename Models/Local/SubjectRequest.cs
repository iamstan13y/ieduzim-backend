namespace IEduZimAPI.Models.Local
{
    public class SubjectRequest
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public int CurrencyId { get; set; }
        public int LevelId { get; set; }
        public int TeacherId { get; set; }
    }
}