using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalReads.API.Migrations
{
    /// <inheritdoc />
    public partial class UserIdLogAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LogActions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LogActions");
        }
    }
}
