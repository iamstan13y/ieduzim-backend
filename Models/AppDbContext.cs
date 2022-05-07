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
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonLocation> LessonLocations { get; set; }

    }
}