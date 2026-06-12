using HelpDeskAPI.Models.Tickets;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(int id);

        Task AddAsync(Ticket ticket);

        void Update(Ticket ticket);

        void Delete(Ticket ticket);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
