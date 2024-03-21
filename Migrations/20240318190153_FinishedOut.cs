using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class FinishedOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Job");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Job",
                type: "bit",
                nullable: true);
        }
    }
}
