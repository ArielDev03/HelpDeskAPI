using HelpDeskAPI.DTOs.TicketsComments;

namespace HelpDeskAPI.DTOs.Tickets
{
    public class TicketDetailDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EstadoId { get; set; } 
        public string Estado { get; set; } = string.Empty;
        public int? PrioridadId { get; set; }
        public string? Prioridad { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public int? UsuarioAsignadoId { get; set; }
        public string UsuarioAsignado { get; set; } = string.Empty;
        public ICollection<TicketCommentDto> Comentarios { get; set; }
            = new List<TicketCommentDto>();
    }
}
