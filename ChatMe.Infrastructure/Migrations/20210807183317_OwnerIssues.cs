using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatMe.Infrastructure.Migrations
{
    public partial class OwnerIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                schema: "ChatMe",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                schema: "ChatMe",
                table: "Messages");
        }
    }
}
