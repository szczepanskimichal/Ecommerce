using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Crispy and delicious samosa filled with spiced potatoes and peas.", "", "Samosa", 2.9900000000000002, "" },
                    { 2, "Main Course", "Grilled paneer cubes marinated in a blend of spices and yogurt.", "", "Paneer Tikka", 9.9900000000000002, "" },
                    { 3, "Dessert", "Soft and sweet gulab jamun soaked in rose-flavored syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 4, "Dessert", "Soft and sweet gulab jamun soaked in rose-flavored syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 5, "Dessert", "Soft and sweet gulab jamun syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 6, "Dessert", "Soft and sweet gulab jamun syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 7, "Dessert", "Soft and sweet gulab jamun syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 8, "Dessert", "Soft and sweet gulab jamun syrup.", "", "Gulab Jamun", 4.9900000000000002, "" },
                    { 9, "Dessert", "Soft and sweet gulab jamun syrup.", "", "Gulab Jamun", 4.9900000000000002, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
