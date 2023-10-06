using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeModify2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleEntityId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RoleEntityId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionEntityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionEntityRoleEntity",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntityRoleEntity", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionEntityRoleEntity_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionEntityRoleEntity_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PermissionEntityId",
                table: "AspNetUsers",
                column: "PermissionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEntityRoleEntity_RolesId",
                table: "PermissionEntityRoleEntity",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Permissions_PermissionEntityId",
                table: "AspNetUsers",
                column: "PermissionEntityId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Permissions_PermissionEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PermissionEntityRoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PermissionEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PermissionEntityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleEntityId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleEntityId",
                table: "Permissions",
                column: "RoleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleEntityId",
                table: "Permissions",
                column: "RoleEntityId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
