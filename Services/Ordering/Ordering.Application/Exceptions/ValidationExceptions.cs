
using FluentValidation.Results;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ordering.Application.Exceptions
{
    public class ValidationExceptions : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ValidationExceptions() : base("one or more validation failures have occurred")
        {
            Errors = new Dictionary<string, string[]>();    
        }
        public ValidationExceptions(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(s=> s.PropertyName, e=> e.ErrorMessage).ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        
    }
}
