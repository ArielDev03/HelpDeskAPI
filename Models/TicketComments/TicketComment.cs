using HelpDeskAPI.Models.Tickets;

namespace HelpDeskAPI.Models.TicketComments
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; } = null!;


    }
}
