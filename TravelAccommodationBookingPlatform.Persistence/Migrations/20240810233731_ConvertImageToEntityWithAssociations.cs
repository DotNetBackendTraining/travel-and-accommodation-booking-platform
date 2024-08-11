using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConvertImageToEntityWithAssociations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotels_Images");

            migrationBuilder.DropTable(
                name: "Rooms_Images");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage_Url",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage_Url",
                table: "Cities");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailImageId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailImageId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelImageAssociation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImageAssociation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelImageAssociation_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HotelImageAssociation_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomImageAssociation",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImageAssociation", x => new { x.ImageId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_RoomImageAssociation_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomImageAssociation_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ThumbnailImageId",
                table: "Hotels",
                column: "ThumbnailImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ThumbnailImageId",
                table: "Cities",
                column: "ThumbnailImageId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImageAssociation_HotelId",
                table: "HotelImageAssociation",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImageAssociation_ImageId",
                table: "HotelImageAssociation",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImageAssociation_ImageId",
                table: "RoomImageAssociation",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImageAssociation_RoomId",
                table: "RoomImageAssociation",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Images_ThumbnailImageId",
                table: "Cities",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Images_ThumbnailImageId",
                table: "Hotels",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Images_ThumbnailImageId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Images_ThumbnailImageId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelImageAssociation");

            migrationBuilder.DropTable(
                name: "RoomImageAssociation");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ThumbnailImageId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ThumbnailImageId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageId",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage_Url",
                table: "Hotels",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage_Url",
                table: "Cities",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Hotels_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Images_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Images_Rooms_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_Images_HotelId",
                table: "Hotels_Images",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Images_HotelId",
                table: "Rooms_Images",
                column: "HotelId");
        }
    }
}
