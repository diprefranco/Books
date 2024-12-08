using Books.API.Entities;
using Books.API.Models.External;

namespace Books.API.Services;

public interface IBooksRepository
{
    void AddBook(Book book);
    Task<Book?> GetBookAsync(Guid id);
    Task<BookCoverDto?> GetBookCoverAsync(string id);
    Task<IEnumerable<BookCoverDto>> GetBookCoversProcessAfterWaitForAllAsync(Guid bookId);
    Task<IEnumerable<BookCoverDto>> GetBookCoversProcessOneByOneAsync(Guid bookId, CancellationToken cancellationToken);
    IEnumerable<Book> GetBooks();
    IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds);
    Task<bool> SaveChangesAsync();
}
