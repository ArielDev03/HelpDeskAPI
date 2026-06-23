using HelpDeskAPI.Models.TicketsStatus;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface ITicketStatusRepository
    {
        Task<List<TicketStatus>> GetAllAsync();

    }
}
