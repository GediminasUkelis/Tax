using Microsoft.AspNetCore.Mvc;
using TaxRepo.Api.Infrastructure.Authorization;
using TaxRepo.Api.Models.Responses;
using TaxRepo.Application.Services.Interfaces;

namespace TaxRepo.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }
        
        [RoleHeaderAuthorize("User")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(cityService.GetAll()
                .Select(x => new CityResponse
                {
                    Id = x.Id,
                    Name = x.Name
                }));
        }
    }
}