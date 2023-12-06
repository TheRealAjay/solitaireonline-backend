using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPostitionToPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Postition",
                table: "Cards",
                newName: "Position");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Cards",
                newName: "Postition");
        }
    }
}
