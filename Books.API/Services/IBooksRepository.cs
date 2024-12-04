using Books.API.Entities;

namespace Books.API.Services;

public interface IBooksRepository
{
    Task<Book?> GetBookAsync(Guid id);
    Task<IEnumerable<Book>> GetBooksAsync();
}
