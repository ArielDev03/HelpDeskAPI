using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces;
using HelpDeskAPI.Mappers.Tickets;
using HelpDeskAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketService> _logger;

        public TicketService(AppDbContext context, ILogger<TicketService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TicketDto>> GetAllTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.UsuarioAsignado)
                .ToListAsync();

            var ticketDtos = tickets.Select(t => t.ToTicketDto()).ToList();

            return ticketDtos;
        }

        public async Task<TicketDetailDto> GetTicketById(int id)
        {
            //consulta anidada
            var ticketDetail = await _context.Tickets
                .Include(t => t.Estado) //Trae una relación
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.Comentarios)
                .ThenInclude(c => c.Usuario) // Trae una relación de esa relación 
                .FirstOrDefaultAsync(t => t.Id == id);


            if (ticketDetail == null)
            {
                throw new NotFoundException("Ticket no encontrado");
            }

            return ticketDetail.ToTicketDetailDto();
        }
    }
}
