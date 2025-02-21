using eDocument.Domain.Entities;
using eDocument.Domain.Interfaces;
using eDocument.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eDocument.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<Role?> GetByIdAsync(string id) =>
            await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
