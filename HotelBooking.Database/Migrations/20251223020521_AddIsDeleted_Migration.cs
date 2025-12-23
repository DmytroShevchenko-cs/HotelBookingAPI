using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "rooms",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "hotels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "hotels");
        }
    }
}
