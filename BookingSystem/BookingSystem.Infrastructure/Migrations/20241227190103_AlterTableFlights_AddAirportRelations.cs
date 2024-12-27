using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFlights_AddAirportRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartureAirportId",
                table: "Flights",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationAirportId",
                table: "Flights",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirportId",
                table: "Flights",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DepartureAirportId",
                table: "Flights",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId",
                principalTable: "Airports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DepartureAirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DestinationAirportId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DepartureAirportId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DestinationAirportId",
                table: "Flights");
        }
    }
}
