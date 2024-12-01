using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class LastMessageDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessageDate",
                table: "Devices",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessageDate",
                table: "Devices");
        }
    }
}
