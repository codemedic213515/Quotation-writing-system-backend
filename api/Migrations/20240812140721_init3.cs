using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eea4e8b-8d9f-4c4b-bd17-89924e7146bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f13282e5-d73c-49c9-91d4-9b5e4f9b47ee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "470ff9c9-f77e-4688-881c-6bf18ab79497", null, "User", "USER" },
                    { "7e149691-380e-4a1e-a5a2-681e105a5f2a", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "470ff9c9-f77e-4688-881c-6bf18ab79497");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e149691-380e-4a1e-a5a2-681e105a5f2a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1eea4e8b-8d9f-4c4b-bd17-89924e7146bc", null, "User", "USER" },
                    { "f13282e5-d73c-49c9-91d4-9b5e4f9b47ee", null, "Admin", "ADMIN" }
                });
        }
    }
}
