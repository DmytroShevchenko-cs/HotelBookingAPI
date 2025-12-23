using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddress_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_city_country_country_id",
                table: "city");

            migrationBuilder.DropForeignKey(
                name: "fk_hotels_city_city_id",
                table: "hotels");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropPrimaryKey(
                name: "pk_city",
                table: "city");

            migrationBuilder.DropIndex(
                name: "ix_city_country_id",
                table: "city");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "city");

            migrationBuilder.RenameTable(
                name: "city",
                newName: "cities");

            migrationBuilder.RenameIndex(
                name: "ix_city_name",
                table: "cities",
                newName: "ix_cities_name");

            migrationBuilder.AddColumn<int>(
                name: "room_number",
                table: "rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_cities",
                table: "cities",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_hotels_cities_city_id",
                table: "hotels",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            
            var now = DateTimeOffset.UtcNow;

            migrationBuilder.InsertData(
            table: "cities",
            columns: new[] { "id", "name", "created_at" },
            values: new object[,]
            {
                { 1, "Kyiv", now },
                { 2, "Vinnytsia", now },
                { 3, "Zhmerynka", now },
                { 4, "Khmilnyk", now },
                { 5, "Lutsk", now },
                { 6, "Kovel", now },
                { 7, "Novovolynsk", now },
                { 8, "Dnipro", now },
                { 9, "Kryvyi Rih", now },
                { 10, "Kamianske", now },
                { 11, "Pavlohrad", now },
                { 12, "Nikopol", now },
                { 13, "Donetsk", now },
                { 14, "Mariupol", now },
                { 15, "Kramatorsk", now },
                { 16, "Sloviansk", now },
                { 17, "Zhytomyr", now },
                { 18, "Berdychiv", now },
                { 19, "Korosten", now },
                { 20, "Uzhhorod", now },
                { 21, "Mukachevo", now },
                { 22, "Khust", now },
                { 23, "Zaporizhzhia", now },
                { 24, "Melitopol", now },
                { 25, "Berdiansk", now },
                { 26, "Ivano-Frankivsk", now },
                { 27, "Kalush", now },
                { 28, "Kolomyia", now },
                { 29, "Bila Tserkva", now },
                { 30, "Boryspil", now },
                { 31, "Irpin", now },
                { 32, "Bucha", now },
                { 33, "Fastiv", now },
                { 34, "Kropyvnytskyi", now },
                { 35, "Oleksandriia", now },
                { 36, "Luhansk", now },
                { 37, "Severodonetsk", now },
                { 38, "Lysychansk", now },
                { 39, "Lviv", now },
                { 40, "Drohobych", now },
                { 41, "Stryi", now },
                { 42, "Truskavets", now },
                { 43, "Mykolaiv", now },
                { 44, "Pervomaisk", now },
                { 45, "Odesa", now },
                { 46, "Izmail", now },
                { 47, "Bilhorod-Dnistrovskyi", now },
                { 48, "Chornomorsk", now },
                { 49, "Poltava", now },
                { 50, "Kremenchuk", now },
                { 51, "Lubny", now },
                { 52, "Rivne", now },
                { 53, "Dubno", now },
                { 54, "Sumy", now },
                { 55, "Konotop", now },
                { 56, "Shostka", now },
                { 57, "Ternopil", now },
                { 58, "Chortkiv", now },
                { 59, "Kharkiv", now },
                { 60, "Lozova", now },
                { 61, "Izium", now },
                { 62, "Kherson", now },
                { 63, "Nova Kakhovka", now },
                { 64, "Skadovsk", now },
                { 65, "Khmelnytskyi", now },
                { 66, "Kamianets-Podilskyi", now },
                { 67, "Cherkasy", now },
                { 68, "Uman", now },
                { 69, "Smila", now },
                { 70, "Chernihiv", now },
                { 71, "Nizhyn", now },
                { 72, "Pryluky", now },
                { 73, "Chernivtsi", now },
                { 74, "Novodnistrovsk", now }
            });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_hotels_cities_city_id",
                table: "hotels");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cities",
                table: "cities");

            migrationBuilder.DropColumn(
                name: "room_number",
                table: "rooms");

            migrationBuilder.RenameTable(
                name: "cities",
                newName: "city");

            migrationBuilder.RenameIndex(
                name: "ix_cities_name",
                table: "city",
                newName: "ix_city_name");

            migrationBuilder.AddColumn<int>(
                name: "country_id",
                table: "city",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_city",
                table: "city",
                column: "id");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_city_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_country_name",
                table: "country",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_city_country_country_id",
                table: "city",
                column: "country_id",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hotels_city_city_id",
                table: "hotels",
                column: "city_id",
                principalTable: "city",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValues: new object[]
                {
                    1,2,3,4,5,6,7,8,9,10,
                    11,12,13,14,15,16,17,18,19,20,
                    21,22,23,24,25,26,27,28,29,30,
                    31,32,33,34,35,36,37,38,39,40,
                    41,42,43,44,45,46,47,48,49,50,
                    51,52,53,54,55,56,57,58,59,60
                });
        }
    }
}
