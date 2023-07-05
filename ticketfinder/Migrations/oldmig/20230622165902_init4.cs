using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ticketfinder.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSeats_Events_EventId",
                table: "EventSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSeats_EventStages_EventStageId",
                table: "EventSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_EventStages_Events_EventId",
                table: "EventStages");

            migrationBuilder.DropIndex(
                name: "IX_EventSeats_EventId",
                table: "EventSeats");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventStages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventStageId",
                table: "EventSeats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSeats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSeats_EventStages_EventStageId",
                table: "EventSeats",
                column: "EventStageId",
                principalTable: "EventStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventStages_Events_EventId",
                table: "EventStages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSeats_EventStages_EventStageId",
                table: "EventSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_EventStages_Events_EventId",
                table: "EventStages");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventStages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventStageId",
                table: "EventSeats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSeats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_EventId",
                table: "EventSeats",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSeats_Events_EventId",
                table: "EventSeats",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSeats_EventStages_EventStageId",
                table: "EventSeats",
                column: "EventStageId",
                principalTable: "EventStages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventStages_Events_EventId",
                table: "EventStages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
