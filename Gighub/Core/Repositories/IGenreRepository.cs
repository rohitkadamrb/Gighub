using Gighub.Core.Models;
using System.Collections.Generic;

namespace Gighub.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGenres();
    }
}