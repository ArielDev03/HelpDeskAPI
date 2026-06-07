using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Mappers.TicketsComments;
using HelpDeskAPI.Models.Tickets;

namespace HelpDeskAPI.Mappers.Tickets
{
    public static class TicketDetailMappers
    {
        public static TicketDetailDto ToTicketDetailDto(this Ticket ticketModel)
        {
            return new TicketDetailDto
            {
                Id = ticketModel.Id,
                Titulo = ticketModel.Titulo,
                Descripcion = ticketModel.Descripcion,
                EstadoId = ticketModel.Estado.Id,
                Estado = ticketModel.Estado.Nombre,
                PrioridadId = ticketModel.Prioridad?.Id,
                Prioridad = ticketModel.Prioridad?.Nombre,
                FechaCreacion = ticketModel.FechaCreacion,
                UsuarioId = ticketModel.Usuario.Id,
                Usuario = ticketModel.Usuario.Name,
                UsuarioAsignadoId = ticketModel.Usuario?.Id,
                UsuarioAsignado = ticketModel.Usuario?.Name ?? string.Empty,
                Comentarios = ticketModel.Comentarios
                .Select(c => c.ToTicketCommentDto())
                .ToList()
            };
        }
    }
}
