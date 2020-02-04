using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _252020329 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormBuilder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(maxLength: 100, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateUserId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    EditTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormBuilder_FormBuilderType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FormBuilderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 4, 22, 29, 23, 23, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2020, 2, 4, 22, 29, 23, 100, DateTimeKind.Utc).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2020, 2, 4, 22, 29, 23, 101, DateTimeKind.Utc).AddTicks(2371));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 4, 22, 29, 23, 106, DateTimeKind.Utc).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { new DateTime(2020, 2, 4, 22, 29, 23, 120, DateTimeKind.Utc).AddTicks(5701), "07e63ef0386c1bf691ea22f8b3c4c04d" });

            migrationBuilder.CreateIndex(
                name: "IX_FormBuilder_TypeId",
                table: "FormBuilder",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormBuilder");

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
