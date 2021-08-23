using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreCRUD.Migrations
{
    public partial class insertion_urls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "Urls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "Urls");
        }
    }
}
