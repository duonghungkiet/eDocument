using eDocument.Domain.Entities;
using eDocument.Domain.Interfaces;
using eDocument.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eDocument.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(string id) => 
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByEmailAsync(string email) => 
            await _context.Users.FirstOrDefaultAsync(u => EF.Property<string>(u, "Email") == email);
           
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
