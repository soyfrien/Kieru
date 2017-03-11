using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kieru.Data.Migrations
{
    public partial class OwnerIdToSecretsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Secret",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Secret",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Secret");

            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Secret",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
