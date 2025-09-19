using System.Collections.Generic;
using System.Linq;
using TaxRepo.Application.Dto;
using TaxRepo.Application.Services.Interfaces;
using TaxRepo.Infrastructure.Repositories.Interfaces;

namespace TaxRepo.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        
        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        
        public List<CityDto> GetAll()
        {
            return cityRepository.GetAll()
                .Select(x => new CityDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}