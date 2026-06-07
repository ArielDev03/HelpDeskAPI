using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskAPI.Migrations
{
    /// <inheritdoc />
    public partial class agregarFkYnavegaciondeUserAModelTicketComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TicketComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_UsuarioId",
                table: "TicketComments",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Users_UsuarioId",
                table: "TicketComments",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Users_UsuarioId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_UsuarioId",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TicketComments");
        }
    }
}
