using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToshokanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedBy",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: 0);
        }
    }
}
