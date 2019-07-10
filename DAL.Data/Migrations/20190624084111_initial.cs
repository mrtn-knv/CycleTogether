using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Terrain = table.Column<int>(nullable: false),
                    Endurance = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    StartPoint = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    SuitableForKids = table.Column<bool>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    Terrain = table.Column<int>(nullable: false),
                    Endurance = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEquipments",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    EquipmentId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquipments", x => new { x.UserId, x.EquipmentId });
                    table.UniqueConstraint("AK_UserEquipments_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEquipments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEquipments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    RouteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteEquipments",
                columns: table => new
                {
                    RouteId = table.Column<Guid>(nullable: false),
                    EquipmentId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEquipments", x => new { x.EquipmentId, x.RouteId });
                    table.UniqueConstraint("AK_RouteEquipments_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEquipments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteEquipments_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoutes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoutes", x => new { x.UserId, x.RouteId });
                    table.UniqueConstraint("AK_UserRoutes_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoutes_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoutes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_RouteId",
                table: "Pictures",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteEquipments_RouteId",
                table: "RouteEquipments",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserId",
                table: "Routes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_EquipmentId",
                table: "UserEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoutes_RouteId",
                table: "UserRoutes",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "RouteEquipments");

            migrationBuilder.DropTable(
                name: "UserEquipments");

            migrationBuilder.DropTable(
                name: "UserRoutes");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
