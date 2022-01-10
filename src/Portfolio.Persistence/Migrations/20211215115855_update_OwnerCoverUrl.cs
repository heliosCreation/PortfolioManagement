using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Migrations
{
    public partial class update_OwnerCoverUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "OwnerPresentation");

            migrationBuilder.RenameColumn(
                name: "LogoText",
                table: "OwnerPresentation",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "HomeMainTexts",
                newName: "CoverUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "OwnerPresentation",
                newName: "LogoText");

            migrationBuilder.RenameColumn(
                name: "CoverUrl",
                table: "HomeMainTexts",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "OwnerPresentation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
