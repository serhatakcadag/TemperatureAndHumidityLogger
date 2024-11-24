using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SerialNumberUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Devices_SerialNumber",
                table: "Devices",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_SerialNumber",
                table: "Devices");
        }
    }
}
