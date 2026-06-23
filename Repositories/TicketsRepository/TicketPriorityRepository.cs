
using HelpDeskAPI.Data;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Models.TicketsPriority;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories.TicketsRepository
{
    public class TicketPriorityRepository : ITicketPriorityRepository
    {
        private readonly AppDbContext _context;

        public TicketPriorityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketPriority>> GetAllAsync()
        {
            return await _context.TicketPriorities
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
