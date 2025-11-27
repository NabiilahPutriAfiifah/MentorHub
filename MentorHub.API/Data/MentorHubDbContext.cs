using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Data
{
    public class MentorHubDbContext : DbContext
    {
        public MentorHubDbContext(DbContextOptions<MentorHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<LearningGoals> LearningGoals { get; set; }
        public DbSet<MenteeGoals> MenteeGoals { get; set; }
        public DbSet<MentorSkills> MentorSkills { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Skills> Skills { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MentorHubDbContext).Assembly);

            // ------------------------
            // Accounts & Roles (One-to-One)
            // ------------------------
            modelBuilder.Entity<Accounts>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // Accounts & Employees (One-to-One)
            // ------------------------
            modelBuilder.Entity<Accounts>()
                        .HasOne(a => a.Employee)
                        .WithOne(e => e.Account)
                        .HasForeignKey<Employees>(e => e.Id)
                        .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // Employees & Mentor (Self-referencing One-to-Many)
            // ------------------------
            modelBuilder.Entity<Employees>()
                        .HasOne(e => e.Mentor)
                        .WithMany(m => m.Employee)
                        .HasForeignKey(e => e.MentorId)
                        .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // MentorSkills & Accounts / Skills (Many-to-One)
            // ------------------------
            modelBuilder.Entity<MentorSkills>()
                        .HasOne<Accounts>()
                        .WithMany(a => a.MentorSkills)
                        .HasForeignKey(ms => ms.MentorId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentorSkills>()
                        .HasOne<Skills>()
                        .WithMany(s => s.MentorSkills)
                        .HasForeignKey(ms => ms.SkillId)
                        .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // MenteeGoals & Accounts / learningGoals (Many-to-One)
            // ------------------------
            modelBuilder.Entity<MenteeGoals>()
                        .HasOne<Accounts>()
                        .WithMany(a => a.MenteeGoals)
                        .HasForeignKey(mg => mg.MenteeId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenteeGoals>()
                        .HasOne<LearningGoals>()
                        .WithMany(a => a.MenteeGoals)
                        .HasForeignKey(mg => mg.LearningId)
                        .OnDelete(DeleteBehavior.Restrict);
            
            // modelBuilder.Entity<MenteeGoals>()
            //     .HasOne(mg => mg.LearningGoals)
            //     .WithMany(lg => lg.MenteeGoals)
            //     .HasForeignKey(mg => mg.LearningId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // SEED ACCOUNTS, SKILLS, LEARNINGGOALS
            // ------------------------
            modelBuilder.Entity<Roles>().HasData(MentorHubDataSeeder.GetDefaultRoles());
            modelBuilder.Entity<Skills>().HasData(MentorHubDataSeeder.GetDefaultSkills());
            modelBuilder.Entity<LearningGoals>().HasData(MentorHubDataSeeder.GetDefaultLearningGoals());

        }
    }
}
