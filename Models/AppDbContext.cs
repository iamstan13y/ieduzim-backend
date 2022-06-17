using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
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
        public virtual DbSet<LessonStructure> LessonStructures { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonLocation> LessonLocations { get; set; }
        public DbSet<LessonSchedule> LessonSchedules { get; set; }
        public DbSet<HybridLessonSchedule> HybridLessonSchedules { get; set; }
        public DbSet<LocalAddress> Addresses { get; set; }
        public DbSet<LessonDay> LessonDays { get; set; }
    }
}