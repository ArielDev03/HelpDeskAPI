using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);

        Task AddAsync(User user);

        void Update(User user);

        void Delete(User user);

        Task<bool> ExistsAsync(int id);

        Task<bool> EmailExistsAsync(string email);

        Task SaveChangesAsync();
    }
}
