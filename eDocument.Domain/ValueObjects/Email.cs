using eDocument.Domain.Exceptions;

namespace eDocument.Domain.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Value { get; set; }
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                throw new UserDomainException("Invalid email format");
            }
            Value = email;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        public override bool Equals(object obj) => Equals(obj as Email);
        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
        public override string ToString() => Value;

        public bool Equals(Email other) => other != null &&
                                            Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
}
