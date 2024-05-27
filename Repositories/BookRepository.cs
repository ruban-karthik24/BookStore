using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAllBooksSortedByPublisherAuthorTitleAsync()
        {
            //return await _context.Books.FromSqlRaw("SELECT * FROM Books ORDER BY Publisher, AuthorLastName, AuthorFirstName, Title").ToListAsync();
            return await _context.Books
            .OrderBy(b => b.Publisher)
            .ThenBy(b => b.AuthorLastName)
            .ThenBy(b => b.AuthorFirstName)
            .ThenBy(b => b.Title)
            .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksSortedByAuthorTitleAsync()
        {
            //return await _context.Books.FromSqlRaw("SELECT * FROM Books ORDER BY AuthorLastName, AuthorFirstName, Title").ToListAsync();
            return await _context.Books
            .OrderBy(b => b.AuthorLastName)
            .ThenBy(b => b.AuthorFirstName)
            .ThenBy(b => b.Title)
            .ToListAsync();
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            var total = await _context.Books.SumAsync(b => (double)b.Price);
            return (decimal)total;
        }

        public async Task AddBooksAsync(IEnumerable<Book> books)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Books.AddRangeAsync(books);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }

}
