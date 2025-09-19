using Microsoft.EntityFrameworkCore;
using TaxRepo.Domain.Entities;
using TaxRepo.Infrastructure.Persistence;
using TaxRepo.Infrastructure.Repositories.Interfaces;

namespace TaxRepo.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext appDbContext;
        
        public CityRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
        public List<City> GetAll()
        {
            return appDbContext.Cities.AsNoTracking().ToList();
        }
    }
}