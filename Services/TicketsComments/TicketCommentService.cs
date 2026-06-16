using FluentValidation;
using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Mappers.TicketsComments;
using HelpDeskAPI.Models.TicketComments;
using HelpDeskAPI.Interfaces.Repositories;

namespace HelpDeskAPI.Services.TicketsComments
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketCommentService> _logger;
        private readonly IValidator<CreateTicketCommentDto> _createValidator;


        public TicketCommentService(
            ITicketCommentRepository ticketCommentRepository,
            IUserRepository userRepository,
            ITicketRepository ticketRepository,
            ILogger<TicketCommentService> logger,
            IValidator<CreateTicketCommentDto> createValidator)

        {

            _ticketCommentRepository = ticketCommentRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _logger = logger;
            _createValidator = createValidator;
        }

        public async Task<List<TicketCommentDto>> GetAllTicketsComments(int id)
        {
            _logger.LogInformation("Obteniendo todos los comentarios del ticket: {TicketId}", id);

            var ticketExists = await _ticketRepository.ExistsAsync(id);

            if (!ticketExists)
            {
                _logger.LogWarning("Ticket {TicketId} no encontrado", id);

                throw new NotFoundException("Ticket no encontrado");
            }

            var ticketsComments = await _ticketCommentRepository.GetByTicketIdAsync(id);

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

            var ticketExists = await _ticketRepository.ExistsAsync(id);
            if (!ticketExists)
            {
                _logger.LogWarning("Ticket {TicketId} no encontrado", id);

                throw new NotFoundException("Ticket no encontrado");
            }

            var userExiste = await _userRepository.ExistsAsync(dto.UsuarioId);

            if (!userExiste)
            {
                _logger.LogWarning("Usuario no encontrado con ID: {UsuarioId}", dto.UsuarioId);
                throw new NotFoundException("Usuario no encontrado");
            }

            var comment = dto.CreateTicketCommentDto(id);

            await _ticketCommentRepository.AddAsync(comment);

            await _ticketCommentRepository.SaveChangesAsync();

            _logger.LogInformation("Comentario del ticket creado con ID: {TicketId}", id);

            var commentDb = await _ticketCommentRepository.GetByIdAsync(comment.Id);

            if (commentDb is null)
            {
                _logger.LogWarning("Comentario no encontrado. ID: {commentId}", comment.Id);
                throw new NotFoundException("Comentario no encontrado");
            }

            return commentDb.ToTicketCommentDto();
        }

    }
}
