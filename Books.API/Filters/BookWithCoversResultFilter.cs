using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Books.API.Entities;
using Books.API.Models;

namespace Books.API.Filters;
public class BookWithCoversResultFilter : IAsyncResultFilter
{
    private readonly IMapper _mapper;

    public BookWithCoversResultFilter(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300)
        {
            await next();
            return;
        }

        var (book, bookCovers) = ((Book, IEnumerable<Models.External.BookCoverDto>))resultFromAction.Value;

        var bookWithCovers = _mapper.Map<BookWithCoversDto>(book);
        resultFromAction.Value = _mapper.Map(bookCovers, bookWithCovers);

        await next();
    }
}
