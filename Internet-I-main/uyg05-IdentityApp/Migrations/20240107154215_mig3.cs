using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uyg05_IdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FileManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileManagers_CategoryId",
                table: "FileManagers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileManagers_Categories_CategoryId",
                table: "FileManagers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileManagers_Categories_CategoryId",
                table: "FileManagers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_FileManagers_CategoryId",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FileManagers");
        }
    }
}
