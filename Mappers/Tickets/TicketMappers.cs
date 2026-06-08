using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.Models.Tickets;


namespace HelpDeskAPI.Mappers.Tickets
{
    public static class TicketMappers
    {
        public static TicketDto ToTicketDto(this Ticket ticketModel)
        {
            return new TicketDto
            {
                Id = ticketModel.Id,
                Titulo = ticketModel.Titulo,
                Estado = ticketModel.Estado.Nombre,
                Prioridad = ticketModel.Prioridad.Nombre,
                FechaCreacion = ticketModel.FechaCreacion,
                Usuario = ticketModel.Usuario.Name,
                UsuarioAsignado = ticketModel.UsuarioAsignado?.Name ?? string.Empty
            };
        }
    }
}
