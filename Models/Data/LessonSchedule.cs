using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class LessonSchedule
    {
        public int Id { get; set; }
        public int LessonStructureId { get; set; }
        public int LessonDay { get; set; }
        public int StudentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SubscriptionId { get; set; } = default;
        public bool Status { get; set; } = false;
        [ForeignKey("LessonStructureId")]
        public LessonStructure LessonStructure { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}