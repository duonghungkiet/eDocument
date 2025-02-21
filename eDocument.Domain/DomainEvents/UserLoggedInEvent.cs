using eDocument.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocument.Domain.DomainEvents
{
    public class UserLoggedInEvent
    {
        public User User { get; }
        public UserLoggedInEvent(User user)
        {
            User = user;
        }
    }
}
