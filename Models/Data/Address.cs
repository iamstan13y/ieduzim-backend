namespace IEduZimAPI.Models.Data
{
    public class Address : Local.LocalAddress
    {
        public int Id { get; set; }
        public virtual City City { get; set; }
        public virtual Province Province { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
