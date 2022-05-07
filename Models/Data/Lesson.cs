using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Lesson
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmed { get; set; } = false;
        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }
    }
}