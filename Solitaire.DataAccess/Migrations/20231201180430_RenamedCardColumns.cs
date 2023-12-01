using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCardColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Suit",
                table: "Cards",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "Cards",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Cards",
                newName: "Suit");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Cards",
                newName: "Rank");
        }
    }
}
