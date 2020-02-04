using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inspection.Data.Migrations
{
    public partial class _2320201032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(maxLength: 100, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateUserId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    EditTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
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
                    RoleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateUserId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    EditTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantId = table.Column<int>(maxLength: 100, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Guid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleRight",
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
                    RoleId = table.Column<int>(nullable: false),
                    ControllerId = table.Column<string>(nullable: true),
                    ControllerRightId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRight_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TenantId = table.Column<int>(maxLength: 100, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateUserId = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    EditTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantApplication",
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
                    ApplicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantApplication_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantApplication_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "CompanyId", "CreateTime", "CreateUserId", "EditTime", "EditUserId", "IsDeleted", "Name", "TenantId" },
                values: new object[] { 1, null, null, null, null, null, false, "Admin", null });

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "CompanyId", "CreateTime", "CreateUserId", "EditTime", "EditUserId", "IsDeleted", "Name", "TenantId" },
                values: new object[] { 2, null, null, null, null, null, false, "Tenant", null });

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "CompanyId", "CreateTime", "CreateUserId", "EditTime", "EditUserId", "IsDeleted", "Name", "TenantId" },
                values: new object[] { 3, null, null, null, null, null, false, "Customer", null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CompanyId", "CreateTime", "CreateUserId", "EditTime", "EditUserId", "IsDeleted", "Name", "RoleType", "TenantId" },
                values: new object[] { 1, null, null, null, null, null, false, "Admin", 1, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CompanyId", "CreateTime", "CreateUserId", "EditTime", "EditUserId", "IsDeleted", "Password", "RoleId", "TenantId" },
                values: new object[] { "inspecionAdmin", null, null, null, null, null, false, "07e63ef0386c1bf691ea22f8b3c4c04d", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_RoleRight_RoleId",
                table: "RoleRight",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantApplication_ApplicationId",
                table: "TenantApplication",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantApplication_TenantId",
                table: "TenantApplication",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleRight");

            migrationBuilder.DropTable(
                name: "TenantApplication");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
