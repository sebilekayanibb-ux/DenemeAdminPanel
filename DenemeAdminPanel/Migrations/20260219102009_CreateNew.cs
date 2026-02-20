using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class CreateNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "84560b7e-876b-4964-9e6e-d64ea0900f7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "60524774-c73d-46de-b07d-a8d7ee8a41cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e7g396h-5d2h-668h-08ch-605f78hf9432",
                column: "ConcurrencyStamp",
                value: "14168f42-661b-47d7-b435-6062da34a2e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "17e31043-66c5-4ba4-a43e-62b340d4347b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6g9i518j-7f4j-880j-20ej-827h90jh1654",
                column: "ConcurrencyStamp",
                value: "dcb94034-6451-48b4-b515-0444452f8a60");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "c9f882e6-b249-4218-8d76-857ac5c798c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f285f-4c1f-557g-97bg-594e67ge8321",
                column: "ConcurrencyStamp",
                value: "05b3fb3a-fe70-4826-9d54-e87b87590db5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e7g396h-5d2h-668h-08ch-605f78hf9432",
                column: "ConcurrencyStamp",
                value: "e4f3a17f-a465-49b9-aab8-c053f0006440");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8h407i-6e3i-779i-19di-716g89ig0543",
                column: "ConcurrencyStamp",
                value: "9b364183-81aa-4251-9c89-43d8b6a91938");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6g9i518j-7f4j-880j-20ej-827h90jh1654",
                column: "ConcurrencyStamp",
                value: "8185e5c7-c795-4e55-9974-65c4264093be");
        }
    }
}
