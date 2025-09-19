using System;

namespace TaxRepo.Application.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string exception) : base(exception)
        {
        }
    }
}