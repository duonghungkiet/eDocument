using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eDocument.Domain.Exceptions
{
    public class UserDomainException : Exception
    {
        public UserDomainException(string? message) : base(message)
        {
        }

        public UserDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
