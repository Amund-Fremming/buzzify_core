using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GameBaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HubGroupName",
                schema: "Beer",
                table: "SpinGame");

            migrationBuilder.AddColumn<bool>(
                name: "IsOriginal",
                schema: "Beer",
                table: "SpinGame",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOriginal",
                schema: "Beer",
                table: "AskGame",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SpinGame_Id_IsOriginal",
                schema: "Beer",
                table: "SpinGame",
                columns: new[] { "Id", "IsOriginal" });

            migrationBuilder.CreateIndex(
                name: "IX_AskGame_Id_IsOriginal",
                schema: "Beer",
                table: "AskGame",
                columns: new[] { "Id", "IsOriginal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SpinGame_Id_IsOriginal",
                schema: "Beer",
                table: "SpinGame");

            migrationBuilder.DropIndex(
                name: "IX_AskGame_Id_IsOriginal",
                schema: "Beer",
                table: "AskGame");

            migrationBuilder.DropColumn(
                name: "IsOriginal",
                schema: "Beer",
                table: "SpinGame");

            migrationBuilder.DropColumn(
                name: "IsOriginal",
                schema: "Beer",
                table: "AskGame");

            migrationBuilder.AddColumn<string>(
                name: "HubGroupName",
                schema: "Beer",
                table: "SpinGame",
                type: "text",
                nullable: true);
        }
    }
}
