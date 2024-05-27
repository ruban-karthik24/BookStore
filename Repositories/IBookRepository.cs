using BookStore.Models;

namespace BookStore.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetAllBooksSortedByPublisherAuthorTitleAsync();
        Task<IEnumerable<Book>> GetAllBooksSortedByAuthorTitleAsync();
        Task<decimal> GetTotalPriceAsync();
        Task AddBooksAsync(IEnumerable<Book> books);
    }
}
