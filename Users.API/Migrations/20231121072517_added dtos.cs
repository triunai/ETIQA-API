using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Migrations
{
    /// <inheritdoc />
    public partial class addeddtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillsets_Users_UserId",
                table: "Skillsets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skillsets",
                table: "Skillsets");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Skillsets",
                newName: "Skillset");

            migrationBuilder.RenameIndex(
                name: "IX_Skillsets_UserId",
                table: "Skillset",
                newName: "IX_Skillset_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skillset",
                table: "Skillset",
                column: "SkillsetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skillset",
                table: "Skillset");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Skillset",
                newName: "Skillsets");

            migrationBuilder.RenameIndex(
                name: "IX_Skillset_UserId",
                table: "Skillsets",
                newName: "IX_Skillsets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skillsets",
                table: "Skillsets",
                column: "SkillsetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillsets_Users_UserId",
                table: "Skillsets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
