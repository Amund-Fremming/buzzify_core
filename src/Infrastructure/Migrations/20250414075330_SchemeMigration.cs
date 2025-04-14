using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchemeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "buzzify");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    LastActive = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Auth0Id = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBase",
                schema: "buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniversalId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IterationCount = table.Column<int>(type: "integer", nullable: false),
                    CurrentIteration = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CreatorId = table.Column<int>(type: "integer", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SpinGame_Category = table.Column<int>(type: "integer", nullable: true),
                    SpinGame_State = table.Column<int>(type: "integer", nullable: true),
                    HubGroupName = table.Column<string>(type: "text", nullable: true),
                    HostId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBase_Users_HostId",
                        column: x => x.HostId,
                        principalSchema: "buzzify",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                schema: "buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ReadBeforeSpin = table.Column<bool>(type: "boolean", nullable: false),
                    SpinGameId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_GameBase_SpinGameId",
                        column: x => x.SpinGameId,
                        principalSchema: "buzzify",
                        principalTable: "GameBase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AskGameId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_GameBase_AskGameId",
                        column: x => x.AskGameId,
                        principalSchema: "buzzify",
                        principalTable: "GameBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpinPlayers",
                schema: "buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpinPlayers_GameBase_GameId",
                        column: x => x.GameId,
                        principalSchema: "buzzify",
                        principalTable: "GameBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpinPlayers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "buzzify",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_SpinGameId",
                schema: "buzzify",
                table: "Challenges",
                column: "SpinGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBase_HostId",
                schema: "buzzify",
                table: "GameBase",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AskGameId",
                schema: "buzzify",
                table: "Questions",
                column: "AskGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinPlayers_GameId",
                schema: "buzzify",
                table: "SpinPlayers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinPlayers_UserId_GameId",
                schema: "buzzify",
                table: "SpinPlayers",
                columns: new[] { "UserId", "GameId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenges",
                schema: "buzzify");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "buzzify");

            migrationBuilder.DropTable(
                name: "SpinPlayers",
                schema: "buzzify");

            migrationBuilder.DropTable(
                name: "GameBase",
                schema: "buzzify");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "buzzify");
        }
    }
}
