using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Migrations
{
    /// <inheritdoc />
    public partial class manytomanyrevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_Skillset_SkillsetId1",
                table: "Skillset");

            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset");

            migrationBuilder.DropIndex(
                name: "IX_Skillset_SkillsetId1",
                table: "Skillset");

            migrationBuilder.DropIndex(
                name: "IX_Skillset_UserId",
                table: "Skillset");

            migrationBuilder.DropColumn(
                name: "SkillsetId1",
                table: "Skillset");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Skillset");

            migrationBuilder.CreateTable(
                name: "SkillsetUser",
                columns: table => new
                {
                    SkillsetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsetUser", x => new { x.SkillsetId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SkillsetUser_Skillset_SkillsetId",
                        column: x => x.SkillsetId,
                        principalTable: "Skillset",
                        principalColumn: "SkillsetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsetUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillsetUser_UserId",
                table: "SkillsetUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillsetUser");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillsetId1",
                table: "Skillset",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Skillset",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skillset_SkillsetId1",
                table: "Skillset",
                column: "SkillsetId1");

            migrationBuilder.CreateIndex(
                name: "IX_Skillset_UserId",
                table: "Skillset",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_Skillset_SkillsetId1",
                table: "Skillset",
                column: "SkillsetId1",
                principalTable: "Skillset",
                principalColumn: "SkillsetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
