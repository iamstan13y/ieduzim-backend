namespace IEduZimAPI.Models.Data
{
    public class Location
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string? Area { get; set; }
        public double Distance { get; set; }
    }
}