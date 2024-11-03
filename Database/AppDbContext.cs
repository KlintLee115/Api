using Api.Model;
using Api.Model.Courses;
using Api.Model.People;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; init; }
        public DbSet<CourseSchedule> CourseSchedules { get; init; }
        public DbSet<Family> Families { get; init; }
        public DbSet<Admin> Admins { get; init; }
        public DbSet<Coach> Coaches { get; init; }
        public DbSet<Customer> Customers { get; init; }
        public DbSet<Account> IndividualAccounts { get; init; }
        public DbSet<AthleteInfo> AthleteInfo { get; init; }
        public DbSet<Facility> Facilities { get; init; }
        public DbSet<FinancialInfo> FinancialInfo { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const bool shouldUseLocalHostDb = true;

            if (optionsBuilder.IsConfigured) return;
            var dbConnectionString = Environment.GetEnvironmentVariable(
                $"DB_URL_FOR_SERVER_ON_{(shouldUseLocalHostDb ? "LOCAL" : "DOCKER")}_NETWORK");
            optionsBuilder.UseNpgsql(dbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");

                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.Property(f => f.Id).ValueGeneratedOnAdd();

                entity.HasMany(f => f.Members)
                    .WithOne(m => m.Family)
                    .HasForeignKey(m => m.FamilyId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<CourseSchedule>(entity =>
           {
               entity.ToTable("CourseSchedules");
               entity.HasKey(cs =>
               new
               {
                   cs.CourseId,
                   cs.Location,
                   cs.BeginTime,
               });
           });

            modelBuilder.Entity<Account>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Admin>().ToTable("Admins");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(c => c.Coach)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CoachId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Facility>().Property(f => f.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AthleteInfo>(entity =>
            {
                entity.ToTable("CustomerAthleteInfo");

                entity.HasOne(ai => ai.Customer)
            .WithOne(c => c.AthleteInfo)
            .HasForeignKey<AthleteInfo>(ai => ai.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Coach>().ToTable("Coaches");

            modelBuilder.Entity<Customer>()
            .HasMany(c => c.FinancialInfos)
            .WithMany(fi => fi.Customers)
            .UsingEntity(j => j.ToTable("CustomerFinancialInfo"));
        }
    }
}