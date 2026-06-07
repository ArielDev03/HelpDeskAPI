using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskAPI.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketDto>> GetAllTickets();

        Task<TicketDetailDto> GetTicketById(int id);
    }
}
