using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCityFieldsAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId1",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_Name",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostOffice_Address",
                table: "Cities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostOffice_Description",
                table: "Cities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage_Url",
                table: "Cities",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId1",
                table: "Hotels",
                column: "CityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Cities_CityId1",
                table: "Hotels",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Cities_CityId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CityId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Country_Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PostOffice_Address",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PostOffice_Description",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage_Url",
                table: "Cities");
        }
    }
}
