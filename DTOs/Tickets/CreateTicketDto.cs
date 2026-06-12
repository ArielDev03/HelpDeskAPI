using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs.Tickets
{
    public class CreateTicketDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int UsuarioId { get; set; }

    }
}
