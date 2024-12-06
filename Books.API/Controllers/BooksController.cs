using AutoMapper;
using Books.API.Entities;
using Books.API.Filters;
using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;

    public BooksController(IBooksRepository booksRepository, IMapper mapper)
    {
        _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("books")]
    [TypeFilter(typeof(BooksResultFilter))]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _booksRepository.GetBooksAsync();
        return Ok(books);
    }

    [HttpGet("bookstream")]
    public async IAsyncEnumerable<BookDto> StreamBooks()
    {
        HttpContext.Response.ContentType = "text/event-stream; charset=utf-8";

        await foreach (var book in _booksRepository.GetBooksAsAsyncEnumerable())
        {
            await Task.Delay(500); //for demo purposes.
            yield return _mapper.Map<BookDto>(book);
        }
    }

    [HttpGet("books/{id}", Name = "GetBook")]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var book = await _booksRepository.GetBookAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    
    [HttpPost("books")]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<IActionResult> CreateBook(BookForCreationDto book)
    {
        var bookEntity = _mapper.Map<Book>(book);

        _booksRepository.AddBook(bookEntity);
        await _booksRepository.SaveChangesAsync();
        await _booksRepository.GetBookAsync(bookEntity.Id);

        return CreatedAtRoute("GetBook", new { id = bookEntity.Id }, bookEntity);
    }
}
