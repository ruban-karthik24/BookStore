using BookStore.Data;

namespace BookStore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _context;
        public IBookRepository Books { get; }

        public UnitOfWork(BookContext context, IBookRepository bookRepository)
        {
            _context = context;
            Books = bookRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
