using System;
using System.Linq;
using System.Threading.Tasks;
using TaxRepo.Application.Dto;
using TaxRepo.Application.Exceptions;
using TaxRepo.Application.Services.Interfaces;
using TaxRepo.Domain.Entities;
using TaxRepo.Infrastructure.Repositories.Interfaces;

namespace TaxRepo.Application.Services
{
    public class TaxRuleService : ITaxRuleService
    {
        private readonly ITaxRuleRepository taxRuleRepository;

        public TaxRuleService(ITaxRuleRepository taxRuleRepository)
        {
            this.taxRuleRepository = taxRuleRepository;
        }

        public async Task<int> AddAsync(TaxRuleDto taxRule)
        {
            return await this.taxRuleRepository.AddAsync(new TaxRule
            {
                CityId = taxRule.CityId,
                Category = taxRule.Category,
                Rate = taxRule.Rate,
                ValidFrom = taxRule.ValidFrom,
                ValidTo = taxRule.ValidTo,
            });
        }

        public async Task UpdateAsync(int cityId, int taxRuleId, TaxRuleDto taxRule)
        {
            if (taxRule.ValidFrom > taxRule.ValidTo)
            {
                throw new InvalidDateRangeException(
                    $"Invalid date range: start date must not be after end date. Received start={taxRule.ValidFrom:yyyy-MM-dd}, end={taxRule.ValidTo:yyyy-MM-dd}.");

            }
            
            await this.taxRuleRepository.UpdateAsync(cityId, taxRuleId, new TaxRule
            {
                CityId = taxRule.CityId,
                Category = taxRule.Category,
                Rate = taxRule.Rate,
                ValidFrom = taxRule.ValidFrom,
                ValidTo = taxRule.ValidTo,
            });
        }

        public async Task DeleteAsync(int taxRuleId)
        {
            await this.taxRuleRepository.DeleteAsync(taxRuleId);
        }

        public IQueryable<TaxRuleDto> GetAllTaxesForCity(int cityId)
        {
            return this.taxRuleRepository.GetAllTaxesForCity(cityId)
                .Select(x => new TaxRuleDto
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    Category = x.Category,
                    Rate = x.Rate,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                    CreatedAt = x.CreatedAt,
                    ArchiveDate = x.ArchiveDate,
                    ModifiedAt = x.ModifiedAt,
                });
        }

        public async Task<decimal> GetTaxesForCityOnSpecificDateAsync(int cityId, DateOnly date)
        {
            return await this.taxRuleRepository.GetTaxesForCityOnSpecificDateAsync(cityId, date);
        }

        public Task<decimal> GetTaxAverageForCityOverPeriodOfTime(int cityId, DateOnly from, DateOnly to)
        {
            if (to < from)
            {
                throw new InvalidDateRangeException(
                    $"Invalid date range: start date must not be after end date. Received start={from:yyyy-MM-dd}, end={to:yyyy-MM-dd}.");
            }
            
            return this.taxRuleRepository.GetTaxAverageForCityOverPeriodOfTime(cityId, from, to);
        }
    }
}