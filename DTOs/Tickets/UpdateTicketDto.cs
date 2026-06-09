namespace HelpDeskAPI.DTOs.Tickets
{
    public class UpdateTicketDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int EstadoId { get; set; }

        public int PrioridadId { get; set; }

        public int? UsuarioAsignadoId { get; set; }
    }
}
