using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatMe.Infrastructure.Migrations
{
    public partial class SeedingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ChatMe",
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("26f40fdf-8f92-4c4f-80c1-71090d86aef4"), "10edaa2ccab4a337894a84fae672038702658567", "Will" });

            migrationBuilder.InsertData(
                schema: "ChatMe",
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("1b0f8308-feb0-4d55-93ec-0765971e0bb7"), "10edaa2ccab4a337894a84fae672038702658567", "Test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ChatMe",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b0f8308-feb0-4d55-93ec-0765971e0bb7"));

            migrationBuilder.DeleteData(
                schema: "ChatMe",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("26f40fdf-8f92-4c4f-80c1-71090d86aef4"));
        }
    }
}
