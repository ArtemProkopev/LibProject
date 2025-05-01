using Microsoft.EntityFrameworkCore;
using LibProject.Models;

namespace LibProject.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<BookByReader> BooksByReaders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
