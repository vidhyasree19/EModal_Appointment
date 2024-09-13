using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointment_Api.Migrations
{
    /// <inheritdoc />
    public partial class TerminalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerName",
                table: "Terminals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Terminals",
                newName: "GateNo");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Terminals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Slots",
                table: "Terminals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Terminals");

            migrationBuilder.DropColumn(
                name: "Slots",
                table: "Terminals");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Terminals",
                newName: "ManagerName");

            migrationBuilder.RenameColumn(
                name: "GateNo",
                table: "Terminals",
                newName: "Location");
        }
    }
}
