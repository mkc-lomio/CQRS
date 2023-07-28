using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstWebAPI.Domain.Migrations
{
    public partial class UpdateEntityBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "Countries",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Countries",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Countries",
                newName: "ModifiedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Countries",
                newName: "CreatedUserId");
        }
    }
}
