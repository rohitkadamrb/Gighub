using Gighub.Core.Models;
using Gighub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Gighub.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllFollowersAsPerId(string userId)
        {

            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

    }

}