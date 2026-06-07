namespace HelpDeskAPI.DTOs.TicketsComments
{
    public class TicketCommentDto
    {
        public int Id { get; set; }
        public string Comentario { get; set; } = string.Empty;       
        public DateTime FechaCreacion { get; set; }
        public string Autor { get; set; } = string.Empty;
    }
}
