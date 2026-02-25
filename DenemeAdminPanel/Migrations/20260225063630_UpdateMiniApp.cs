using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMiniApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "MiniApps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9864a2cf-7da2-49af-a71b-73c54138baa1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "3bda338f-b82f-4428-82ba-bb09c99cb93b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "9517b169-f7df-4107-bcc6-bb8514f574c4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "MiniApps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "46aafbd8-68db-40c3-91cc-b41d1378903b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "3164ce91-6b58-45f2-bbed-206b7ab57e04");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "f8e1e848-4830-4815-a87f-abc310a2fa3b");
        }
    }
}
