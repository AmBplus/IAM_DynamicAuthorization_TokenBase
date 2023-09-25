using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifiedDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationPermission_AspNetRoles_ApplicationRoleId",
                table: "ApplicationPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationPermission_GroupPermission_GroupPermissionId",
                table: "ApplicationPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionOperationType_ApplicationPermission_ApplicationPermissionId",
                table: "PermissionOperationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionOperationType",
                table: "PermissionOperationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationPermission",
                table: "ApplicationPermission");

            migrationBuilder.RenameTable(
                name: "PermissionOperationType",
                newName: "PermissionOperationTypes");

            migrationBuilder.RenameTable(
                name: "GroupPermission",
                newName: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "ApplicationPermission",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionOperationType_ApplicationPermissionId",
                table: "PermissionOperationTypes",
                newName: "IX_PermissionOperationTypes_ApplicationPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationPermission_GroupPermissionId",
                table: "Permissions",
                newName: "IX_Permissions_GroupPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationPermission_ApplicationRoleId",
                table: "Permissions",
                newName: "IX_Permissions_ApplicationRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionOperationTypes",
                table: "PermissionOperationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DisplayNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersProfiles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersProfiles_ApplicationUserId",
                table: "UsersProfiles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionOperationTypes_Permissions_ApplicationPermissionId",
                table: "PermissionOperationTypes",
                column: "ApplicationPermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_GroupPermissions_GroupPermissionId",
                table: "Permissions",
                column: "GroupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionOperationTypes_Permissions_ApplicationPermissionId",
                table: "PermissionOperationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_GroupPermissions_GroupPermissionId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "DisplayNames");

            migrationBuilder.DropTable(
                name: "SmsCodes");

            migrationBuilder.DropTable(
                name: "UsersProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionOperationTypes",
                table: "PermissionOperationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "ApplicationPermission");

            migrationBuilder.RenameTable(
                name: "PermissionOperationTypes",
                newName: "PermissionOperationType");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                newName: "GroupPermission");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_GroupPermissionId",
                table: "ApplicationPermission",
                newName: "IX_ApplicationPermission_GroupPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_ApplicationRoleId",
                table: "ApplicationPermission",
                newName: "IX_ApplicationPermission_ApplicationRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionOperationTypes_ApplicationPermissionId",
                table: "PermissionOperationType",
                newName: "IX_PermissionOperationType_ApplicationPermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationPermission",
                table: "ApplicationPermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionOperationType",
                table: "PermissionOperationType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationPermission_AspNetRoles_ApplicationRoleId",
                table: "ApplicationPermission",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationPermission_GroupPermission_GroupPermissionId",
                table: "ApplicationPermission",
                column: "GroupPermissionId",
                principalTable: "GroupPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionOperationType_ApplicationPermission_ApplicationPermissionId",
                table: "PermissionOperationType",
                column: "ApplicationPermissionId",
                principalTable: "ApplicationPermission",
                principalColumn: "Id");
        }
    }
}
