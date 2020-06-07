using DomainContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DomainContext
{
    public class LocalDomainContext : DbContext
    {
        public DbSet<PyInfo> YearResults { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=tuongsoDb.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>();
        }


        private static readonly LocalDomainContext _TuongSoContext = new LocalDomainContext();
        public static async Task<LocalDomainContext> GetContext(string path = null)
        {
            await _TuongSoContext.Database.EnsureCreatedAsync();
            return _TuongSoContext;
        }
    }
}
