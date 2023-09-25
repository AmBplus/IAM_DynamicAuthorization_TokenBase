using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupPermissionId = table.Column<int>(type: "int", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationPermission_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationPermission_GroupPermission_GroupPermissionId",
                        column: x => x.GroupPermissionId,
                        principalTable: "GroupPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionOperationType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionOperationType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionOperationType_ApplicationPermission_ApplicationPermissionId",
                        column: x => x.ApplicationPermissionId,
                        principalTable: "ApplicationPermission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPermission_ApplicationRoleId",
                table: "ApplicationPermission",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPermission_GroupPermissionId",
                table: "ApplicationPermission",
                column: "GroupPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionOperationType_ApplicationPermissionId",
                table: "PermissionOperationType",
                column: "ApplicationPermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionOperationType");

            migrationBuilder.DropTable(
                name: "ApplicationPermission");

            migrationBuilder.DropTable(
                name: "GroupPermission");
        }
    }
}
