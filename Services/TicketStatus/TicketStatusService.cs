using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Status;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Repositories.TicketsStatus;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.TicketStatus
{
    public class TicketStatusService : ITicketStatusService
    {
        private readonly ITicketStatusRepository _ticketStatusRepository;
        private readonly ILogger<TicketStatusService> _logger;

        public TicketStatusService(ITicketStatusRepository ticketStatusRepository,
            ILogger<TicketStatusService> logger)
        {
            _ticketStatusRepository = ticketStatusRepository;
            _logger = logger;
        }

        public async Task<List<TicketStatusDto>> GetAllStatuses()
        {
            _logger.LogInformation("Obteniendo todos los estados del ticket");

            var statuses = await _ticketStatusRepository.GetAllAsync();

            return statuses
                .Select(s => new TicketStatusDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                })
                .ToList();
        }
    }
}
