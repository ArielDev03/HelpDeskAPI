using FluentValidation;
using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Mappers.TicketsComments;
using HelpDeskAPI.Models.TicketComments;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.TicketsComments
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TicketCommentService> _logger;
        private readonly IValidator<CreateTicketCommentDto> _createValidator;



        public TicketCommentService(AppDbContext context,
            ILogger<TicketCommentService> logger,
            IValidator<CreateTicketCommentDto> createValidator)

        {
            _context = context;
            _logger = logger;
            _createValidator = createValidator;
        }

        public async Task<List<TicketCommentDto>> GetAllTicketsComments(int id)
        {
            _logger.LogInformation("Obteniendo todos los comentarios del ticket: {TicketId}", id);

            var ticketExiste = await _context.Tickets
                .AnyAsync(t => t.Id == id); //AnyAsync(): valida que si existe

            if (!ticketExiste)
            {
                _logger.LogWarning("Ticket {TicketId} no encontrado", id);

                throw new NotFoundException("Ticket no encontrado");
            }

            var ticketsComments = await _context.TicketComments
                .Where(tc => tc.TicketId == id)
                .Include(tc => tc.Usuario)
                .ToListAsync();

            return ticketsComments
                   .Select(tc => tc.ToTicketCommentDto())
                   .ToList();
        }

        public async Task<TicketCommentDto> CreateTicketsComments(CreateTicketCommentDto dto, int id)
        {

            _logger.LogInformation("Creando comentario del ticket: {TicketId}", id);

            var result = await _createValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new BusinessException(
                    result.Errors.First().ErrorMessage);
            }

            var ticketExiste = await _context.Tickets
                .AnyAsync(t => t.Id == id); //AnyAsync(): valida que si existe

            if (!ticketExiste)
            {
                _logger.LogWarning("Ticket {TicketId} no encontrado", id);

                throw new NotFoundException("Ticket no encontrado");
            }

            var userExiste = await _context.Users.AnyAsync(t => t.Id == dto.UsuarioId);

            if (!userExiste)
            {
                _logger.LogWarning("Usuario no encontrado con ID: {UsuarioId}", dto.UsuarioId);
                throw new NotFoundException("Usuario no encontrado");
            }

            var comment = dto.CreateTicketCommentDto(id); 

            _context.TicketComments.Add(comment);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Comentario del ticket creado con ID: {TicketId}", id);

            var commentDb = await _context.TicketComments
                .Include(t => t.Usuario)
                .FirstAsync(t => t.Id == comment.Id);

            return commentDb.ToTicketCommentDto();
        }

    }
}
