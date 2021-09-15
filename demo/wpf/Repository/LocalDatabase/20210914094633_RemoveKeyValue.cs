using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWPFUI.SQLiteCipher.Repository.LocalDatabase
{
    public partial class RemoveKeyValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "local_test");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "local_test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "local_test",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "local_test",
                type: "TEXT",
                nullable: true);
        }
    }
}
