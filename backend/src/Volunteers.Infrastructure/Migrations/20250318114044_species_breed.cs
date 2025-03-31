using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class species_breed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "species_breed",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "SpeciesBreed",
                table: "pets",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpeciesBreed",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "species_breed",
                table: "pets",
                type: "text",
                nullable: true);
        }
    }
}
