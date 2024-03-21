using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCardFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardFormatID",
                table: "Field",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardFormat",
                columns: table => new
                {
                    CardFormatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFormat", x => x.CardFormatID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_CardFormatID",
                table: "Field",
                column: "CardFormatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_CardFormat_CardFormatID",
                table: "Field",
                column: "CardFormatID",
                principalTable: "CardFormat",
                principalColumn: "CardFormatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_CardFormat_CardFormatID",
                table: "Field");

            migrationBuilder.DropTable(
                name: "CardFormat");

            migrationBuilder.DropIndex(
                name: "IX_Field_CardFormatID",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "CardFormatID",
                table: "Field");
        }
    }
}
