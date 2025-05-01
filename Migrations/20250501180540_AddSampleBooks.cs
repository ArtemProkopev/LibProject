using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "genresname" },
                values: new object[,]
                {
                    { "Роман" },
                    { "Антиутопия" }
                });

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
                        1
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
