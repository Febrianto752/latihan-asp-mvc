using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStrore.Migrations
{
    public partial class renametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Account_account_guid",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_role_guid",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "AccountRole",
                newName: "account_roles");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Role_name",
                table: "roles",
                newName: "IX_roles_name");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_role_guid",
                table: "account_roles",
                newName: "IX_account_roles_role_guid");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_account_guid_role_guid",
                table: "account_roles",
                newName: "IX_account_roles_account_guid_role_guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account_roles",
                table: "account_roles",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_account_roles_accounts_account_guid",
                table: "account_roles",
                column: "account_guid",
                principalTable: "accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_account_roles_roles_role_guid",
                table: "account_roles",
                column: "role_guid",
                principalTable: "roles",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_roles_accounts_account_guid",
                table: "account_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_account_roles_roles_role_guid",
                table: "account_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account_roles",
                table: "account_roles");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Account");

            migrationBuilder.RenameTable(
                name: "account_roles",
                newName: "AccountRole");

            migrationBuilder.RenameIndex(
                name: "IX_roles_name",
                table: "Role",
                newName: "IX_Role_name");

            migrationBuilder.RenameIndex(
                name: "IX_account_roles_role_guid",
                table: "AccountRole",
                newName: "IX_AccountRole_role_guid");

            migrationBuilder.RenameIndex(
                name: "IX_account_roles_account_guid_role_guid",
                table: "AccountRole",
                newName: "IX_AccountRole_account_guid_role_guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Account_account_guid",
                table: "AccountRole",
                column: "account_guid",
                principalTable: "Account",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_role_guid",
                table: "AccountRole",
                column: "role_guid",
                principalTable: "Role",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
