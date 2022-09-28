using AutoMapper;
using BookStoreWebApi.DbOperations;

namespace BookStoreWebApi.Application.GenreOperations.Queries
{
    public class GetGenresQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}