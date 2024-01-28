using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portale.Data.Migrations
{
    /// <inheritdoc />
    public partial class postUsersNewRelNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_IdentityId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "UserInfo");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserInfo",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInfo");

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "UserInfo",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_IdentityId",
                table: "UserInfo",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers",
                table: "UserInfo",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
