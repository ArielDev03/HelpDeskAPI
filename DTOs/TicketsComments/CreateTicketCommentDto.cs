namespace HelpDeskAPI.DTOs.TicketsComments
{
    public class CreateTicketCommentDto
    {
        public string Comentario { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }
}
