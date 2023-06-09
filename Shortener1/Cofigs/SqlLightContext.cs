using ConsoleApp2205.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2205.Cofigs
{
    public class SqlLightContext : DbContext
    {
        private static string DbConnection = "Data Source=Data/mydatabase.db";

        public DbSet<UrlMap>? UrlMap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DbConnection);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
