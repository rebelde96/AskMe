using Microsoft.EntityFrameworkCore.Migrations;

namespace AskMe.Data.Migrations
{
    public partial class AdsChangesKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_AspNetUsers_ApplicationUserId1",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Categories_CategoryId",
                table: "Ad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ad",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_ApplicationUserId1",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Ad");

            migrationBuilder.RenameTable(
                name: "Ad",
                newName: "Ads");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_CategoryId",
                table: "Ads",
                newName: "IX_Ads_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Ads",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_ApplicationUserId",
                table: "Ads",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_ApplicationUserId",
                table: "Ads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Categories_CategoryId",
                table: "Ads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_ApplicationUserId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Categories_CategoryId",
                table: "Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_ApplicationUserId",
                table: "Ads");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Ad");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_CategoryId",
                table: "Ad",
                newName: "IX_Ad_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Ad",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Ad",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ad",
                table: "Ad",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_ApplicationUserId1",
                table: "Ad",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_AspNetUsers_ApplicationUserId1",
                table: "Ad",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Categories_CategoryId",
                table: "Ad",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
