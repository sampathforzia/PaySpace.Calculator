using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaySpace.Calculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatorSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calculator = table.Column<int>(type: "int", nullable: false),
                    RateType = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    From = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    To = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calculator = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CalculatorSetting",
                columns: new[] { "Id", "Calculator", "From", "Rate", "RateType", "To" },
                values: new object[,]
                {
                    { 1L, 0, 0m, 10m, 0, 8350m },
                    { 2L, 0, 8351m, 15m, 0, 33950m },
                    { 3L, 0, 33951m, 25m, 0, 82250m },
                    { 4L, 0, 82251m, 28m, 0, 171550m },
                    { 5L, 0, 171551m, 33m, 0, 372950m },
                    { 6L, 0, 372951m, 35m, 0, 0m },
                    { 7L, 1, 0m, 5m, 0, 199999m },
                    { 8L, 1, 200000m, 10000m, 1, 0m },
                    { 9L, 2, 0m, 17.5m, 0, 0m }
                });

            migrationBuilder.InsertData(
                table: "PostalCode",
                columns: new[] { "Id", "Calculator", "Code" },
                values: new object[,]
                {
                    { 1L, 0, "7441" },
                    { 2L, 1, "A100" },
                    { 3L, 2, "7000" },
                    { 4L, 0, "1000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatorSetting");

            migrationBuilder.DropTable(
                name: "PostalCode");
        }
    }
}
