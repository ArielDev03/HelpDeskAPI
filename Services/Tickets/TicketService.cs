using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces;
using HelpDeskAPI.Mappers.Tickets;
using HelpDeskAPI.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;
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
            _logger.LogInformation("Obteniendo todos los tickets");

            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .Include(t => t.UsuarioAsignado)
                .ToListAsync();

            var ticketDtos = tickets.Select(t => t.ToTicketDto()).ToList();

            return ticketDtos;
        }

        public async Task<TicketDetailDto> GetTicketById(int id)
        {
            _logger.LogInformation("Buscando ticket {TicketId}",id);

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
                _logger.LogWarning("Ticket {TicketId} no encontrado",id);

                throw new NotFoundException("Ticket no encontrado");
            }

            return ticketDetail.ToTicketDetailDto();
        }

        public async Task<TicketDto> CreateTicket(CreateTicketDto ticketDto)
        {
            _logger.LogInformation("Creando ticket {UsuarioId}", ticketDto.UsuarioId);

            var user = await _context.Users.FindAsync(ticketDto.UsuarioId);

            if(user == null)
            {
                _logger.LogWarning("Usuario no encontrado con ID: {UsuarioId}", ticketDto.UsuarioId);
                throw new NotFoundException("Usuario no encontrado");
            }    

            var ticket = ticketDto.CreateTicketDto();

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Ticket creado con ID: {TicketId}", ticket.Id);

            var ticketDb = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Usuario)
                .FirstAsync(t => t.Id == ticket.Id);

            return ticketDb.ToTicketDto();
        }

    }
}
