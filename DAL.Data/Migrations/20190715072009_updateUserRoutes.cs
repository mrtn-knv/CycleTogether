using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Data.Migrations
{
    public partial class updateUserRoutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserRoutes_Id",
                table: "UserRoutes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoutes",
                table: "UserRoutes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoutes",
                table: "UserRoutes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoutes_UserId",
                table: "UserRoutes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoutes",
                table: "UserRoutes");

            migrationBuilder.DropIndex(
                name: "IX_UserRoutes_UserId",
                table: "UserRoutes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserRoutes_Id",
                table: "UserRoutes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoutes",
                table: "UserRoutes",
                columns: new[] { "UserId", "RouteId" });
        }
    }
}
