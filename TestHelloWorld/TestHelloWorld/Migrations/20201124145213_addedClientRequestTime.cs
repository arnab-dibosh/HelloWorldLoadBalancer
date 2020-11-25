using Microsoft.EntityFrameworkCore.Migrations;

namespace TestHelloWorld.Migrations
{
    public partial class addedClientRequestTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientRequestTime",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientRequestTime",
                table: "Transactions");
        }
    }
}
