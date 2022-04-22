using IEduZimAPI.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Reference { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AccountNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PollUrl { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}