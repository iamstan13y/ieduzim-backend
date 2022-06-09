using System;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LessonScheduleRequest
    {
        public int StudentId { get; set; }
        //public int SubjectId { get; set; }
        public int LessonStructureId { get; set; }
        public List<int> LessonDays { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}