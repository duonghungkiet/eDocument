using eDocument.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocument.Domain.Entities
{
    public class UserRole
    {
        public string UserId { get; private set; }
        public string RoleId { get; private set; }

        private UserRole() { }

        public UserRole(string userId, string roleId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new UserDomainException("UserId is required.");
            if (string.IsNullOrWhiteSpace(roleId))
                throw new UserDomainException("RoleId is required.");

            UserId = userId;
            RoleId = roleId;
        }
    }
}
