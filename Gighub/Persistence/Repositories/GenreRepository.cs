using Gighub.Core.Models;
using Gighub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Gighub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
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