using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixdIdStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_AppUserId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AppUserId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId1",
                table: "Students",
                column: "AppUserId1",
                unique: true,
                filter: "[AppUserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_AppUserId1",
                table: "Students",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
