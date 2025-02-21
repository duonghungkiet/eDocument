using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocument.Domain.Exceptions
{
    public class RoleDomainException : Exception
    {
        public RoleDomainException(string? message) : base(message)
        {
        }

        public RoleDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
