using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncApp.Migrations
{
    public partial class IdentityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PetFriendly",
                table: "Hotels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Hotels",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "Hotels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<long>(nullable: false),
                    RoomId = table.Column<long>(nullable: false),
                    RoomNumber = table.Column<long>(nullable: false),
                    Rate = table.Column<decimal>(type: "money", nullable: false),
                    PetFriendly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_RoomId",
                table: "HotelRooms",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "PetFriendly",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Hotels");
        }
    }
}
