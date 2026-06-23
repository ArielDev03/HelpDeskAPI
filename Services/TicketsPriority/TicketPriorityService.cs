using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Priorities;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Repositories.TicketsRepository;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.TicketsPriority
{
    public class TicketPriorityService : ITicketPriorityService
    {
        private readonly ITicketPriorityRepository _ticketPriorityRepository;
        private readonly ILogger<TicketPriorityService> _logger;

        public TicketPriorityService(
            ITicketPriorityRepository ticketPriorityRepository,
            ILogger<TicketPriorityService> logger)
        {
            _ticketPriorityRepository = ticketPriorityRepository;
            _logger = logger;
        }

        public async Task<List<TicketPriorityDto>> GetAllPriorities()
        {
            _logger.LogInformation("Obteniendo todas las prioridades del ticket");

            var priorities = await _ticketPriorityRepository.GetAllAsync();

            return priorities
                .Select(p => new TicketPriorityDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                })
                .ToList();
        }
    }
}
