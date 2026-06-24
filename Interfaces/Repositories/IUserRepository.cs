using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);

        Task AddAsync(User user);

        Task<User?> FindAsync(int id);
        
        Task UpdateAsync(User user);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> EmailExistsAsync(string email, int? excludeId = null);

        Task SaveChangesAsync();

        Task<User?> GetByEmailAsync(string email);

    }
}
