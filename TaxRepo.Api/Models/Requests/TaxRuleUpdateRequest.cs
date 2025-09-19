using System.ComponentModel.DataAnnotations;

namespace TaxRepo.Api.Models.Requests
{
    public class TaxRuleUpdateRequest : TaxRuleRequest
    {
        [Required]
        public int CityId { get; set; }
    }
}