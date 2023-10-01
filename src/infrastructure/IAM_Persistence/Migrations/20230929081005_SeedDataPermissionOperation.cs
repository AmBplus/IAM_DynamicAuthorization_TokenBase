using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataPermissionOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GroupPermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "PermissionOperationTypes",
                columns: new[] { "Id", "ApplicationPermissionId", "Name" },
                values: new object[,]
                {
                    { 1, null, "ReadPermission" },
                    { 10, null, "WritePermission" },
                    { 20, null, "InsertPermission" },
                    { 30, null, "UpdatePermission" },
                    { 40, null, "DeletePermission" },
                    { 500, null, "AllAccessPermission" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ActionName",
                table: "Permissions",
                column: "ActionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_Name",
                table: "GroupPermissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_ActionName",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermissions_Name",
                table: "GroupPermissions");

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "PermissionOperationTypes",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
