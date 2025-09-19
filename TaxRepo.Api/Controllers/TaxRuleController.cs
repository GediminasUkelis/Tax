using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxRepo.Api.Infrastructure.Authorization;
using TaxRepo.Api.Models.Requests;
using TaxRepo.Api.Models.Responses;
using TaxRepo.Application.Dto;
using TaxRepo.Application.Services.Interfaces;

namespace TaxRepo.Api.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId:int}/taxRules")]
    public class TaxRuleController : ControllerBase
    {
        private readonly ITaxRuleService taxRuleService;

        public TaxRuleController(ITaxRuleService taxRuleService)
        {
            this.taxRuleService = taxRuleService;
        }
        
        [RoleHeaderAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(int cityId, TaxRuleRequest taxRuleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (taxRuleRequest.ValidFrom > taxRuleRequest.ValidTo)
            {
                return BadRequest("Valid from date cannot be after valid to date");
            }
            
            var result = await this.taxRuleService.AddAsync(new TaxRuleDto
            {
                CityId = cityId,
                Category = taxRuleRequest.Category,
                Rate = taxRuleRequest.Rate,
                ValidFrom = taxRuleRequest.ValidFrom,
                ValidTo = taxRuleRequest.ValidTo,
            });

            return Ok(result);
        }

        [RoleHeaderAuthorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int cityId, int taxRuleId, TaxRuleUpdateRequest taxRuleUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await this.taxRuleService.UpdateAsync(cityId, taxRuleId, new TaxRuleDto
            {
                Category = taxRuleUpdateRequest.Category,
                Rate = taxRuleUpdateRequest.Rate,
                ValidFrom = taxRuleUpdateRequest.ValidFrom,
                ValidTo = taxRuleUpdateRequest.ValidTo,
                CityId = taxRuleUpdateRequest.CityId
            });
            
            return Ok();
        }

        [RoleHeaderAuthorize("Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int taxRuleId)
        {
            await this.taxRuleService.DeleteAsync(taxRuleId);
            
            return Ok();
        }

        [RoleHeaderAuthorize("User")]
        [HttpGet]
        public async Task<IActionResult> GetAllTaxesForCityAsync(int cityId)
        {
            return Ok(await this.taxRuleService
                .GetAllTaxesForCity(cityId)
                .Select(x => new TaxRuleResponse
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
                })
                .ToListAsync());
        }
        
        [RoleHeaderAuthorize("User")]
        [HttpGet("{date:Datetime}")]
        public async Task<IActionResult> GetTaxesForCityOnSpecificDateAsync(int cityId, DateTime date)
        {
            return Ok(await this.taxRuleService
                .GetTaxesForCityOnSpecificDateAsync(cityId, DateOnly.FromDateTime(date)));
        }
        
        [RoleHeaderAuthorize("User")]
        [HttpGet("average")]
        public async Task<IActionResult> GetTaxAverageForCityOverPeriodOfTime(int cityId, DateTime from, DateTime to)
        {
            return Ok(await this.taxRuleService
                .GetTaxAverageForCityOverPeriodOfTime(cityId, DateOnly.FromDateTime(from), DateOnly.FromDateTime(to)));
        }
    }
}