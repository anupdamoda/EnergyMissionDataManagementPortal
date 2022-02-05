using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyMission_DataManagement.Migrations
{
    public partial class CACs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ordernumber",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymentreferencenumber",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CACs",
                columns: table => new
                {
                    CAC_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAC_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsedForContract = table.Column<bool>(type: "bit", nullable: false),
                    nmi_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CACs", x => x.CAC_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CACs");

            migrationBuilder.DropColumn(
                name: "ordernumber",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "paymentreferencenumber",
                table: "Contracts");
        }
    }
}
