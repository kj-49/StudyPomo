using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyPomo.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedParentIdToStudyTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "StudyTask",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "StudyTask");
        }
    }
}
