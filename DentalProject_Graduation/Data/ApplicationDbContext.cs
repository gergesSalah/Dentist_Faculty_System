using DentalProject_Graduation.Data.Entities;
using DentalProject_Graduation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DentalProject_Graduation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        //Tables
         public DbSet<Patient> Patients { get; set; }
        public DbSet<DentalStudent> DentalStudents { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<CaseInformation> CaseInformation { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Alarm> Alarms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            //Composite Primary key of CaseInformation Table
            //builder.Entity<CaseInformation>().HasKey(e => new { e.CaseId, e.DentalStudentId });
            //Foreign key between Caseinformation and Dental student

         

            builder.Entity<CaseInformation>()
         .HasOne(c => c.DentalStudent)
         .WithMany(s => s.CaseInformations)
         .HasForeignKey(c => c.DentalStudentId)
         .OnDelete(DeleteBehavior.Restrict);

            // table alarm make compoiste key

            builder.Entity<Alarm>()
      .HasKey(p => new { p.IdDentail, p.IdDiseaase });


        }

    }
    }
