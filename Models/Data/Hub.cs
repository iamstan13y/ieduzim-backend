using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class Hub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
    }
}