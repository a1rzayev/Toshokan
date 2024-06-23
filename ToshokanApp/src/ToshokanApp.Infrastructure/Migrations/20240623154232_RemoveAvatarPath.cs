using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToshokanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAvatarPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
