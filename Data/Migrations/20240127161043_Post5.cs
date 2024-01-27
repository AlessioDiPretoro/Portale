using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portale.Data.Migrations
{
    /// <inheritdoc />
    public partial class Post5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

 

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserInfoId",
                table: "Posts",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserInfo_UserInfoId",
                table: "Posts",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserInfo_UserInfoId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserInfoId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Posts");
        }
    }
}
