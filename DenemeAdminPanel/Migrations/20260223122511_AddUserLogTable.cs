using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7abc763f-0b1c-436b-8529-0ace8fee062c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "2118a6a4-4b50-4340-af0f-5403b397687e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "67db4eef-9c8d-4547-b53c-bd1011c5cc76");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9e0d357d-b7f6-4e64-89f1-2b1722d3972d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "7a489dc3-94e0-4b88-9d64-62ab242999d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "a5a67111-0211-4a8c-a630-daa12655a2a0");
        }
    }
}
