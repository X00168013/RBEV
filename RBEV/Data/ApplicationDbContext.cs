using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RBEV.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RBEV.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-RBEV-AA7EBA5F-0654-43BC-A681-CA582EFA1468;Trusted_Connection=True;MultipleActiveResultSets=true";
        //private const string connectionString = "Server=tcp:rbevdbserver.database.windows.net,1433;Initial Catalog=RBEV_db;Persist Security Info=False;User ID=adminserver;Password=Majella1*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<EventCoordinator> EventCoordinators { get; set; }
        public DbSet<EventAssignment> EventAssignments { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Event>().ToTable("Event");
            builder.Entity<Registration>().ToTable("Registration");
            builder.Entity<Member>().ToTable("Member");
            builder.Entity<Club>().ToTable("Club");
            builder.Entity<EventCoordinator>().ToTable("EventCoordinator");
            builder.Entity<EventAssignment>().ToTable("EventAssignment");
            builder.Entity<Account>().ToTable("Account");
            builder.Entity<EventLocation>().ToTable("EventLocation");

            builder.Entity<EventAssignment>()
                .HasKey(c => new { c.EventID, c.EventCoordinatorID });
        }
    }
}
