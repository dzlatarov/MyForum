using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Persistence.Migrations
{
    public partial class AddCreatorInNewsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "News",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatorId",
                table: "News",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_CreatorId",
                table: "News",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_CreatorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CreatorId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "News");
        }
    }
}
