using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNest.Migrations
{
    /// <inheritdoc />
    public partial class updatenotificationmodel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "Notifications",
                type: "varchar(13)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(13)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Isbn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "BookId",
                keyValue: null,
                column: "BookId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "Notifications",
                type: "varchar(13)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Isbn",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
