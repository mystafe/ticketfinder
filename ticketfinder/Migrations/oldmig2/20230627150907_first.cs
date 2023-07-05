using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ticketfinder.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Events_EventId",
                table: "Tickets",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Events_EventId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets");
        }
    }
}
