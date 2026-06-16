using HelpDeskAPI.Data;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories.Tickets
{
    public class TicketRepository: ITicketRepository 
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .Include(t => t.UsuarioAsignado)
                .ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket?> GetDetailByIdAsync(int id)
        {
            //consulta anidada
            return await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.Comentarios)
                .ThenInclude(c => c.Usuario) // Trae una relación de esa relación 
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tickets.AnyAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<Ticket?> GetTicketCreatedAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        //temporales
        public async Task<bool> StatusExistsAsync(int statusId)
        {
            return await _context.TicketStatuses
                .AnyAsync(s => s.Id == statusId);
        }

        public async Task<bool> PriorityExistsAsync(int priorityId)
        {
            return await _context.TicketPriorities
                .AnyAsync(p => p.Id == priorityId);
        }

    }
}
