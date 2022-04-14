using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Data
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public double Rate { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currencies Currencies { get; set; }
    }
}