using Books.API.Entities;

namespace Books.API.Services;

public interface IBooksRepository
{
    void AddBook(Book book);
    Task<Book?> GetBookAsync(Guid id);
    IEnumerable<Book> GetBooks();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<bool> SaveChangesAsync();
}
