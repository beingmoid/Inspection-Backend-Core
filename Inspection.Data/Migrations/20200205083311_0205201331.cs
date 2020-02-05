using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _0205201331 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 8, 33, 9, 259, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 8, 33, 9, 304, DateTimeKind.Utc).AddTicks(7165));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 8, 33, 9, 304, DateTimeKind.Utc).AddTicks(9672));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 8, 33, 9, 311, DateTimeKind.Utc).AddTicks(7549));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { new DateTime(2020, 2, 5, 8, 33, 9, 325, DateTimeKind.Utc).AddTicks(2287), "07e63ef0386c1bf691ea22f8b3c4c04d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { null, "07e63ef0386c1bf691ea22f8b3c4c04d" });
        }
    }
}
