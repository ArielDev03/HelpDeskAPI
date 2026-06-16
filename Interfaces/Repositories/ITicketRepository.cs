using HelpDeskAPI.Models.Tickets;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(int id);

        Task<Ticket?> GetDetailByIdAsync(int id);

        Task AddAsync(Ticket ticket);

        Task UpdateAsync(Ticket ticket);

        Task<bool> ExistsAsync(int id);

        Task DeleteAsync(Ticket ticket);


        Task SaveChangesAsync();

        Task<Ticket?> GetTicketCreatedAsync(int id);


        //temporales 
        Task<bool> StatusExistsAsync(int statusId);

        Task<bool> PriorityExistsAsync(int priorityId);
    }
}
