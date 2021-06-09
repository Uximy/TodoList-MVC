using System;
using System.Collections.Immutable;
using ITStep.Planner.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITStep.Planner.Contexts
{
    public class PlannerContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public PlannerContext()
        {
            
        }
        
        public PlannerContext(DbContextOptions<PlannerContext> options)
            : base(options)
        {
            
        }
        
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<JobStatus> Statuses { get; set; }
        public virtual DbSet<JobType> Types { get; set; }
        public virtual DbSet<WorkLog> WorkLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasOne(x => x.Author)
                .WithMany(x => x.OwnedProjects)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Project>()
                .HasMany(x => x.Employees)
                .WithMany(x => x.ParticipateProjects);
            builder.Entity<Project>()
                .HasMany(x => x.Jobs)
                .WithOne(x => x.Project)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Job>()
                .HasOne(x => x.Author)
                .WithMany(x => x.OwnedJobs)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Job>()
                .HasMany(x => x.Employees)
                .WithMany(x => x.ParticipateJobs);
            builder.Entity<Job>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Job)
                .OnDelete(DeleteBehavior.NoAction);
            
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=tcp:itstep-database-server.database.windows.net,1433;" +
                                     "Initial Catalog=itstep-planner;Persist Security Info=False;" +
                                     "User ID=itstep-admin;Password=!CthdthysqCthdth7;" +
                                     "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
                                     "Connection Timeout=30;");
            }
            
            base.OnConfiguring(builder);
        }
    }
}