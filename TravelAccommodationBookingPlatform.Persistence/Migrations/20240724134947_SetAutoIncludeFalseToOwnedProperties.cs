using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetAutoIncludeFalseToOwnedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Images_Rooms_RoomId",
                table: "Rooms_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms_Images",
                table: "Rooms_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels_Images",
                table: "Hotels_Images");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Rooms_Images",
                newName: "HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms_Images",
                table: "Rooms_Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels_Images",
                table: "Hotels_Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Images_HotelId",
                table: "Rooms_Images",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_HotelId",
                table: "Review",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_Images_HotelId",
                table: "Hotels_Images",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Images_Rooms_HotelId",
                table: "Rooms_Images",
                column: "HotelId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Images_Rooms_HotelId",
                table: "Rooms_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms_Images",
                table: "Rooms_Images");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Images_HotelId",
                table: "Rooms_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_HotelId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels_Images",
                table: "Hotels_Images");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_Images_HotelId",
                table: "Hotels_Images");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Rooms_Images",
                newName: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms_Images",
                table: "Rooms_Images",
                columns: new[] { "RoomId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                columns: new[] { "HotelId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels_Images",
                table: "Hotels_Images",
                columns: new[] { "HotelId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Images_Rooms_RoomId",
                table: "Rooms_Images",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
