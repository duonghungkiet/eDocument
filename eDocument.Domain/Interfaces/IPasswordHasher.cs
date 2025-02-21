namespace eDocument.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string hashedPassword, string providedPassword);
    }
}
