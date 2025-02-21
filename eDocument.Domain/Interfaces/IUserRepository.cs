using eDocument.Domain.Entities;

namespace eDocument.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task SaveChangesAsync();
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
    }
}
