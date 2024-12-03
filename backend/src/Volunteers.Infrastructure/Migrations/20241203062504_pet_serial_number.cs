using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pet_serial_number : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "locations",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "photo",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "physical_parameters",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "requisites",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "LocationDetails",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoDetails",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalParametersDetails",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequisitesDetails",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "serial_number",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "PhotoDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "PhysicalParametersDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "RequisitesDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "serial_number",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "locations",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "physical_parameters",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "requisites",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
