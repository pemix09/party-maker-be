using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "AspNetUsers",
                newName: "AvarageRating");

            migrationBuilder.AddColumn<int>(
                name: "AmmountOfRatings",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float[]>(
                name: "Ratings",
                table: "AspNetUsers",
                type: "real[]",
                nullable: false,
                defaultValue: new float[0]);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmmountOfRatings",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AvarageRating",
                table: "AspNetUsers",
                newName: "Rating");
        }
    }
}
