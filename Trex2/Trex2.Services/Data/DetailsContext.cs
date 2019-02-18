using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Trex2.Common.Models;
using Trex2.Services.Contracts;

namespace Trex2.Services.Data
{
    public class DetailsContext : DbContext
    {
        public DbSet<Person> PersonDetails { get; set; }
        public DetailsContext(DbContextOptions<DetailsContext> options) : base(options)
        {
        }

        public DetailsContext()
        {
            SeedData();
            //By default it will not track
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PersonDetails;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
        }

        private void SeedData()
        {
            Database.EnsureCreated();
            if (PersonDetails.Any()) return;
            PersonDetails.AddRange(new List<Person>()
                {
                    new Person(){FirstName = "Eldad", LastName = "Micaheli", Email = ""}
                });
            SaveChanges();
        }
    }
}