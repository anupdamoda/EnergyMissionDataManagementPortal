using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyMission_DataManagement.Migrations
{
    public partial class lastupdatedby_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastupdatedby",
                table: "NMIs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastupdatedby",
                table: "NMIs");
        }
    }
}
