using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _0205201446 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponseType",
                table: "FormBuilderQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FormBuilderQuestionsResponse",
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
                    ResponseType = table.Column<int>(nullable: false),
                    MaxScore = table.Column<int>(nullable: false),
                    Scored = table.Column<int>(nullable: false),
                    ResponseText = table.Column<string>(nullable: true),
                    FormBuilderQuestionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderQuestionsResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormBuilderQuestionsResponse_FormBuilderQuestions_FormBuilderQuestionsId",
                        column: x => x.FormBuilderQuestionsId,
                        principalTable: "FormBuilderQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 46, 31, 408, DateTimeKind.Utc).AddTicks(657));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 46, 31, 436, DateTimeKind.Utc).AddTicks(1828));

            migrationBuilder.UpdateData(
                table: "Application",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 46, 31, 436, DateTimeKind.Utc).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2020, 2, 5, 9, 46, 31, 443, DateTimeKind.Utc).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "inspecionAdmin",
                columns: new[] { "CreateTime", "Password" },
                values: new object[] { new DateTime(2020, 2, 5, 9, 46, 31, 455, DateTimeKind.Utc).AddTicks(6510), "07e63ef0386c1bf691ea22f8b3c4c04d" });

            migrationBuilder.CreateIndex(
                name: "IX_FormBuilderQuestionsResponse_FormBuilderQuestionsId",
                table: "FormBuilderQuestionsResponse",
                column: "FormBuilderQuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormBuilderQuestionsResponse");

            migrationBuilder.DropColumn(
                name: "ResponseType",
                table: "FormBuilderQuestions");

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
