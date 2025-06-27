using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalReads.API.Migrations
{
    /// <inheritdoc />
    public partial class RatingFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Favorites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Favorites");
        }
    }
}
