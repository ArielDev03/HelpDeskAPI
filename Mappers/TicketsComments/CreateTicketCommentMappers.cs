using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Models.TicketComments;

namespace HelpDeskAPI.Mappers.TicketsComments
{
    public static class CreateTicketCommentMappers
    {
        public static TicketComment CreateTicketCommentDto(this CreateTicketCommentDto comment, int idTicket)
        {
            return new TicketComment
            {
                Comentario = comment.Comentario,
                FechaCreacion = DateTime.UtcNow,
                TicketId = idTicket,
                UsuarioId = comment.UsuarioId
            };
        }
    }
}
   
