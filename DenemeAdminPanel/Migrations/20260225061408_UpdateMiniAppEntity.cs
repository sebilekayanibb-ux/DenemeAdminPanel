using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMiniAppEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermissionText",
                table: "MiniApps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "52bcaa5e-2991-45ee-9310-10cf47cc224f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "ab1ef32b-b125-4851-9fcb-48d7d6b7bf5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "f5cd6647-5cea-4064-8682-2d330f5f76c9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionText",
                table: "MiniApps");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "315fcc31-a239-45eb-a159-ad24d0d1b314");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "b74b5523-81b4-4f5f-adda-5985f1344222");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "44f388d8-84f7-4227-8c36-1dc2797d8524");
        }
    }
}
