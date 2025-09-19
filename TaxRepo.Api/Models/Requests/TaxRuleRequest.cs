using System.ComponentModel.DataAnnotations;
using TaxRepo.Domain.Enums;

namespace TaxRepo.Api.Models.Requests
{
    public class TaxRuleRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category is required. Select from the list: Yearly, Monthly, Weekly, Daily.")]
        public TaxCategory Category { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive number.")]
        public decimal Rate { get; set; }
        
        [Required]
        public DateOnly ValidFrom { get; set; }
        
        [Required]
        public DateOnly ValidTo { get; set; }
    }
}