using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToshokanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCloudinaryUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LayoutUrl",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d1e90fbb-c731-4e2a-87b4-7c5d9ec4d65f"),
                columns: new[] { "AvatarUrl", "PurchasedBooks", "WishList" },
                values: new object[] { null, new List<Guid>(), new List<Guid>() });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LayoutUrl",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d1e90fbb-c731-4e2a-87b4-7c5d9ec4d65f"),
                columns: new[] { "PurchasedBooks", "WishList" },
                values: new object[] { new List<Guid>(), new List<Guid>() });
        }
    }
}
