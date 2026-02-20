using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class CleanOldRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e7g396h-5d2h-668h-08ch-605f78hf9432");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6g9i518j-7f4j-880j-20ej-827h90jh1654");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "cd0dc4c5-fa5b-4d81-b8d6-39b5153ed0a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "1af20199-e636-45f6-8e41-7911aee36ec3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "833a8064-b5f6-4fa2-a72b-8416deca9416");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e7g396h-5d2h-668h-08ch-605f78hf9432", "12f37500-ec23-4a3c-80f1-368f3d591ade", "Support", "SUPPORT" },
                    { "6g9i518j-7f4j-880j-20ej-827h90jh1654", "c0e0aa7c-e89c-41b6-9340-b771833249dd", "Analyst", "ANALYST" }
                });
        }
    }
}
