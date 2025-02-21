using eDocument.Domain.Entities;

namespace eDocument.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(string id);
        Task AddAsync(Role role);
        Task SaveChangesAsync();
        Task UpdateAsync(Role role);
    }
}
