using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePricePreNightToPricePerHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_pre_night",
                table: "rooms",
                newName: "price_per_hour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_per_hour",
                table: "rooms",
                newName: "price_pre_night");
        }
    }
}
