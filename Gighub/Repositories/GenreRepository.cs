using Gighub.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gighub.Repositories
{
    public class GenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Genre> GetAllGenres()
        {

            return _context.Genres.ToList();
        }
    }
}