namespace TaxRepo.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}