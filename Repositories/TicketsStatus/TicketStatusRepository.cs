using HelpDeskAPI.Data;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Models.TicketsStatus;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories.TicketsStatus
{
    public class TicketStatusRepository : ITicketStatusRepository
    {
        private readonly AppDbContext _context;

        public TicketStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketStatus>> GetAllAsync()
        {
            return await _context.TicketStatuses
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
