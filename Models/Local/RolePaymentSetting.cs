namespace IEduZimAPI.Models.Local
{
    public class RolePaymentSetting
    {
        public string RoleId { get; set; }
        public int PaymentPeriodId { get; set; }
        public string PaymentDescription { get; set; }
        public double TotalAmount { get; set; }
        public string Currency { get; set; }
        public bool Active { get; set; }
    }
}
