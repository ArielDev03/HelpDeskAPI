using HelpDeskAPI.DTOs.Status;

namespace HelpDeskAPI.Interfaces.Services
{
    public interface ITicketStatusService
    {
        Task<List<TicketStatusDto>> GetAllStatuses();
    }
}
