using IEduZimAPI.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Local
{
    public class Subjects
    {
        public string Name { get; set; }
        public string Price { get; set; }
        [NotMapped]
        public double ZwlPrice { get; set; }
        public int CurrencyId { get; set; }
        public int LevelId { get; set; }
        public int TeacherId { get; set; }
        public int LessonLocationId { get; set; }
        public bool Active { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        [ForeignKey("LessonLocationId")]
        public LessonLocation LessonLocation { get; set; }
    }
}