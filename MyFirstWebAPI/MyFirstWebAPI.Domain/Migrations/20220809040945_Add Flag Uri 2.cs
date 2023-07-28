using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstWebAPI.Domain.Migrations
{
    public partial class AddFlagUri2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlagUri",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagUri",
                table: "Countries");
        }
    }
}
