using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibProject.Migrations
{
    public partial class AddSampleBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert into Genre table with correct column name
            migrationBuilder.InsertData(
                table: "Genre", // Correct table name
                columns: new[] { "genresname" }, // Column is 'genresname' in DB
                values: new object[,]
                {
                    { "Роман" },
                    { "Антиутопия" }
                });

            // Insert into Books with correct column names
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "title", "author", "year", "price", "image_url", "is_available", "genre_id" },
                values: new object[,]
                {
                    {
                        "Война и мир",
                        "Лев Толстой",
                        1869,
                        1200.00m,
                        "https://example.com/war-and-peace.jpg",
                        true,
                        1 // Ensure this matches the generated Genre Ids
                    },
                    {
                        "1984",
                        "Джордж Оруэлл",
                        1949,
                        950.00m,
                        "https://example.com/1984.jpg",
                        true,
                        2
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optional: Add code to remove the inserted data if needed
        }
    }
}