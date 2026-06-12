using FluentValidation;
using HelpDeskAPI.DTOs.TicketsComments;

namespace HelpDeskAPI.Validators.TicketComment
{
    public class CreateTicketCommentDtoValidator: AbstractValidator<CreateTicketCommentDto>
    {
        public CreateTicketCommentDtoValidator()
        {
            RuleFor(x => x.Comentario)
                .NotEmpty()
                .WithMessage("El Comentario es obligatorio")
                .MaximumLength(500)
                .WithMessage("El Comentario no puede superar los 500 caracteres");


            RuleFor(x => x.UsuarioId)
                .GreaterThan(0)
                .WithMessage("El usuario debe ser valido");
        }
    }
}
