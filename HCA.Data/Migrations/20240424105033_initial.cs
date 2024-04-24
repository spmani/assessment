using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCA.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HCA_EmployeeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCA_EmployeeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCA_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Createdby = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modifiedby = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCA_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(type: "TEXT", nullable: true),
                    Due_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Createdby = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modifiedby = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskDetails_HCA_EmployeeDetails_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "HCA_EmployeeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskDetails_HCA_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "HCA_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskActivityMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DoneBy = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskActivityMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskActivityMapping_HCA_EmployeeDetails_DoneBy",
                        column: x => x.DoneBy,
                        principalTable: "HCA_EmployeeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskActivityMapping_TaskDetails_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTagMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsName = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Createdby = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modifiedby = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTagMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTagMapping_TaskDetails_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HCA_EmployeeDetails",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Ponnusamy.r@altrockstech.com", "Altrocks1" },
                    { 2, "bharath.p@altrockstech.com", "Altrocks2" }
                });

            migrationBuilder.InsertData(
                table: "HCA_Status",
                columns: new[] { "Id", "CreateDate", "Createdby", "IsActive", "ModifiedDate", "Modifiedby", "StatusDescription" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 24, 16, 20, 32, 639, DateTimeKind.Local).AddTicks(5417), "System", true, null, null, "Pending" },
                    { 2, new DateTime(2024, 4, 24, 16, 20, 32, 639, DateTimeKind.Local).AddTicks(5495), "System", true, null, null, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskActivityMapping_DoneBy",
                table: "TaskActivityMapping",
                column: "DoneBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskActivityMapping_TaskId",
                table: "TaskActivityMapping",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_EmployeeId",
                table: "TaskDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_StatusId",
                table: "TaskDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTagMapping_TaskId",
                table: "TaskTagMapping",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskActivityMapping");

            migrationBuilder.DropTable(
                name: "TaskTagMapping");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "HCA_EmployeeDetails");

            migrationBuilder.DropTable(
                name: "HCA_Status");
        }
    }
}
