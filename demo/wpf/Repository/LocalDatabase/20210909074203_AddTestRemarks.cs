using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWPFUI.SQLiteCipher.Repository.LocalDatabase
{
    public partial class AddTestRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "local_test",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "local_test");
        }
    }
}
