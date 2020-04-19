using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Persistence.Migrations
{
    public partial class AddQuotePropertyToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quote",
                table: "Comments");
        }
    }
}
