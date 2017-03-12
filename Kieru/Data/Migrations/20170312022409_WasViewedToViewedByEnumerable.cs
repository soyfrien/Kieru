using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kieru.Data.Migrations
{
    public partial class WasViewedToViewedByEnumerable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ViewedBy",
                table: "Secret",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "ViewedBy",
                table: "Secret",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
