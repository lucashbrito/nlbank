using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLBank.Infra.Repository.Account.Migrations
{
    public partial class InitialAccountCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    CustomerUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
