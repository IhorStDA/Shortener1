using Microsoft.EntityFrameworkCore;
using Shortener1.Entities;

namespace Shortener1.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UrlMap> UrlMap => Set<UrlMap>();

        public DbSet<MarketingData> MarketingData => Set<MarketingData>();
    }
}