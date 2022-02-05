using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyMission_DataManagement.Migrations
{
    public partial class Operations_History : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpsHists",
                columns: table => new
                {
                    operation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmi_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contract_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cac_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastupdatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpsHists", x => x.operation_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpsHists");
        }
    }
}
