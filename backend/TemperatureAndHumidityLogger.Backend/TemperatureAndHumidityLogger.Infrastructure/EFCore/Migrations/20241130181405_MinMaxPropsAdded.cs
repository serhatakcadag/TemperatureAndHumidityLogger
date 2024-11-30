using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class MinMaxPropsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MaxHumidity",
                table: "Devices",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxTemperature",
                table: "Devices",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinHumidity",
                table: "Devices",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinTemperature",
                table: "Devices",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MaxTemperature",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MinHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MinTemperature",
                table: "Devices");
        }
    }
}
