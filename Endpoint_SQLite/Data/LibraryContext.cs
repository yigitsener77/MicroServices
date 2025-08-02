using Microsoft.EntityFrameworkCore;

namespace Endpoint_SQLite.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
