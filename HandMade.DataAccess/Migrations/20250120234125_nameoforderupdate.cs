using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMade.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class nameoforderupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_orderSummaries_orderSummaryId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderSummaries_AspNetUsers_userId",
                table: "orderSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderSummaries",
                table: "orderSummaries");

            migrationBuilder.RenameTable(
                name: "orderSummaries",
                newName: "OrderSummaries");

            migrationBuilder.RenameIndex(
                name: "IX_orderSummaries_userId",
                table: "OrderSummaries",
                newName: "IX_OrderSummaries_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSummaries",
                table: "OrderSummaries",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderSummaries_orderSummaryId",
                table: "OrderDetails",
                column: "orderSummaryId",
                principalTable: "OrderSummaries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummaries_AspNetUsers_userId",
                table: "OrderSummaries",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderSummaries_orderSummaryId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummaries_AspNetUsers_userId",
                table: "OrderSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSummaries",
                table: "OrderSummaries");

            migrationBuilder.RenameTable(
                name: "OrderSummaries",
                newName: "orderSummaries");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSummaries_userId",
                table: "orderSummaries",
                newName: "IX_orderSummaries_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderSummaries",
                table: "orderSummaries",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_orderSummaries_orderSummaryId",
                table: "OrderDetails",
                column: "orderSummaryId",
                principalTable: "orderSummaries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderSummaries_AspNetUsers_userId",
                table: "orderSummaries",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
