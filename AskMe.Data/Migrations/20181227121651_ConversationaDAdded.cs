using Microsoft.EntityFrameworkCore.Migrations;

namespace AskMe.Data.Migrations
{
    public partial class ConversationaDAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdId",
                table: "Conversations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_AdId",
                table: "Conversations",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Ads_AdId",
                table: "Conversations",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_AdId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "Conversations");
        }
    }
}
