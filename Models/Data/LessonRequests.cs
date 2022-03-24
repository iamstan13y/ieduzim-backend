namespace IEduZimAPI.Models.Data
{
    public class LessonRequests: Local.LessonRequest
    {
        public int Id { get; set; }
        public virtual PaymentPeriods PaymentPeriod { get; set; }
        public virtual ExamTypes ExamType { get; set; }
        public virtual LessonLocationsData LessonLocation { get; set; }
    }
}
