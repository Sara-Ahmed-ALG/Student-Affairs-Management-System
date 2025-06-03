using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixnavigationpropfCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_CourseId",
                table: "LectureSchedules",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Courses_CourseId",
                table: "LectureSchedules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Courses_CourseId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_CourseId",
                table: "LectureSchedules");
        }
    }
}
