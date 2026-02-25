using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class AddMiniAppTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "MiniApps",
                newName: "IconUrl");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "MiniApps",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "MiniApps");

            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "MiniApps",
                newName: "IconPath");

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
    }
}
