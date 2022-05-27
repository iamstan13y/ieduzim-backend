using IEduZimAPI.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Local
{
    public class LocalLessonStructure
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int LevelId { get; set; }
        public bool Active { get; set; }
        public int LessonLocationId { get; set; }
        public string ExamTypeId { get; set; }
        [ForeignKey("LessonLocationId")]
        public LessonLocation LessonLocation { get; set; }
    }
}
