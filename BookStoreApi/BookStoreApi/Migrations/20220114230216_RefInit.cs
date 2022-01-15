using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreApi.Migrations
{
    public partial class RefInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "BooksList");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BooksList",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksList",
                table: "BooksList",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NodesList",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodesList", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_NodesList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodesList_BooksList_BookId",
                        column: x => x.BookId,
                        principalTable: "BooksList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodesList_BookId",
                table: "NodesList",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodesList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksList",
                table: "BooksList");

            migrationBuilder.RenameTable(
                name: "BooksList",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "ID");
        }
    }
}
