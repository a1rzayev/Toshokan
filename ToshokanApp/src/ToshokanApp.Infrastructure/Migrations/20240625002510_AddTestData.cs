using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToshokanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "Role" },
                values: new object[] { new Guid("f3f02127-b696-42be-9426-8aa37761a9b5"), "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PurchasedBooks", "Surname", "WishList" },
                values: new object[] { new Guid("f3f02127-b696-42be-9426-8aa37761a9b5"), "admin.adminov@gmail.com", "Admin", "QWRtaW4xMjM0", "[]", "Adminov", "[]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UserId",
                keyValue: new Guid("f3f02127-b696-42be-9426-8aa37761a9b5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3f02127-b696-42be-9426-8aa37761a9b5"));
        }
    }
}
