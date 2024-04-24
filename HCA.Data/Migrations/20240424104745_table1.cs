using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCA.Data.Migrations
{
    /// <inheritdoc />
    public partial class table1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Emailtriggered",
                table: "TaskDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 16, 17, 45, 591, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 16, 17, 45, 591, DateTimeKind.Local).AddTicks(9320));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emailtriggered",
                table: "TaskDetails");

            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 14, 18, 50, 167, DateTimeKind.Local).AddTicks(6313));

            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 14, 18, 50, 167, DateTimeKind.Local).AddTicks(6362));
        }
    }
}
