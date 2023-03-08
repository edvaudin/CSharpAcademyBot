using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademyBot.Migrations
{
    /// <inheritdoc />
    public partial class ModelNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReputations");

            migrationBuilder.CreateTable(
                name: "UserReputation",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReputation", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserReputation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReputation");

            migrationBuilder.CreateTable(
                name: "UserReputations",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReputations", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserReputations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
