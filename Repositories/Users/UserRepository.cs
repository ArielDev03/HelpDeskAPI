using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(t => t.Id == id);
        }
        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Users.AnyAsync(u =>
                u.Email == email &&
                (!excludeId.HasValue || u.Id != excludeId));
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> FindAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


    }

}
