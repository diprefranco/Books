﻿using AutoMapper;
using Books.API.Entities;
using Books.API.Filters;
using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api/books")]
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

    [HttpGet]
    [TypeFilter(typeof(BooksResultFilter))]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _booksRepository.GetBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}", Name = "GetBook")]
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
    
    [HttpPost]
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
