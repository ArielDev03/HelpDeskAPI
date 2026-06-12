using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.TicketsComments;

namespace HelpDeskAPI.Interfaces.Services
{
    public interface ITicketCommentService
    {
        Task<List<TicketCommentDto>> GetAllTicketsComments(int id);

        Task<TicketCommentDto> CreateTicketsComments(CreateTicketCommentDto dto, int id);
    }
}
