using Microsoft.EntityFrameworkCore;
using LibProject.Models.Domain;

namespace LibProject.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<Favorites> Favorites { get; set; } = null!;
        public DbSet<BorrowedBook> BorrowedBooks { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация для Basket
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasOne(b => b.Book)
                    .WithMany(b => b.Baskets)
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Reader)
                    .WithMany(r => r.Baskets)
                    .HasForeignKey(b => b.ReaderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация для Favorites
            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasOne(f => f.Book)
                    .WithMany(b => b.Favorites)
                    .HasForeignKey(f => f.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Reader)
                    .WithMany(r => r.Favorites)
                    .HasForeignKey(f => f.ReaderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация для BorrowedBook
            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.HasOne(bb => bb.Book)
                    .WithMany(b => b.BorrowedBooks)
                    .HasForeignKey(bb => bb.BookId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(bb => bb.Reader)
                    .WithMany(r => r.BorrowedBooks)
                    .HasForeignKey(bb => bb.ReaderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}