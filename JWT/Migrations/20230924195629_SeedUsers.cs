using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9b0fdb80-3ead-4d7a-bea5-df9ea6f77338", 0, "f121e9bf-5f96-4bc2-92ad-697ef669155b", "alberto@email.com", false, false, null, "ALBERTO@EMAIL.COM", "BETO", "AQAAAAIAAYagAAAAELkd2xSIghhdNfXM0chK5XklpQhqz7ZRDwSz3WbWQquvnjCSV4OM59+cDaHgmGv0BA==", null, false, "0960b0fd-871e-4b79-b697-a12deff0715f", false, "Beto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b0fdb80-3ead-4d7a-bea5-df9ea6f77338");
        }
    }
}
