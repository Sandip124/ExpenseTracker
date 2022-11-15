using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ExpenseTracker.Infrastructure.Migrations
{
    public partial class addbudgetentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "budget",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkspaceId1 = table.Column<int>(type: "integer", nullable: true),
                    WorkspaceId = table.Column<long>(type: "bigint", nullable: false),
                    RecByUserId = table.Column<int>(type: "integer", nullable: true),
                    RecById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_budget_user_RecByUserId",
                        column: x => x.RecByUserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_budget_workspace_WorkspaceId1",
                        column: x => x.WorkspaceId1,
                        principalTable: "workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_budget_RecByUserId",
                table: "budget",
                column: "RecByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_budget_WorkspaceId1",
                table: "budget",
                column: "WorkspaceId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budget");
        }
    }
}
