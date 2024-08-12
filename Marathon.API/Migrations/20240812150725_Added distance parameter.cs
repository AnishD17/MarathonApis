using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathon.API.Migrations
{
    /// <inheritdoc />
    public partial class Addeddistanceparameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistanceInKm",
                table: "Races",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceInKm",
                table: "Races");
        }
    }
}
