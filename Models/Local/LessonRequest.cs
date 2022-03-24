using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Local
{
    public class LessonRequest
    {
        public int PaymentPeriodId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject{ get; set; }
        public string Teacher { get; set; }
        public int TeacherId { get; set; }
        [NotMapped]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public string Email { get; set; }
        public int PaymentId { get; set; }
        public bool Paid { get; set; }
        public bool Accepted { get; set; }
        public double Amount { get; set; }
        public bool Administered { get; set; }
        public int ExamTypeId { get; set; }
        public int LessonLocationId { get; set; }
        [NotMapped]
        public string Title { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string Payer { get; set; }
        [NotMapped]
        public string PaymentMethod { get; set; }
        [NotMapped]
        public string AccountNumber { get; set; }
        [NotMapped]
        public string Status { get; set; }
        [NotMapped]
        public string PollUrl { get; set; }
        [NotMapped]
        public string Currency { get; set; }
        [NotMapped]
        public string Description { get; set; }
    }
}
