using IEduZimAPI.Models.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Subscription
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        public bool Active { get; set; } = false;
        [NotMapped]
        public List<int> LessonScheduleIds { get; set; }
        [NotMapped]
        public List<LessonSchedule> LessonSchedules { get; set; }
    }
}