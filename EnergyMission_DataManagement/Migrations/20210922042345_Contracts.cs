using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyMission_DataManagement.Migrations
{
    public partial class Contracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nmi_number",
                table: "NMIs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    contract_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nmi_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jurisdiction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    metertype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startofdelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    meterserialno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.contract_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "nmi_number",
                table: "NMIs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
