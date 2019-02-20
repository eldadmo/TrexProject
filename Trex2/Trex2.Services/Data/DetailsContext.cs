using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using Trex2.Common.Models;
using Trex2.Services.Contracts;

namespace Trex2.Services.Data
{
    public class DetailsContext : DbContext
    {
        private string m_createCommand;
        private string m_tableName;
        public DbSet<Person> PersonDetails { get; set; }


        public DetailsContext() : base(new SQLiteConnection()
        {
            ConnectionString =
                new SQLiteConnectionStringBuilder()
                        { DataSource = ConfigurationManager.AppSettings["DbPath"] }
                    .ConnectionString
        }, true)
        {
            m_tableName = ConfigurationManager.AppSettings["TableName"];
            m_createCommand = "'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"+
            "'FirstName' TEXT NOT NULL,"+
            "'LastName' TEXT NOT NULL,"+
            "'Email' TEXT,"+
            "'Comment' TEXT";
            SeedData();
            //By default it will not track
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().ToTable(m_tableName);
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData()
        {          
            var columns = m_createCommand;
            CreateTableOperation(m_tableName, columns);
            if (PersonDetails.Any()) return;
            PersonDetails.AddRange(new List<Person>()
                {
                    new Person(){FirstName = "Eldad", LastName = "Michaeli", Email = "Eldadmo@gmail.com"}
                });
            SaveChanges();
        }

        private void CreateTableOperation(string tableName,string columns)
        {
            if(string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columns)) return;
            string command = $"CREATE TABLE IF NOT EXISTS '{tableName}' ({columns});";
            Database.ExecuteSqlCommand(command);
        }
    }
}