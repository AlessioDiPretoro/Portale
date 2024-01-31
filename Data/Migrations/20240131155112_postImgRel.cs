using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portale.Data.Migrations
{
    /// <inheritdoc />
    public partial class postImgRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostImgs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "PostImgs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
