using FluentValidation;
using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Mappers.Tickets;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;
using HelpDeskAPI.Repositories.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace HelpDeskAPI.Services.Tickets
{
    public class TicketService : ITicketService

    {
        private readonly AppDbContext _context;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;
        private readonly IValidator<CreateTicketDto> _createValidator;
        private readonly IValidator<UpdateTicketDto> _updateValidator;
        

        public TicketService(AppDbContext context,
            ITicketRepository ticketRepository,
            IUserRepository userRepository,
            ILogger<TicketService> logger,
            IValidator<UpdateTicketDto> updateValidator,
            IValidator<CreateTicketDto> createValidator)

        {
            _context = context;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _logger = logger;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        public async Task<List<TicketDto>> GetAllTickets()
        {
            _logger.LogInformation("Obteniendo todos los tickets");

            var tickets = await _ticketRepository.GetAllAsync();

            var ticketDtos = tickets.Select(t => t.ToTicketDto()).ToList();

            return ticketDtos;
        }

        public async Task<TicketDetailDto> GetTicketById(int id)
        {
            _logger.LogInformation("Buscando ticket {TicketId}",id);

            var ticketDetail = await _ticketRepository.GetDetailByIdAsync(id);

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

            var result = await _createValidator.ValidateAsync(ticketDto);

            if (!result.IsValid)
            {
                throw new BusinessException(
                    result.Errors.First().ErrorMessage);
            }

            var user = await _userRepository.FindAsync(ticketDto.UsuarioId); 

            if(user == null)
            {
                _logger.LogWarning("Usuario no encontrado con ID: {UsuarioId}", ticketDto.UsuarioId);
                throw new NotFoundException("Usuario no encontrado");
            }    

            var ticket = ticketDto.CreateTicketDto();


            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            _logger.LogInformation("Ticket creado con ID: {TicketId}", ticket.Id);


            var ticketDb = await _ticketRepository.GetTicketCreatedAsync(ticket.Id);

            if (ticketDb is null)
            {
                _logger.LogWarning("No se pudo recuperar el ticket recién creado: {UsuarioId}", ticketDto.UsuarioId);
                throw new NotFoundException(
                    "No se pudo recuperar el ticket recién creado");
            }

            return ticketDb.ToTicketDto();
        }

        public async Task UpdateTicket(UpdateTicketDto ticketDto, int id)
        {
            _logger.LogInformation("Actualizando ticket {id}", id);

            var validationResult = await _updateValidator.ValidateAsync(ticketDto);

            if (!validationResult.IsValid)
            {
                throw new BusinessException(
                    validationResult.Errors.First().ErrorMessage);
            }

            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket no encontrado con ID: {id}", id);
                throw new NotFoundException("Ticket no encontrado");
            }

            if (ticketDto.UsuarioAsignadoId.HasValue)
            {
                var usuarioExiste = await _userRepository.ExistsAsync(ticketDto.UsuarioAsignadoId.Value);

                if (!usuarioExiste)
                {
                    _logger.LogWarning("Usuario no encontrado con ID: {usuarioExiste}", id);
                    throw new BusinessException("Usuario asignado no válido");
                }
            }

            // AnyAsyncv No trae registros. ¿Existe? Sí o No
            var estadoExiste = await _ticketRepository
            .StatusExistsAsync(ticketDto.EstadoId);

            if (!estadoExiste)
            {
                _logger.LogWarning("Estado no válido. EstadoId: {EstadoId}", ticketDto.EstadoId);
                throw new BusinessException("Estado no válido");
            }

            var prioridadExiste = await _ticketRepository
            .PriorityExistsAsync(ticketDto.PrioridadId);

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

            await _ticketRepository.UpdateAsync(ticket);

        }

        public async Task DeleteTicket(int id)
        {
            _logger.LogInformation("Eliminando ticket {TicketId}",id);

            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket no encontrado. TicketId: {TicketId}",id);

                throw new NotFoundException("Ticket no encontrado");
            }

            if (ticket.EstadoId != 4)
            {
                throw new BusinessException("Solo se pueden eliminar tickets cerrados");
            }

            await _ticketRepository.DeleteAsync(ticket);

            _logger.LogInformation("Ticket eliminado correctamente. TicketId: {TicketId}",id);
        }

    }
}
