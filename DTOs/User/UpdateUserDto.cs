using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? Edad { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; } = string.Empty;
    }
}
