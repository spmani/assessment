using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCA.Data.Migrations
{
    /// <inheritdoc />
    public partial class table12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 16, 33, 33, 570, DateTimeKind.Local).AddTicks(4103));

            migrationBuilder.UpdateData(
                table: "HCA_Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 24, 16, 33, 33, 570, DateTimeKind.Local).AddTicks(4214));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
