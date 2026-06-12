using HelpDeskAPI.Models.TicketComments;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface ITicketCommentRepository
    {
        Task<List<TicketComment>> GetByTicketIdAsync(int ticketId);

        Task<TicketComment?> GetByIdAsync(int id);

        Task AddAsync(TicketComment comment);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
