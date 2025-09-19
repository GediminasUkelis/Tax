using Microsoft.EntityFrameworkCore;
using TaxRepo.Domain.Entities;
using TaxRepo.Infrastructure.Exceptions;
using TaxRepo.Infrastructure.Persistence;
using TaxRepo.Infrastructure.Repositories.Interfaces;

namespace TaxRepo.Infrastructure.Repositories
{
     public class TaxRuleRepository : ITaxRuleRepository
    {
        private readonly AppDbContext appDbContext;

        public TaxRuleRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(TaxRule taxRule)
        {
            var result =  await this.appDbContext.AddAsync(taxRule);
            await this.appDbContext.SaveChangesAsync();
            
            return result.Entity.Id;
        }

        public async Task UpdateAsync(int cityId, int taxRuleId, TaxRule taxRule)
        {
            var taxRuleEntity = await this.appDbContext.TaxRules
                .SingleOrDefaultAsync(x => x.Id == taxRuleId && x.CityId == cityId);
            
            if (taxRuleEntity == null)
            {
                throw new EntityNotFoundException($"TaxRule {taxRuleId} for City {cityId} not found");
            }
            
            taxRuleEntity.Category = taxRule.Category;
            taxRuleEntity.Rate = taxRule.Rate;
            taxRuleEntity.ValidFrom = taxRule.ValidFrom;
            taxRuleEntity.ValidTo = taxRule.ValidTo;
            taxRuleEntity.ModifiedAt = DateTime.UtcNow;
            
            await this.appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int taxRuleId)
        {
            var taxRuleEntity = await this.appDbContext.TaxRules
                .SingleOrDefaultAsync(x => x.Id == taxRuleId);
            
            if (taxRuleEntity == null)
            {
                throw new EntityNotFoundException($"TaxRule {taxRuleId} not found");
            }
            
            this.appDbContext.TaxRules.Remove(taxRuleEntity);
            await this.appDbContext.SaveChangesAsync();
        }

        public IQueryable<TaxRule> GetAllTaxesForCity(int cityId)
        {
            return this.appDbContext.TaxRules.Where(x => x.CityId == cityId);
        }

        public async Task<decimal> GetTaxesForCityOnSpecificDateAsync(int cityId, DateOnly date)
        {
            var firstValidRule = await appDbContext.TaxRules
                .Where(x => x.CityId == cityId &&
                               x.ValidFrom <= date &&
                               x.ValidTo >= date)
                .OrderBy(x => x.Category)
                .ThenByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (firstValidRule == null)
            {
                throw new EntityNotFoundException($"No valid tax rule found for city id: {cityId}");
            }
            
            return firstValidRule.Rate;
        }

        public async Task<decimal> GetTaxAverageForCityOverPeriodOfTime(int cityId, DateOnly from, DateOnly to)
        {
            var rules = await appDbContext.TaxRules
                .Where(x => x.CityId == cityId &&
                                    x.ValidFrom <= to &&
                                    x.ValidTo >= from)
                .OrderBy(x => x.Category)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();

            if (rules.Count == 0)
            {
                throw new EntityNotFoundException($"No valid tax rules found for city id: {cityId} within the specified period.");
            }

            decimal sum = 0;
            for (var d = from; d.DayNumber <= to.DayNumber; d = d.AddDays(1))
            {
                var rule = rules.FirstOrDefault(x => x.ValidFrom <= d && x.ValidTo >= d);
                if (rule == null)
                {
                    throw new EntityNotFoundException($"No valid tax rule found for city id: {cityId} on {d}");
                }

                sum += rule.Rate;
            }
            
            var totalDays = (to.DayNumber - from.DayNumber) + 1;
            
            return sum / totalDays;
        }
    }
}