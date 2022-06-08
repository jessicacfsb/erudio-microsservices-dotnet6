using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CuponAPI.Migrations
{
    public partial class SeedCuponDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cupon",
                columns: new[] { "id", "cupon_code", "discount-amount" },
                values: new object[] { 1L, "ERUDIO_2022_10", 10m });

            migrationBuilder.InsertData(
                table: "cupon",
                columns: new[] { "id", "cupon_code", "discount-amount" },
                values: new object[] { 2L, "ERUDIO_2022_15", 15m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cupon",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cupon",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
