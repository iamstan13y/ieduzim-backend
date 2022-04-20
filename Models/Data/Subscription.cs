using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Subscription
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string StudentId { get; set; }
        public int HoursRemaining { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}