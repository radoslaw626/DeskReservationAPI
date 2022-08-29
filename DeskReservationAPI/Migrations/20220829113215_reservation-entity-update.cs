using Microsoft.EntityFrameworkCore.Migrations;

namespace DeskReservationAPI.Migrations
{
    public partial class reservationentityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationStartDateTime",
                table: "Reservations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ReservationEndDateTime",
                table: "Reservations",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Reservations",
                newName: "ReservationStartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Reservations",
                newName: "ReservationEndDateTime");
        }
    }
}
