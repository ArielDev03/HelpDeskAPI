using HelpDeskAPI.Models.TicketsPriority;

namespace HelpDeskAPI.Interfaces.Repositories
{
    public interface ITicketPriorityRepository
    {
        Task<List<TicketPriority>> GetAllAsync();

    }
}
