using Books.API.DbContexts;
using Books.API.Entities;
using Books.API.Models.External;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Books.API.Services;

public class BooksRepository : IBooksRepository
{
    private readonly BooksContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public BooksRepository(BooksContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public void AddBook(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        _context.Books.Add(book);
    }

    public Task<Book?> GetBookAsync(Guid id)
    {
        return _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BookCoverDto?> GetBookCoverAsync(string id)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetAsync($"http://localhost:5096/api/bookcovers/{id}");
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<BookCoverDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        return null;
    }

    public async Task<IEnumerable<BookCoverDto>> GetBookCoversProcessOneByOneAsync(Guid bookId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var bookCovers = new List<BookCoverDto>();
        var bookCoverUrls = new[]
        {
            $"http://localhost:5096/api/bookcovers/{bookId}-cover1",
            $"http://localhost:5096/api/bookcovers/{bookId}-cover2",
            $"http://localhost:5096/api/bookcovers/{bookId}-cover3",
            $"http://localhost:5096/api/bookcovers/{bookId}-cover4",
            $"http://localhost:5096/api/bookcovers/{bookId}-cover5"
        };

        foreach (var bookCoverUrl in bookCoverUrls)
        {
            var response = await httpClient.GetAsync(bookCoverUrl);
            if (response.IsSuccessStatusCode)
            {
                var bookCover = JsonSerializer.Deserialize<BookCoverDto>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                if (bookCover != null)
                {
                    bookCovers.Add(bookCover);
                }
            }
        }

        return bookCovers;
    }

    public IEnumerable<Book> GetBooks()
    {
        return _context.Books.Include(b => b.Author).ToList();
    }

    public IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable()
    {
        return _context.Books.Include(b => b.Author).AsAsyncEnumerable();
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds)
    {
        return await _context.Books.Where(b => bookIds.Contains(b.Id)).Include(b => b.Author).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
