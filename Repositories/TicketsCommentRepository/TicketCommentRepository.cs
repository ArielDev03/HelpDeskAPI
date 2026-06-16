using HelpDeskAPI.Data;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Models.TicketComments;
using HelpDeskAPI.Models.Tickets;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories.TicketsCommentRepository
{
    public class TicketCommentRepository : ITicketCommentRepository
    {
        private readonly AppDbContext _context;

        public TicketCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketComment>> GetByTicketIdAsync(int id) 
        {
            return await _context.TicketComments
                .AsNoTracking()
                .Where(tc => tc.TicketId == id)
                .Include(tc => tc.Usuario)
                .ToListAsync();
        }
        
        public async Task<TicketComment?> GetByIdAsync(int id)
        {
            return await _context.TicketComments
                .AsNoTracking()
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TicketComment ticketComment)
        {
            await _context.TicketComments.AddAsync(ticketComment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
