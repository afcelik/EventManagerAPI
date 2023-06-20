using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerAPI.Migrations
{
    public partial class eventPriceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EventPrice",
                table: "Events",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventPrice",
                table: "Events");
        }
    }
}
