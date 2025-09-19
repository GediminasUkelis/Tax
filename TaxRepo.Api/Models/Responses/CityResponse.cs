using System.ComponentModel.DataAnnotations;

namespace TaxRepo.Api.Models.Responses
{
    public class CityResponse
    {
        public int Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "City name is required.")]
        public string Name { get; set; }
    }
}