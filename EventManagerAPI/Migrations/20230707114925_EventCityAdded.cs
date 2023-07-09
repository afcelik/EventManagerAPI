using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerAPI.Migrations
{
    public partial class EventCityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventCity",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCity",
                table: "Events");
        }
    }
}
