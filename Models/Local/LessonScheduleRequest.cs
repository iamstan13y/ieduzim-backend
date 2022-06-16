using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LessonScheduleRequest
    {
        public int StudentId { get; set; }
        public int LessonStructureId { get; set; }
        public List<int> LessonDays { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}