using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeskReservationAPI.Migrations
{
    public partial class updatereservationentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationDateTime",
                table: "Reservations",
                newName: "ReservationStartDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationEndDateTime",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEndDateTime",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservationStartDateTime",
                table: "Reservations",
                newName: "ReservationDateTime");
        }
    }
}
