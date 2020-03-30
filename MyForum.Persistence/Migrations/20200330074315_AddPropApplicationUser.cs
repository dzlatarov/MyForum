using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Persistence.Migrations
{
    public partial class AddPropApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactivate",
                table: "AspNetUsers");
        }
    }
}
