using HelpDeskAPI.Models.Users;
using HelpDeskAPI.Models.TicketComments;
using HelpDeskAPI.Models.TicketsStatus;
using HelpDeskAPI.Models.TicketsPriority;


namespace HelpDeskAPI.Models.Tickets
{    public class Ticket
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int EstadoId { get; set; }

        public TicketStatus Estado { get; set; } = null!;

        public int PrioridadId { get; set; }

        public TicketPriority Prioridad { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }
            = DateTime.UtcNow;

        // Usuario que creó el ticket
        public int UsuarioId { get; set; }

        public User Usuario { get; set; } = null!;

        // Usuario asignado
        public int? UsuarioAsignadoId { get; set; }

        public User? UsuarioAsignado { get; set; } 

        public ICollection<TicketComment> Comentarios { get; set; }
            = new List<TicketComment>();
    }
}
