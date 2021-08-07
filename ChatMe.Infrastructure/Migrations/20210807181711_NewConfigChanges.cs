using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatMe.Infrastructure.Migrations
{
    public partial class NewConfigChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                schema: "ChatMe",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                schema: "ChatMe",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "ChatMe",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "ChatMe",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                schema: "ChatMe",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                schema: "ChatMe",
                table: "Messages",
                column: "UserId",
                principalSchema: "ChatMe",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
