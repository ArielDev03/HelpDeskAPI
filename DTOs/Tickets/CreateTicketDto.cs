using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs.Tickets
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public int UsuarioId { get; set; }

    }
}
