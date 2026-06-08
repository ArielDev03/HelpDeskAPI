using System.Runtime.CompilerServices;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.DTOs.Tickets;

namespace HelpDeskAPI.Mappers.Tickets
{
    public static class CreateTicketMappers
    {
        public static  Ticket CreateTicketDto(this CreateTicketDto ticketDto)
        {
            return new Ticket
            {
                Titulo = ticketDto.Titulo,
                Descripcion = ticketDto.Descripcion,
                EstadoId = 1,
                PrioridadId = 1,
                UsuarioId = ticketDto.UsuarioId,
                FechaCreacion = DateTime.UtcNow
            };
        }
    }
}
