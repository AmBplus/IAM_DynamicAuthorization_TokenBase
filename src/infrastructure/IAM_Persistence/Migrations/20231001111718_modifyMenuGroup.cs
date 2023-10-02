using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifyMenuGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities");

            migrationBuilder.RenameColumn(
                name: "ParrentId",
                table: "MenuGroupEntities",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities",
                newName: "IX_MenuGroupEntities_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParentId",
                table: "MenuGroupEntities",
                column: "ParentId",
                principalTable: "MenuGroupEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParentId",
                table: "MenuGroupEntities");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "MenuGroupEntities",
                newName: "ParrentId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuGroupEntities_ParentId",
                table: "MenuGroupEntities",
                newName: "IX_MenuGroupEntities_ParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuGroupEntities_MenuGroupEntities_ParrentId",
                table: "MenuGroupEntities",
                column: "ParrentId",
                principalTable: "MenuGroupEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
