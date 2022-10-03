using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLBank.Infra.Repository.Transaction.Migrations
{
    public partial class ChangedOperationUIdtoOperationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationUid",
                table: "Transactions",
                newName: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "Transactions",
                newName: "OperationUid");
        }
    }
}
