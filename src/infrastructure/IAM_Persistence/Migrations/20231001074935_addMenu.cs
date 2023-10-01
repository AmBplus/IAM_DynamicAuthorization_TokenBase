using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionOperationTypes");

            migrationBuilder.CreateTable(
                name: "MenuGroupEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroupEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModulePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuEntities_MenuEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuEntities_MenuGroupEntities_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroupEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenuEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenuEntities_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenuEntities_MenuEntities_MenuEntityId",
                        column: x => x.MenuEntityId,
                        principalTable: "MenuEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntities_MenuGroupId",
                table: "MenuEntities",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntities_ParentId",
                table: "MenuEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuEntities_MenuEntityId",
                table: "RoleMenuEntities",
                column: "MenuEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuEntities_RoleId",
                table: "RoleMenuEntities",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModulePermissions");

            migrationBuilder.DropTable(
                name: "RoleMenuEntities");

            migrationBuilder.DropTable(
                name: "MenuEntities");

            migrationBuilder.DropTable(
                name: "MenuGroupEntities");

            migrationBuilder.CreateTable(
                name: "PermissionOperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationPermissionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionOperationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionOperationTypes_Permissions_ApplicationPermissionId",
                        column: x => x.ApplicationPermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

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
                name: "IX_PermissionOperationTypes_ApplicationPermissionId",
                table: "PermissionOperationTypes",
                column: "ApplicationPermissionId");
        }
    }
}
