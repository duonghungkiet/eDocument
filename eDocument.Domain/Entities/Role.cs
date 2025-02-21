using eDocument.Domain.Exceptions;

namespace eDocument.Domain.Entities
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        private readonly List<string> _permissions = new List<string>();
        public IReadOnlyCollection<string> Permissions => _permissions.AsReadOnly();

        private Role()
        {
        }

        public Role(string name, string description, IEnumerable<string> permissions = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new RoleDomainException("Role name is required");
            }

            Id = NanoidDotNet.Nanoid.Generate();
            Name = name;
            Description = description;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            if (permissions != null)
            {
                _permissions.AddRange(permissions);
            }
        }

        public void AddPermission(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
                throw new RoleDomainException("Permission cannot be empty.");
            if (!_permissions.Contains(permission))
            {
                _permissions.Add(permission);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void RemovePermission(string permission)
        {
            if (_permissions.Remove(permission))
            {
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new RoleDomainException("Role name cannot be empty.");

            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
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
    }
}
