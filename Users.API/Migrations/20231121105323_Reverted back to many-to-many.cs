using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Migrations
{
    /// <inheritdoc />
    public partial class Revertedbacktomanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkillsetId1",
                table: "Skillset",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skillset_SkillsetId1",
                table: "Skillset",
                column: "SkillsetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_Skillset_SkillsetId1",
                table: "Skillset",
                column: "SkillsetId1",
                principalTable: "Skillset",
                principalColumn: "SkillsetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_Skillset_SkillsetId1",
                table: "Skillset");

            migrationBuilder.DropIndex(
                name: "IX_Skillset_SkillsetId1",
                table: "Skillset");

            migrationBuilder.DropColumn(
                name: "SkillsetId1",
                table: "Skillset");
        }
    }
}
