using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ticketfinder.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "EventSeats",
                newName: "EventPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventPrice",
                table: "EventSeats",
                newName: "Price");
        }
    }
}
