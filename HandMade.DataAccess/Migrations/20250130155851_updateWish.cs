using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMade.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateWish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_applicationUserId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "WishLists",
                newName: "applicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_applicationUserId",
                table: "WishLists",
                newName: "IX_WishLists_applicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_applicationUserID",
                table: "WishLists",
                column: "applicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_applicationUserID",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "applicationUserID",
                table: "WishLists",
                newName: "applicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_applicationUserID",
                table: "WishLists",
                newName: "IX_WishLists_applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_applicationUserId",
                table: "WishLists",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
