using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccess.Migrations
{
    public partial class adddescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Steps",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Steps",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Items",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Steps",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }
    }
}
