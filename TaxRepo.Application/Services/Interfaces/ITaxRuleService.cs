
using System;
using System.Linq;
using System.Threading.Tasks;
using TaxRepo.Application.Dto;

namespace TaxRepo.Application.Services.Interfaces
{
    public interface ITaxRuleService
    {
        public Task<int> AddAsync(TaxRuleDto taxRule);
        
        public Task UpdateAsync(int cityId, int taxRuleId, TaxRuleDto taxRule);
        
        public Task DeleteAsync(int taxRuleId);

        public IQueryable<TaxRuleDto> GetAllTaxesForCity(int cityId);
        
        public Task<decimal> GetTaxesForCityOnSpecificDateAsync(int cityId, DateOnly date);
        
        public Task<decimal> GetTaxAverageForCityOverPeriodOfTime(int cityId, DateOnly from, DateOnly to);
    }
}