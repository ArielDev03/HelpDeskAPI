using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Models.TicketComments;

namespace HelpDeskAPI.Mappers.TicketsComments
{
    public static class TicketCommentMappers
    {
        public static TicketCommentDto ToTicketCommentDto(this TicketComment comment)
        {
            return new TicketCommentDto
            {
                Id = comment.Id,
                Comentario = comment.Comentario,
                FechaCreacion = comment.FechaCreacion,
                Autor = comment.Usuario.Name
            };
        }
    }
}
