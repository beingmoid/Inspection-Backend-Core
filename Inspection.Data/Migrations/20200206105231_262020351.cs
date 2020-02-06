using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _262020351 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "FormBuilder",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 6, 10, 52, 29, 435, DateTimeKind.Utc).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2020, 2, 6, 10, 52, 30, 128, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2020, 2, 6, 10, 52, 30, 128, DateTimeKind.Utc).AddTicks(8595));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 6, 10, 52, 30, 135, DateTimeKind.Utc).AddTicks(638));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { new DateTime(2020, 2, 6, 10, 52, 30, 184, DateTimeKind.Utc).AddTicks(6642), "07e63ef0386c1bf691ea22f8b3c4c04d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "FormBuilder",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
