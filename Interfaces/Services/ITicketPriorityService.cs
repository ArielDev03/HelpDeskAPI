using HelpDeskAPI.DTOs.Priorities;

namespace HelpDeskAPI.Interfaces.Services
{
    public interface ITicketPriorityService
    {
        Task<List<TicketPriorityDto>> GetAllPriorities();
    }
}
