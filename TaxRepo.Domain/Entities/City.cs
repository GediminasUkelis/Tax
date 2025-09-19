namespace TaxRepo.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<TaxRule> TaxRules { get; set; } = new List<TaxRule>();
    }
}