using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _0205201425 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormBuilderQuestions",
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
                    Text = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    IsScored = table.Column<bool>(nullable: true),
                    MaxScore = table.Column<int>(nullable: true),
                    Scored = table.Column<int>(nullable: true),
                    IsMandatory = table.Column<bool>(nullable: false),
                    ISMultiSelect = table.Column<bool>(nullable: false),
                    IsNotified = table.Column<bool>(nullable: false),
                    FormBuilderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormBuilderQuestions_FormBuilder_FormBuilderId",
                        column: x => x.FormBuilderId,
                        principalTable: "FormBuilder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 26, 51, 70, DateTimeKind.Utc).AddTicks(2869));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 26, 51, 98, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 26, 51, 98, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 26, 51, 104, DateTimeKind.Utc).AddTicks(8275));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { new DateTime(2020, 2, 5, 9, 26, 51, 118, DateTimeKind.Utc).AddTicks(8224), "07e63ef0386c1bf691ea22f8b3c4c04d" });

            migrationBuilder.CreateIndex(
                name: "IX_FormBuilderQuestions_FormBuilderId",
                table: "FormBuilderQuestions",
                column: "FormBuilderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormBuilderQuestions");

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
