using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookUserPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the table if it already exists
            migrationBuilder.DropTable(
                name: "BookUsers");

            // Recreate the table with the new schema
            migrationBuilder.CreateTable(
                name: "BookUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    BookId = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Progress = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUsers", x => new { x.UserId, x.BookId, x.Progress });
                });

            // If necessary, reinsert the backed-up data
            // migrationBuilder.Sql("INSERT INTO BookUsers SELECT * FROM BookUsersBackup;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new table if you need to revert
            migrationBuilder.DropTable(name: "BookUsers");

            // Recreate the old table
            migrationBuilder.CreateTable(
                name: "BookUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    BookId = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Progress = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUsers", x => new { x.UserId, x.BookId });
                });
        }
    }
}
