using IEduZimAPI.Models.Enums;

namespace IEduZimAPI.Models.Local
{
    public class PaynowPaymentRequest
    {
        public double Amount { get; set; }
        public string Reference { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}