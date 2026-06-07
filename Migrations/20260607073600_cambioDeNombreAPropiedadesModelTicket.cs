using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskAPI.Migrations
{
    /// <inheritdoc />
    public partial class cambioDeNombreAPropiedadesModelTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketPriorities_TicketPriorityId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketStatuses_TicketStatusId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketStatusId",
                table: "Tickets",
                newName: "PrioridadId");

            migrationBuilder.RenameColumn(
                name: "TicketPriorityId",
                table: "Tickets",
                newName: "EstadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketStatusId",
                table: "Tickets",
                newName: "IX_Tickets_PrioridadId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketPriorityId",
                table: "Tickets",
                newName: "IX_Tickets_EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketPriorities_PrioridadId",
                table: "Tickets",
                column: "PrioridadId",
                principalTable: "TicketPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketStatuses_EstadoId",
                table: "Tickets",
                column: "EstadoId",
                principalTable: "TicketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketPriorities_PrioridadId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketStatuses_EstadoId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "PrioridadId",
                table: "Tickets",
                newName: "TicketStatusId");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Tickets",
                newName: "TicketPriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PrioridadId",
                table: "Tickets",
                newName: "IX_Tickets_TicketStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_EstadoId",
                table: "Tickets",
                newName: "IX_Tickets_TicketPriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketPriorities_TicketPriorityId",
                table: "Tickets",
                column: "TicketPriorityId",
                principalTable: "TicketPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketStatuses_TicketStatusId",
                table: "Tickets",
                column: "TicketStatusId",
                principalTable: "TicketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
