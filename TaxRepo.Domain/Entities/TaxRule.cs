using TaxRepo.Domain.Enums;

namespace TaxRepo.Domain.Entities
{
    public class TaxRule
    {
        public int Id { get; set; }
        
        public int CityId { get; set; }
        
        public TaxCategory Category { get; set; }
        
        public decimal Rate { get; set; }
        
        public DateOnly ValidFrom { get; set; }
        
        public DateOnly ValidTo { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? ArchiveDate { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
    }
}