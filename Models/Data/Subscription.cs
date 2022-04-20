using System;

namespace IEduZimAPI.Models.Data
{
    public class Subscription
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int HoursRemaining { get; set; }
        public DateTime DateCreated { get; set; }
    }
}