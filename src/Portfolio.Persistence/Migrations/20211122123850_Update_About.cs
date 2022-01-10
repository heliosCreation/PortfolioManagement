using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Migrations
{
    public partial class Update_About : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowCvLink",
                table: "About",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowGithubUrl",
                table: "About",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowLinkedinUrl",
                table: "About",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowCvLink",
                table: "About");

            migrationBuilder.DropColumn(
                name: "ShowGithubUrl",
                table: "About");

            migrationBuilder.DropColumn(
                name: "ShowLinkedinUrl",
                table: "About");
        }
    }
}
