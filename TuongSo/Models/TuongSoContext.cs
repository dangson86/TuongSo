using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TuongSo.Models
{
    public class TuongSoContext : DbContext
    {
        public DbSet<YearResultModel> YearResults { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=tuongsoDb.db");
        }


        private static readonly TuongSoContext _TuongSoContext = new TuongSoContext();
        public static async Task<TuongSoContext> GetContext(string path = null)
        {
            await _TuongSoContext.Database.EnsureCreatedAsync();
            return _TuongSoContext;
        }
    }
}
