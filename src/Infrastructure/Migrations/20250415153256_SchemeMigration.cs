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
                name: "Buzzify");

            migrationBuilder.CreateTable(
                name: "AskGame",
                schema: "Buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UniversalId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IterationCount = table.Column<int>(type: "integer", nullable: false),
                    CurrentIteration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AskGame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Buzzify",
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "Buzzify",
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
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_AskGame_AskGameId",
                        column: x => x.AskGameId,
                        principalSchema: "Buzzify",
                        principalTable: "AskGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpinGame",
                schema: "Buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    HubGroupName = table.Column<string>(type: "text", nullable: true),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    UniversalId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IterationCount = table.Column<int>(type: "integer", nullable: false),
                    CurrentIteration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpinGame_User_HostId",
                        column: x => x.HostId,
                        principalSchema: "Buzzify",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenge",
                schema: "Buzzify",
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
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenge_SpinGame_SpinGameId",
                        column: x => x.SpinGameId,
                        principalSchema: "Buzzify",
                        principalTable: "SpinGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpinPlayer",
                schema: "Buzzify",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpinPlayer_SpinGame_GameId",
                        column: x => x.GameId,
                        principalSchema: "Buzzify",
                        principalTable: "SpinGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpinPlayer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Buzzify",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_SpinGameId",
                schema: "Buzzify",
                table: "Challenge",
                column: "SpinGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AskGameId",
                schema: "Buzzify",
                table: "Question",
                column: "AskGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinGame_HostId",
                schema: "Buzzify",
                table: "SpinGame",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinPlayer_GameId",
                schema: "Buzzify",
                table: "SpinPlayer",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinPlayer_UserId_GameId",
                schema: "Buzzify",
                table: "SpinPlayer",
                columns: new[] { "UserId", "GameId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenge",
                schema: "Buzzify");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "Buzzify");

            migrationBuilder.DropTable(
                name: "SpinPlayer",
                schema: "Buzzify");

            migrationBuilder.DropTable(
                name: "AskGame",
                schema: "Buzzify");

            migrationBuilder.DropTable(
                name: "SpinGame",
                schema: "Buzzify");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Buzzify");
        }
    }
}
