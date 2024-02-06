using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portale.Data.Migrations
{
	/// <inheritdoc />
	public partial class postTagsRels : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_PostTags",
				table: "PostTags");

			migrationBuilder.DropColumn(
				name: "Id",
				table: "PostTags");

			migrationBuilder.AddColumn<int>(
				name: "Id",
				table: "PostTags",
				type: "int",
				nullable: false)
				.Annotation("SqlServer:Identity", "1, 1");

			migrationBuilder.AddPrimaryKey(
				name: "PK_PostTags",
				table: "PostTags",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_PostTags_PostId",
				table: "PostTags",
				column: "PostId");
		}
		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_PostTags",
				table: "PostTags");

			migrationBuilder.DropIndex(
				name: "IX_PostTags_PostId",
				table: "PostTags");

			migrationBuilder.DropColumn(
				name: "Id",
				table: "PostTags");

			migrationBuilder.AddColumn<int>(
			   name: "Id",
			   table: "PostTags",
			   type: "int",
			   nullable: false);

			migrationBuilder.AddPrimaryKey(
				name: "PK_PostTags",
				table: "PostTags",
				columns: new[] { "PostId", "TagId" });
		}
	}
}