using FluentValidation;
using HelpDeskAPI.DTOs.User;

namespace HelpDeskAPI.Validators.Users
{
    public class CreateUserDtoValidator: AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator() 
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("El nombre es obligatorio")
               .MinimumLength(2)
               .WithMessage("El nombre debe tener al menos 2 caracteres")
               .MaximumLength(100)
               .WithMessage("El nombre no puede superar los 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo es obligatorio")
                .EmailAddress()
                .WithMessage("Debe ingresar un correo válido")
                .MaximumLength(255)
                .WithMessage("El correo no puede superar los 255 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria")
                .MinimumLength(8)
                .WithMessage("La contraseña debe tener al menos 8 caracteres")
                .MaximumLength(300)
                .WithMessage("La contraseña no puede superar los 300 caracteres");

            RuleFor(x => x.Edad)
                .InclusiveBetween(15, 100)
                .WithMessage("La edad debe estar entre 15 y 100 años");

            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage("La Direccion es obligatoria")
                .MaximumLength(500)
                .WithMessage("La Direccion no puede superar los 500 caracteres");

            RuleFor(x => x.Ciudad)
                .MaximumLength(500)
                .WithMessage("La Ciudad no puede superar los 500 caracteres");
        }
    }
}
