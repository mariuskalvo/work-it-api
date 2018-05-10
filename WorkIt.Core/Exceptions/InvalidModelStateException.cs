using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace Core.Exceptions
{

    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException() { }
        public InvalidModelStateException(string message) : base(message) { }
        public InvalidModelStateException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ExceptionFactory
    {
        public static InvalidModelStateException CreateFromValidationResults(ValidationResult validationResults)
        {
            var errors = validationResults.Errors.Select(error => error.PropertyName);
            var errorListString = string.Join(",", errors);
            return new InvalidModelStateException($"The following properties are invalid: {errorListString}");
        }
    }
}
