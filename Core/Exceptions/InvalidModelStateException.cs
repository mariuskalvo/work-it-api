using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException() { }
        public InvalidModelStateException(string message) : base(message) { }
        public InvalidModelStateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
