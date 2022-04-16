namespace IEduZimAPI.Models.Local
{
    public class LocationRequest
    {
        public int CityId { get; set; }
        public string? Area { get; set; }
        public double Distance { get; set; }
    }
}