using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.PaymentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablePaymentStatuses_AddColumn_FlightId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "PaymentStatuses",
                newName: "PaymentId");

            migrationBuilder.AddColumn<Guid>(
                name: "FlightId",
                table: "PaymentStatuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "PaymentStatuses");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "PaymentStatuses",
                newName: "Message");
        }
    }
}
