namespace IEduZimAPI.Models.Data
{
    public class Subject : Local.Subjects
    {
        public int Id { get; set; }
        public virtual Currencies Currency { get; set; }
        public virtual Level Level { get; set; }
    }
}
