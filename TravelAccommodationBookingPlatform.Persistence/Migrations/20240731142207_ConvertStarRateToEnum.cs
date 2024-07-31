using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConvertStarRateToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarRate_Rate",
                table: "Hotels",
                newName: "StarRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarRate",
                table: "Hotels",
                newName: "StarRate_Rate");
        }
    }
}
