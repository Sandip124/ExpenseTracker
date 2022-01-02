using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ExpenseTracker.Web.Migrations
{
    public partial class schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transaction_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "workspace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    WorkSpaceName = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkspaceType = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workspace_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TransactionCategoryId = table.Column<int>(type: "int", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_transaction_category_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "transaction_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_workspace_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_TransactionCategoryId",
                table: "transaction",
                column: "TransactionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_WorkspaceId",
                table: "transaction",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_workspace_UserId",
                table: "workspace",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "transaction_category");

            migrationBuilder.DropTable(
                name: "workspace");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
