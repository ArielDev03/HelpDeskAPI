using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Priorities;
using HelpDeskAPI.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.TicketsPriority
{
    public class TicketPriorityService : ITicketPriorityService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketPriorityService> _logger;

        public TicketPriorityService(
            AppDbContext context,
            ILogger<TicketPriorityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TicketPriorityDto>> GetAllPriorities()
        {
            _logger.LogInformation("Obteniendo todas las prioridades");

            var priorities = await _context.TicketPriorities
                .Select(p => new TicketPriorityDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                })
                .ToListAsync();

            return priorities;
        }
    }
}
