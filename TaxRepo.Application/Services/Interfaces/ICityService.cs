using System.Collections.Generic;
using TaxRepo.Application.Dto;

namespace TaxRepo.Application.Services.Interfaces
{
    public interface ICityService
    {
        public List<CityDto> GetAll();
    }
}