using eDocument.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocument.Domain.DomainEvents
{
    public class UserRegisteredEvent
    {
        public User User { get; }
        public DateTime OccurredOn { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
