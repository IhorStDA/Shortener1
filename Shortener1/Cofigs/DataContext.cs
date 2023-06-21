using ConsoleApp2205.Entities;
using Microsoft.EntityFrameworkCore;
using Shortener1.Entities;

namespace Shortener1.Cofigs
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        } 
         public DbSet<UrlMap> UrlMap  => Set<UrlMap>();
         public DbSet<MyUser> MyUser => Set<MyUser>();
         public DbSet<MarketingData> MarketingData => Set<MarketingData>();

    }
}
