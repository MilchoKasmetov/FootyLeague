using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootyLeague.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelCreationChange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Teams");
        }
    }
}
