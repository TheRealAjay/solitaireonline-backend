using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SolitairSessionIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SolitaireSessionId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SolitaireSessionId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
