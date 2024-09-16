using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointment_Api.Migrations
{
    /// <inheritdoc />
    public partial class truckmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trucks_TruckId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TruckId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TruckId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TruckId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TruckId",
                table: "Appointments",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Trucks_TruckId",
                table: "Appointments",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
