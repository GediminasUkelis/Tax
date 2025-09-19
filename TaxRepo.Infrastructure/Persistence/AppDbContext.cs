using Microsoft.EntityFrameworkCore;
using TaxRepo.Domain.Entities;

namespace TaxRepo.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<City> Cities { get; set; }
        
        public DbSet<TaxRule> TaxRules { get; set; }
    }
}