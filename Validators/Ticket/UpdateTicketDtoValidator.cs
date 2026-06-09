using FluentValidation;
using HelpDeskAPI.DTOs.Tickets;

namespace HelpDeskAPI.Validators.Tickets
{
    public class UpdateTicketDtoValidator : AbstractValidator<UpdateTicketDto>
    {

        //IValidator: el contrato para validar un objeto(DTOs) T. Es como una Interface

        public UpdateTicketDtoValidator()
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

            RuleFor(x => x.EstadoId)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un estado válido");

            RuleFor(x => x.PrioridadId)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar una prioridad válida");

            RuleFor(x => x.UsuarioAsignadoId)
                .GreaterThan(0)
                .When(x => x.UsuarioAsignadoId.HasValue)
                .WithMessage("El usuario asignado debe ser válido");
        }
    }
}