using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedTestsTableInUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tests_TestId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TestId",
                table: "AspNetUsers",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tests_TestId",
                table: "AspNetUsers",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "test_id");
        }
    }
}
