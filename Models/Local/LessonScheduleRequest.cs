using IEduZimAPI.Models.Enums;
using System;

namespace IEduZimAPI.Models.Local
{
    public class LessonScheduleRequest
    {
        public int UserId { get; set; }
        public Day LessonDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}