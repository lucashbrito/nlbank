using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLBank.Infra.Repository.Account.Migrations
{
    public partial class ChangedUIdtoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerUid",
                table: "Accounts",
                newName: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Accounts",
                newName: "CustomerUid");
        }
    }
}
