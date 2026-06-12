using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Status;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Services.TicketsComments;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.TicketStatus
{
    public class TicketStatusService : ITicketStatusService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketStatusService> _logger;

        public TicketStatusService(AppDbContext context,
            ILogger<TicketStatusService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TicketStatusDto>> GetAllStatuses()
        {
            _logger.LogInformation("Obteniendo todos los estados del ticket");

            return await _context.TicketStatuses
                .Select(s => new TicketStatusDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                })
                .ToListAsync();
        }
    }
}
