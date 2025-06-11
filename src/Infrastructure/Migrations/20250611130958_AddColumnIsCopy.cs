using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIsCopy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOriginal",
                schema: "Beer",
                table: "SpinGame",
                newName: "IsCopy");

            migrationBuilder.RenameIndex(
                name: "IX_SpinGame_Id_IsOriginal",
                schema: "Beer",
                table: "SpinGame",
                newName: "IX_SpinGame_Id_IsCopy");

            migrationBuilder.RenameColumn(
                name: "IsOriginal",
                schema: "Beer",
                table: "AskGame",
                newName: "IsCopy");

            migrationBuilder.RenameIndex(
                name: "IX_AskGame_Id_IsOriginal",
                schema: "Beer",
                table: "AskGame",
                newName: "IX_AskGame_Id_IsCopy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCopy",
                schema: "Beer",
                table: "SpinGame",
                newName: "IsOriginal");

            migrationBuilder.RenameIndex(
                name: "IX_SpinGame_Id_IsCopy",
                schema: "Beer",
                table: "SpinGame",
                newName: "IX_SpinGame_Id_IsOriginal");

            migrationBuilder.RenameColumn(
                name: "IsCopy",
                schema: "Beer",
                table: "AskGame",
                newName: "IsOriginal");

            migrationBuilder.RenameIndex(
                name: "IX_AskGame_Id_IsCopy",
                schema: "Beer",
                table: "AskGame",
                newName: "IX_AskGame_Id_IsOriginal");
        }
    }
}
