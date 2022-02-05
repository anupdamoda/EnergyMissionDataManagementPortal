using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyMission_DataManagement.Migrations
{
    public partial class last_updated_by_cac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastupdatedby",
                table: "CACs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastupdatedby",
                table: "CACs");
        }
    }
}
