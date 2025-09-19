using TaxRepo.Domain.Entities;

namespace TaxRepo.Infrastructure.Repositories.Interfaces
{
    public interface ICityRepository
    {
        public List<City> GetAll();
    }
}