using System;

namespace IEduZimAPI.Models.Local
{
    public class TeacherLessonRequest
    {
        public int LessonStructureId { get; set; }
        public DateTime LessonDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}