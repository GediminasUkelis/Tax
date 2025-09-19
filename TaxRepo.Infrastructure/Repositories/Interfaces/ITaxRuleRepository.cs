using TaxRepo.Domain.Entities;

namespace TaxRepo.Infrastructure.Repositories.Interfaces
{
    public interface ITaxRuleRepository
    {
        public Task<int> AddAsync(TaxRule taxRule);
        
        public Task UpdateAsync(int cityId, int taxRuleId, TaxRule taxRule);
        
        public Task DeleteAsync(int taxRuleId);
        
        public IQueryable<TaxRule> GetAllTaxesForCity(int cityId);
        
        public Task<decimal> GetTaxesForCityOnSpecificDateAsync(int cityId, DateOnly date);
        
        public Task<decimal> GetTaxAverageForCityOverPeriodOfTime(int cityId, DateOnly from, DateOnly to);
    }
}