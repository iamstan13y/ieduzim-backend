using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LocalLessonStructure
    {
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public int LevelId { get; set; }
        public bool Active { get; set; }
        public string LessonLocationId { get; set; }
        public string ExamTypeId { get; set; }
    }
}
