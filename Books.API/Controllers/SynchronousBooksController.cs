using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api/books/sync")]
[ApiController]
public class SynchronousBooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public SynchronousBooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _booksRepository.GetBooks();
        return Ok(books);
    }
}
