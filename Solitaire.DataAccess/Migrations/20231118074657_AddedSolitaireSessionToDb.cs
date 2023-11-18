using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSolitaireSessionToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Session_SessionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "AspNetUsers",
                newName: "SolitaireSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SessionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SolitaireSessionId");

            migrationBuilder.CreateTable(
                name: "SolitaireSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: null),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolitaireSessions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers",
                column: "SolitaireSessionId",
                principalTable: "SolitaireSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SolitaireSessions_SolitaireSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SolitaireSessions");

            migrationBuilder.RenameColumn(
                name: "SolitaireSessionId",
                table: "AspNetUsers",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SolitaireSessionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SessionId");

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Session_SessionId",
                table: "AspNetUsers",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
