

namespace HelpDeskAPI.DTOs.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public string Prioridad { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } 

        public string? UsuarioAsignado { get; set; } 
    }
}
