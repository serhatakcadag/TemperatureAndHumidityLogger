using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore.Migrations
{
    public partial class LogIdTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            // Step 2: Drop the old Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Logs");

            // Step 3: Add the new Id column with the desired type (int) and identity property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Logs",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            // Step 4: Recreate the primary key constraint on the new column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            // Step 2: Drop the new Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Logs");

            // Step 3: Add the old Id column back with the original type (Guid)
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Logs",
                nullable: false);

            // Step 4: Recreate the primary key constraint on the old column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");
        }
    }
}
