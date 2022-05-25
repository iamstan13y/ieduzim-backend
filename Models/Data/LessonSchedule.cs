using IEduZimAPI.Models.Enums;
using System;

namespace IEduZimAPI.Models.Data
{
    public class LessonSchedule
    {
        public int Id { get; set; }
        public int LessonStructureId { get; set; }
        public Day LessonDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; } = true;
    }
}