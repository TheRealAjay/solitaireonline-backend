using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedFlipedToFlipped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fliped",
                table: "Cards",
                newName: "Flipped");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Flipped",
                table: "Cards",
                newName: "Fliped");
        }
    }
}
