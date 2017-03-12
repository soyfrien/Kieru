using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kieru.Data.Migrations
{
    public partial class Signed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "WasViewed",
                table: "Secret",
                nullable: false,
                defaultValue: (short)-1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
