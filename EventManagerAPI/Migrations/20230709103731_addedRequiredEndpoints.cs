using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerAPI.Migrations
{
    public partial class addedRequiredEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceName",
                table: "Events");
        }
    }
}
