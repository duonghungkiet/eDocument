using eDocument.Domain.DomainEvents;
using eDocument.Domain.Exceptions;
using eDocument.Domain.Interfaces;
using eDocument.Domain.ValueObjects;

namespace eDocument.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string PasswordHash { get; private set; }
        public Email Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime? LastLoginAt { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiry { get; private set; }

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private readonly List<object> _domainEvents = new List<object>();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        protected User() { } // Required by EF Core

        public static User Register(string email, string passwordHash, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UserDomainException("Email is required");
            }
            if (string.IsNullOrEmpty(firstName))
            {
                throw new UserDomainException("Firstname is required");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new UserDomainException("Lastname is required");
            }
            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new UserDomainException("Password is required");
            }

            var user = new User
            {
                Id = NanoidDotNet.Nanoid.Generate(),
                PasswordHash = passwordHash,
                Email = new Email(email),
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            user.AddDomainEvent(new UserRegisteredEvent(user));

            return user;
        }

        public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.Verify(PasswordHash, password);
        }

        public bool ChangePassword(string currentPassword, string newPassword, IPasswordHasher passwordHasher)
        {
            if (!VerifyPassword(currentPassword, passwordHasher))
            {
                return false;
            }
            PasswordHash = passwordHasher.HashPassword(newPassword);
            UpdatedAt = DateTime.UtcNow;
            return true;
        }

        public void AddRole(Role role)
        {
            if (role == null)
                throw new UserDomainException("Role cannot be null.");

            if (_userRoles.Any(ur => ur.RoleId == role.Id))
                throw new UserDomainException("User already has this role.");

            _userRoles.Add(new UserRole(this.Id, role.Id));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveRole(Role role)
        {
            if (role == null)
                throw new UserDomainException("Role cannot be null.");

            var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
            if (userRole == null)
                throw new UserDomainException("User does not have this role.");

            _userRoles.Remove(userRole);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new UserDomainException("Firstname is required");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new UserDomainException("Lastname is required");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new UserDomainException("Email is required");
            }

            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsLoggedIn()
        {
            LastLoginAt = DateTime.UtcNow;

            AddDomainEvent(new UserLoggedInEvent(this));
        }

        private void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRefreshToken(string token, DateTime expiry)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new UserDomainException("Refresh token cannot be empty.");
            RefreshToken = token;
            RefreshTokenExpiry = expiry;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
