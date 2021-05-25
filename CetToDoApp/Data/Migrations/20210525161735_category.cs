using Microsoft.EntityFrameworkCore.Migrations;

namespace CetToDoApp.Data.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categorites",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categorites");
        }
    }
}
