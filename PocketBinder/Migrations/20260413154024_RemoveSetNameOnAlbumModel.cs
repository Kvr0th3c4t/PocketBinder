using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketBinder.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSetNameOnAlbumModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetName",
                table: "Albums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SetName",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
