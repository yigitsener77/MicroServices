using Endpoint_SQLite.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Endpoint_SQLite
{

    public class LibraryService : ILibraryService
    {
        private readonly LibraryContext db;

        public LibraryService(LibraryContext db)
        {
            this.db = db;
        }

        private async Task saveChangesAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await db.DisposeAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            await db.Authors.AddAsync(author);
            await saveChangesAsync();
            return author;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await db.Books.AddAsync(book);
            await saveChangesAsync();
            return book;
        }

        public async Task<Publisher> AddPublisherAsync(Publisher publisher)
        {
            await db.Publishers.AddAsync(publisher);
            await saveChangesAsync();
            return publisher;
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var author = await db.Authors.FindAsync(authorId);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {authorId} not found.");
            }

            // Check if the author has any books associated with them
            try
            {
                db.Remove(author);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking author's books: {ex.Message}");
            }

            await saveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await db.Books.FindAsync(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }
            db.Remove(book);
        }

        public async Task DeletePublisherAsync(int publisherId)
        {
            var publisher = await db.Publishers.FindAsync(publisherId);
            if (publisher == null)
            {
                throw new KeyNotFoundException($"Publisher with ID {publisherId} not found.");
            }
            // Check if the publisher has any books associated with them
            try
            {
                db.Remove(publisher);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking publisher's books: {ex.Message}");
            }
            db.Remove(publisher);
        }

        public async Task<Author?> GetAuthorAsync(int authorId)
        {
            return await db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await db.Authors.Include("Books").ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int bookId)
        {
            return await db.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int? authorId = null, int? publisherId = null)
        {
            return await db.Books.Include(b => b.Author).Include(b => b.Publisher)
                .Where(b => (authorId == null || b.AuthorId == authorId) && (publisherId == null || b.PublisherId == publisherId)).ToListAsync();
        }

        public async Task<Publisher?> GetPublisherAsync(int publisherId)
        {
            return await db.Publishers.Include(p => p.Books).FirstOrDefaultAsync(p => p.Id == publisherId);
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await db.Publishers.Include("Books").ToListAsync();
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            return await Task.Run(() =>
            {
                db.Authors.Update(author);
                return author;
            });
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            return await Task.Run(() =>
            {
                db.Books.Update(book);
                return book;
            });
        }

        public async Task<Publisher> UpdatePublisherAsync(Publisher publisher)
        {
            return await Task.Run(() =>
            {
                db.Publishers.Update(publisher);
                return publisher;
            });
        }
    }
}
