using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace IEduZimAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}