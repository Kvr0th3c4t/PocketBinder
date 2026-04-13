using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketBinder.Migrations
{
    /// <inheritdoc />
    public partial class AddedSetOnAlbumModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SetId",
                table: "Albums",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_SetId",
                table: "Albums",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Sets_SetId",
                table: "Albums",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Sets_SetId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_SetId",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "SetId",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
