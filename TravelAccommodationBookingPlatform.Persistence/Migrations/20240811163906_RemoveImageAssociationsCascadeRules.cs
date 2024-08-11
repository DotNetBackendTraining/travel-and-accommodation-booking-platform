using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageAssociationsCascadeRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImageAssociation_Hotels_HotelId",
                table: "HotelImageAssociation");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImageAssociation_Images_ImageId",
                table: "HotelImageAssociation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImageAssociation_Images_ImageId",
                table: "RoomImageAssociation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImageAssociation_Rooms_RoomId",
                table: "RoomImageAssociation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomImageAssociation",
                table: "RoomImageAssociation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImageAssociation",
                table: "HotelImageAssociation");

            migrationBuilder.RenameTable(
                name: "RoomImageAssociation",
                newName: "RoomImageAssociations");

            migrationBuilder.RenameTable(
                name: "HotelImageAssociation",
                newName: "HotelImageAssociations");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImageAssociation_RoomId",
                table: "RoomImageAssociations",
                newName: "IX_RoomImageAssociations_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImageAssociation_ImageId",
                table: "RoomImageAssociations",
                newName: "IX_RoomImageAssociations_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImageAssociation_ImageId",
                table: "HotelImageAssociations",
                newName: "IX_HotelImageAssociations_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImageAssociation_HotelId",
                table: "HotelImageAssociations",
                newName: "IX_HotelImageAssociations_HotelId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RoomImageAssociations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomImageAssociations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RoomImageAssociations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomImageAssociations",
                table: "RoomImageAssociations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImageAssociations",
                table: "HotelImageAssociations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImageAssociations_Hotels_HotelId",
                table: "HotelImageAssociations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImageAssociations_Images_ImageId",
                table: "HotelImageAssociations",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImageAssociations_Images_ImageId",
                table: "RoomImageAssociations",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImageAssociations_Rooms_RoomId",
                table: "RoomImageAssociations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImageAssociations_Hotels_HotelId",
                table: "HotelImageAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImageAssociations_Images_ImageId",
                table: "HotelImageAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImageAssociations_Images_ImageId",
                table: "RoomImageAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImageAssociations_Rooms_RoomId",
                table: "RoomImageAssociations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomImageAssociations",
                table: "RoomImageAssociations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImageAssociations",
                table: "HotelImageAssociations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomImageAssociations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomImageAssociations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RoomImageAssociations");

            migrationBuilder.RenameTable(
                name: "RoomImageAssociations",
                newName: "RoomImageAssociation");

            migrationBuilder.RenameTable(
                name: "HotelImageAssociations",
                newName: "HotelImageAssociation");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImageAssociations_RoomId",
                table: "RoomImageAssociation",
                newName: "IX_RoomImageAssociation_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImageAssociations_ImageId",
                table: "RoomImageAssociation",
                newName: "IX_RoomImageAssociation_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImageAssociations_ImageId",
                table: "HotelImageAssociation",
                newName: "IX_HotelImageAssociation_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImageAssociations_HotelId",
                table: "HotelImageAssociation",
                newName: "IX_HotelImageAssociation_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomImageAssociation",
                table: "RoomImageAssociation",
                columns: new[] { "ImageId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImageAssociation",
                table: "HotelImageAssociation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImageAssociation_Hotels_HotelId",
                table: "HotelImageAssociation",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImageAssociation_Images_ImageId",
                table: "HotelImageAssociation",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImageAssociation_Images_ImageId",
                table: "RoomImageAssociation",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImageAssociation_Rooms_RoomId",
                table: "RoomImageAssociation",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
