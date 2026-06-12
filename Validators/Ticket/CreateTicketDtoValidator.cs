using FluentValidation;
using HelpDeskAPI.DTOs.Tickets;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.Validators.Ticket
{
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketDtoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("El título es obligatorio")
                .MaximumLength(100)
                .WithMessage("El título no puede superar los 100 caracteres");

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripción es obligatoria")
                .MaximumLength(500)
                .WithMessage("La descripción no puede superar los 500 caracteres");

            RuleFor(x => x.UsuarioId)
                .GreaterThan(0)
                .WithMessage("El usuario es obligatorio");
        }
    }
}
