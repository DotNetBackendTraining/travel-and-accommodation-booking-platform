using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountAndPaymentFieldsAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Payment_PaymentId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Hotels_HotelId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.RenameIndex(
                name: "IX_Discount_HotelId",
                table: "Discounts",
                newName: "IX_Discounts_HotelId");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveDiscountId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppliedDiscountId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationNumber",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalInformation_FullName",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalInformation_PhoneNumber",
                table: "Payments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rate_Percentage",
                table: "Discounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ActiveDiscountId",
                table: "Hotels",
                column: "ActiveDiscountId",
                unique: true,
                filter: "[ActiveDiscountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppliedDiscountId",
                table: "Payments",
                column: "AppliedDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Hotels_HotelId",
                table: "Discounts",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Discounts_ActiveDiscountId",
                table: "Hotels",
                column: "ActiveDiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Discounts_AppliedDiscountId",
                table: "Payments",
                column: "AppliedDiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Hotels_HotelId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Discounts_ActiveDiscountId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Discounts_AppliedDiscountId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ActiveDiscountId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AppliedDiscountId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ActiveDiscountId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "AppliedDiscountId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PersonalInformation_FullName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PersonalInformation_PhoneNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Rate_Percentage",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_HotelId",
                table: "Discount",
                newName: "IX_Discount_HotelId");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "Discount",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Payment_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Hotels_HotelId",
                table: "Discount",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
