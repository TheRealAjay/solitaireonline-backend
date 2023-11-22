using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyBetweenCardAndSolitaireSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolitaireSessionId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SolitaireSessionId",
                table: "Cards",
                column: "SolitaireSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_SolitaireSessions_SolitaireSessionId",
                table: "Cards",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_SolitaireSessions_SolitaireSessionId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_SolitaireSessionId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "SolitaireSessionId",
                table: "Cards");
        }
    }
}
