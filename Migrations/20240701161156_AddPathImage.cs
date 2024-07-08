﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProdutos.Migrations
{
    /// <inheritdoc />
    public partial class AddPathImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Products");
        }
    }
}
