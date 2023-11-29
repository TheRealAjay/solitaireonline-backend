using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdaptedForeignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_SolitaireSession_SolitaireSessionId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Draw_SolitaireSession_SolitaireSessionId",
                table: "Draw");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SolitaireSession_SolitaireSessionId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolitaireSession",
                table: "SolitaireSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Draw",
                table: "Draw");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.RenameTable(
                name: "SolitaireSession",
                newName: "SolitaireSessions");

            migrationBuilder.RenameTable(
                name: "Draw",
                newName: "Draws");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_Draw_SolitaireSessionId",
                table: "Draws",
                newName: "IX_Draws_SolitaireSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_SolitaireSessionId",
                table: "Cards",
                newName: "IX_Cards_SolitaireSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolitaireSessions",
                table: "SolitaireSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Draws",
                table: "Draws",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_SolitaireSessions_SolitaireSessionId",
                table: "Cards",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Draws_SolitaireSessions_SolitaireSessionId",
                table: "Draws",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SolitaireSessions_SolitaireSessionId",
                table: "Users",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_SolitaireSessions_SolitaireSessionId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Draws_SolitaireSessions_SolitaireSessionId",
                table: "Draws");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SolitaireSessions_SolitaireSessionId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolitaireSessions",
                table: "SolitaireSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Draws",
                table: "Draws");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "SolitaireSessions",
                newName: "SolitaireSession");

            migrationBuilder.RenameTable(
                name: "Draws",
                newName: "Draw");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameIndex(
                name: "IX_Draws_SolitaireSessionId",
                table: "Draw",
                newName: "IX_Draw_SolitaireSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_SolitaireSessionId",
                table: "Card",
                newName: "IX_Card_SolitaireSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolitaireSession",
                table: "SolitaireSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Draw",
                table: "Draw",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_SolitaireSession_SolitaireSessionId",
                table: "Card",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Draw_SolitaireSession_SolitaireSessionId",
                table: "Draw",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SolitaireSession_SolitaireSessionId",
                table: "Users",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSession",
                principalColumn: "Id");
        }
    }
}
