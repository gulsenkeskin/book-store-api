using BookStoreWebApi.DbOperations;

namespace BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreModel? Model { get; set; }
        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            if (_context.Genres.Any(x => x.Name != null && x.Name.ToLower() == Model!.Name!.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut");

            genre.Name = string.IsNullOrEmpty(Model!.Name!.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;

            _context.SaveChanges();
        }

    }

    public class UpdateGenreModel
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}