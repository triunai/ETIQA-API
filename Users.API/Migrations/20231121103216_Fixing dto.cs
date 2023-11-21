﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Migrations
{
    /// <inheritdoc />
    public partial class Fixingdto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Skillset",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Skillset",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Skillset_User_UserId",
                table: "Skillset",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
