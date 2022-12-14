using AutoMapper;
using BookStoreWebApi.DbOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel? Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model!.Name);

            if (genre is not null)
            {
                throw new InvalidOperationException("Bu kategori sisteme kayıtlı");
            }

            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string? Name { get; set; }
    }
}