using System;

namespace IEduZimAPI.Models.Data
{
    public class HybridLessonSchedule
    {
        public int Id { get; set; }
        public int LessonDay { get; set; }
        public int SubjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}