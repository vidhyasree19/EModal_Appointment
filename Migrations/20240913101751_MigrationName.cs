using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointment_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chassis",
                table: "TruckingCompanies");

            migrationBuilder.DropColumn(
                name: "Driver_Name",
                table: "TruckingCompanies");

            migrationBuilder.DropColumn(
                name: "Truck_No",
                table: "TruckingCompanies");

            migrationBuilder.RenameColumn(
                name: "TruckingCompany_Name",
                table: "TruckingCompanies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Container_Size",
                table: "TruckingCompanies",
                newName: "WorkType");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckingCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_TruckingCompanies_TruckingCompanyId",
                        column: x => x.TruckingCompanyId,
                        principalTable: "TruckingCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChasis = table.Column<bool>(type: "bit", nullable: false),
                    TruckingCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_TruckingCompanies_TruckingCompanyId",
                        column: x => x.TruckingCompanyId,
                        principalTable: "TruckingCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckingCompanyId",
                table: "Drivers",
                column: "TruckingCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TruckingCompanyId",
                table: "Trucks",
                column: "TruckingCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.RenameColumn(
                name: "WorkType",
                table: "TruckingCompanies",
                newName: "Container_Size");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TruckingCompanies",
                newName: "TruckingCompany_Name");

            migrationBuilder.AddColumn<bool>(
                name: "Chassis",
                table: "TruckingCompanies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Driver_Name",
                table: "TruckingCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Truck_No",
                table: "TruckingCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
