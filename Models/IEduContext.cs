using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Data.AccountManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models
{
    public class IEduContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Book> Bookings { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<LessonStructure> LessonStructures { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<TeacherSchedule> TeacherSchedules { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<PaymentPeriods> PaymentPeriods { get; set; }
        public virtual DbSet<DistancePrices> DistancePrices { get; set; }
        public virtual DbSet<AspNetVerificationCode> AspNetVerificationCodes { get; set; }
        public virtual DbSet<AspNetTempPassword> AspNetTempPasswords { get; set; }
        public virtual DbSet<QualificationDocuments> QualificationDocuments { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<ExamTypes> ExamTypes { get; set; }
        //public virtual DbSet<LessonLocationsData> LessionLocations { get; set; }
        public virtual DbSet<TeacherDocuments> TeacherDocuments { get; set; }
        public virtual DbSet<RolePaymentsSettings> RolePaymentSettings { get; set; }
        public virtual DbSet<BankDetails> BankDetails { get; set; }
        //public virtual DbSet<Payments> Payments { get; set; }
        //public virtual DbSet<LessonRequests> LessonRequests { get; set; }
        public virtual DbSet<TeacherActiveDays> TeacherActiveDays { get; set; }
        public IEduContext(DbContextOptions<IEduContext> options) : base(options) { }
        public IEduContext() : base(GetOptions(Startup.configuration.GetConnectionString("Connection"))) { }

        private static DbContextOptions GetOptions(string connectionString) =>
            SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<QualificationDocuments>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<Level>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<Province>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<Subject>()
                .HasIndex(i => new { i.Name, i.LevelId })
                .IsUnique(true);
            builder.Entity<Title>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<Gender>()
                .HasIndex(i => i.Name)
                .IsUnique(true);
            builder.Entity<LessonStructure>()
                .HasIndex(i => new { i.LevelId, i.SubjectId, i.TeacherId })
                .IsUnique(true);
            builder.Entity<AspNetVerificationCode>()
                .HasKey(k => new { k.UserId, k.Code });
            builder.Entity<AspNetTempPassword>()
                .HasKey(k => k.UserId);
            //  builder.Entity<Address>()
            // .HasIndex(k => k.UserId)
            //.IsUnique(true);
            builder.Entity<Currencies>()
                .HasIndex(k => k.Name)
                .IsUnique(true);
            builder.Entity<PaymentPeriods>()
                .HasIndex(k => k.Name)
                .IsUnique(true);
            builder.Entity<Banks>()
                .HasIndex(k => k.Name)
                .IsUnique(true);
            builder.Entity<ExamTypes>()
                .HasIndex(k => k.Name)
                .IsUnique(true);
            //builder.Entity<LessonLocationsData>()
            //    .HasIndex(k => k.Name)
            //    .IsUnique(true);
            builder.Entity<RolePaymentsSettings>()
                .HasIndex(i => new { i.RoleId, i.PaymentPeriodId })
                .IsUnique(true);
            builder.Entity<BankDetails>()
                .HasIndex(i => new { i.UserId, i.AccountTypeId })
                .IsUnique(true);
            builder.Entity<TeacherActiveDays>()
                .HasIndex(i => new { i.UserId })
                .IsUnique(true);
            var relationships = builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            Parallel.ForEach(relationships, relationship => relationship.DeleteBehavior = DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}