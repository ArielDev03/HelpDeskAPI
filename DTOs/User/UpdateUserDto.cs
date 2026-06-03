using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "La edad debe ser mayor a 0.")]
        public int? Edad { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; } = string.Empty;
    }
}
