using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Users");
        }
    }
}
