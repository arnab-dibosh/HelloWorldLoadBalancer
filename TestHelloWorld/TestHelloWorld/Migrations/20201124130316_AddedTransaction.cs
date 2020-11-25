using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestHelloWorld.Migrations
{
    public partial class AddedTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleBank1Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleBank1Info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SampleBank2Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleBank2Info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderVid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAccNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderBankId = table.Column<int>(type: "int", nullable: false),
                    ReceicerVid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverAccNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceicerBankId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleBank1Info");

            migrationBuilder.DropTable(
                name: "SampleBank2Info");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
