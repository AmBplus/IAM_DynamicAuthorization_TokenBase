using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntitiy1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "DisplayNames");

            migrationBuilder.DropTable(
                name: "ModulePermissions");

            migrationBuilder.DropTable(
                name: "SmsCodes");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationRoleId",
                table: "Permissions",
                newName: "RoleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_ApplicationRoleId",
                table: "Permissions",
                newName: "IX_Permissions_RoleEntityId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UsersProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UsersProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RoleMenuEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "RoleMenuEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MenuGroupEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "MenuGroupEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParrentId",
                table: "MenuGroupEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MenuEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "MenuEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GroupPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "GroupPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SystemEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemEntities_SystemEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SystemEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities",
                column: "ParrentId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemEntities_ParentId",
                table: "SystemEntities",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities",
                column: "ParrentId",
                principalTable: "MenuGroupEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleEntityId",
                table: "Permissions",
                column: "RoleEntityId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_SystemEntities_SystemId",
                table: "Permissions",
                column: "SystemId",
                principalTable: "SystemEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleEntityId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_SystemEntities_SystemId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "SystemEntities");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UsersProfiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "UsersProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RoleMenuEntities");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "RoleMenuEntities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MenuGroupEntities");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "MenuGroupEntities");

            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "MenuGroupEntities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MenuEntities");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "MenuEntities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RoleEntityId",
                table: "Permissions",
                newName: "ApplicationRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_RoleEntityId",
                table: "Permissions",
                newName: "IX_Permissions_ApplicationRoleId");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DisplayNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayNames", x => x.Id);
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
                name: "SmsCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsCodes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
