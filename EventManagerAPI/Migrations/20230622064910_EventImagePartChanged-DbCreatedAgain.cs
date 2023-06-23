using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerAPI.Migrations
{
    public partial class EventImagePartChangedDbCreatedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventImage1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImage2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImage3",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "EventImageUrlOne",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventImageUrlThree",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventImageUrlTwo",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventImageUrlOne",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImageUrlThree",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImageUrlTwo",
                table: "Events");

            migrationBuilder.AddColumn<byte[]>(
                name: "EventImage1",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "EventImage2",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "EventImage3",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
