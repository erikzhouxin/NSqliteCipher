using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWPFUI.SQLiteCipher.Repository.LocalDatabase
{
    public partial class AddTestValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "local_test",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "local_test");
        }
    }
}
