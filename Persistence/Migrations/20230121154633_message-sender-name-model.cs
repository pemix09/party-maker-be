using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class messagesendernamemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<List<string>>(
                name: "ParticipatorsIds",
                table: "Events",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Messages");

            migrationBuilder.AlterColumn<List<int>>(
                name: "ParticipatorsIds",
                table: "Events",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);
        }
    }
}
