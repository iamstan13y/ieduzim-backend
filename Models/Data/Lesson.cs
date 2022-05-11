using IEduZimAPI.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Lesson
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime LessonDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public LessonStatus LessonStatus { get; set; } = LessonStatus.Pending;
        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }
    }
}