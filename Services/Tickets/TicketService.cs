using FluentValidation;
using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces;
using HelpDeskAPI.Mappers.Tickets;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketService> _logger;
        private readonly IValidator<UpdateTicketDto> _validator;

        public TicketService(AppDbContext context, ILogger<TicketService> logger, IValidator<UpdateTicketDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
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


        public async Task UpdateTicket(UpdateTicketDto ticketDto, int id)
        {
            _logger.LogInformation("Actualizando ticket {id}", id);

            var validationResult = await _validator.ValidateAsync(ticketDto);

            if (!validationResult.IsValid)
            {
                throw new BusinessException(
                    validationResult.Errors.First().ErrorMessage);
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket no encontrado con ID: {id}", id);
                throw new NotFoundException("Ticket no encontrado");
            }

            if (ticketDto.UsuarioAsignadoId.HasValue)
            {
                var usuarioExiste = await _context.Users
                    .AnyAsync(u => u.Id == ticketDto.UsuarioAsignadoId);

                if (!usuarioExiste)
                {
                    _logger.LogWarning("Usuario no encontrado con ID: {usuarioExiste}", id);
                    throw new BusinessException("Usuario asignado no válido");
                }
            }

            // AnyAsyncv No trae registros. ¿Existe? Sí o No
            var estadoExiste = await _context.TicketStatuses
                .AnyAsync(e => e.Id == ticketDto.EstadoId);

            if (!estadoExiste)
            {
                _logger.LogWarning("Estado no válido. EstadoId: {EstadoId}", ticketDto.EstadoId);
                throw new BusinessException("Estado no válido");
            }

            var prioridadExiste = await _context.TicketPriorities
                .AnyAsync(p => p.Id == ticketDto.PrioridadId);

            if (!prioridadExiste)
            {
                _logger.LogWarning("Prioridad no válida. PrioridadId: {PrioridadId}", ticketDto.PrioridadId);
                throw new BusinessException("Prioridad no válida");
            }

            ticket.Titulo = ticketDto.Titulo;
            ticket.Descripcion = ticketDto.Descripcion;
            ticket.EstadoId = ticketDto.EstadoId;
            ticket.PrioridadId = ticketDto.PrioridadId;
            ticket.UsuarioAsignadoId = ticketDto.UsuarioAsignadoId;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteTicket(int id)
        {
            _logger.LogInformation("Eliminando ticket {TicketId}",id);

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket no encontrado. TicketId: {TicketId}",id);

                throw new NotFoundException("Ticket no encontrado");
            }

            if (ticket.EstadoId != 4)
            {
                throw new BusinessException("Solo se pueden eliminar tickets cerrados");
            }

            _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Ticket eliminado correctamente. TicketId: {TicketId}",id);
        }

    }
}
