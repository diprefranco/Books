using AutoMapper;

namespace Books.API.Profiles;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Entities.Book, Models.BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ConstructUsing(src => new Models.BookDto(src.Id, string.Empty, src.Title, src.Description));
        
        CreateMap<Models.BookForCreationDto, Entities.Book>()
            .ConstructUsing(src => new Entities.Book(Guid.NewGuid(), src.AuthorId, src.Title, src.Description));
    }
}
