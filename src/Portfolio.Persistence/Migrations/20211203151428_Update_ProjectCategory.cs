using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Migrations
{
    public partial class Update_ProjectCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectCategories_CategoryId",
                table: "Projects",
                column: "CategoryId",
                principalTable: "ProjectCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectCategories_CategoryId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CategoryId",
                table: "Projects");
        }
    }
}
