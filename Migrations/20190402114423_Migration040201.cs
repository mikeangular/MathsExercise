using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathsExercise.Migrations
{
    public partial class Migration040201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUIDValue = table.Column<string>(maxLength: 36, nullable: true),
                    Operations = table.Column<string>(maxLength: 4, nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    QuantityOfOperation = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SettingId = table.Column<int>(nullable: false),
                    Formula = table.Column<string>(maxLength: 500, nullable: true),
                    UserAnswer = table.Column<string>(maxLength: 20, nullable: true),
                    RightAnswer = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    SaveTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exercises_Setting_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Setting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_SettingId",
                table: "Exercises",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Setting");
        }
    }
}
