using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWPFUI.SQLiteCipher.Repository.LocalDatabase
{
    public partial class AddTestKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "local_test",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "local_test");
        }
    }
}
