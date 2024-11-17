using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SerialNumbercolumnhasadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SerialNumber",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Devices");
        }
    }
}
