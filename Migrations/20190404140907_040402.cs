using Microsoft.EntityFrameworkCore.Migrations;

namespace MathsExercise.Migrations
{
    public partial class _040402 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "systemLog",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "systemLog");
        }
    }
}
