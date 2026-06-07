using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Models.TicketComments
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; } = null!;
        public int UsuarioId { get; set; }
        public User Usuario { get; set; } = null!;


    }
}
