using System;

namespace IEduZimAPI.Models.Data
{
    public class TeacherSchedule
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Id { get; set; }
        public int Duration { get; set; }
        public int StructureId { get; set; }
        public bool Available { get; set; }
        public LessonStructure Structure { get; set; }
    }
}
