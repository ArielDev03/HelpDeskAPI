using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "La edad debe ser mayor a 0.")]
        public int? Edad { get; set; }
        [Required]
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; }
    }
}
