using TaxRepo.Domain.Enums;

namespace TaxRepo.Api.Models.Responses
{
    public class TaxRuleResponse
    {
        public int Id { get; set; }
        
        public int CityId { get; set; }
        
        public TaxCategory Category { get; set; }
        
        public decimal Rate { get; set; }
        
        public DateOnly ValidFrom { get; set; }
        
        public DateOnly ValidTo { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? ArchiveDate { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
    }
}