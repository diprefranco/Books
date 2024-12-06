using Books.API.Entities;

namespace Books.API.Services;

public interface IBooksRepository
{
    void AddBook(Book book);
    Task<Book?> GetBookAsync(Guid id);
    IEnumerable<Book> GetBooks();
    IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds);
    Task<bool> SaveChangesAsync();
}
