using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarCollectionApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedFullCarList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Germany", "BMW" },
                    { 2, "Germany", "Audi" },
                    { 3, "USA", "Chevrolet" },
                    { 4, "Italy", "Ferrari" },
                    { 5, "Italy", "Lamborghini" },
                    { 6, "UK", "McLaren" },
                    { 7, "Germany", "Mercedes-Benz" },
                    { 8, "USA", "Ford" },
                    { 9, "Germany", "Porsche" },
                    { 10, "USA", "Tesla" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DoorCount", "Seats", "Type" },
                values: new object[,]
                {
                    { 1, 2, 2, "Coupe" },
                    { 2, 4, 5, "Sedan" },
                    { 3, 2, 2, "Supercar" }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "City", "Country", "Email", "Name", "Phone" },
                values: new object[] { 1, "Los Angeles", "USA", "contact@calidreamcars.com", "California Dream Cars", "323-555-9876" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "DealerId", "Engine", "FuelType", "Horsepower", "ImagePath", "Model", "Price", "Transmission", "Year" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "3.0L Twin-Turbo I6", "Petrol", 503, "bmw-m4.jpg", "BMW M4", 85000m, "Automatic", 2023 },
                    { 2, 2, 3, 1, "5.2L V10", "Petrol", 562, "audi-r8.jpg", "Audi R8", 145000m, "Automatic", 2022 },
                    { 3, 3, 1, 1, "6.2L V8", "Petrol", 495, "corvette-c8.jpg", "Corvette C8", 67000m, "Automatic", 2022 },
                    { 4, 4, 3, 1, "3.9L Twin-Turbo V8", "Petrol", 710, "ferrari-f8.jpg", "Ferrari F8", 276000m, "Automatic", 2023 },
                    { 5, 5, 3, 1, "5.2L V10", "Petrol", 631, "lamborghini-huracan.jpg", "Lamborghini Huracan", 261000m, "Automatic", 2022 },
                    { 6, 6, 3, 1, "4.0L Twin-Turbo V8", "Petrol", 710, "mclaren-720s.jpg", "McLaren 720S", 299000m, "Automatic", 2023 },
                    { 7, 7, 1, 1, "4.0L V8 Biturbo", "Petrol", 523, "mercedes-amg-gt.jpg", "Mercedes-AMG GT", 118000m, "Automatic", 2023 },
                    { 8, 8, 1, 1, "5.0L V8", "Petrol", 450, "mustang-gt.jpg", "Ford Mustang GT", 55000m, "Manual", 2022 },
                    { 9, 9, 1, 1, "3.0L Twin-Turbo Flat-6", "Petrol", 443, "porsche-911.jpg", "Porsche 911", 110000m, "Automatic", 2023 },
                    { 10, 10, 2, 1, "Dual Motor Electric", "Electric", 1020, "tesla-model-s.jpg", "Tesla Model S", 104000m, "Automatic", 2023 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
