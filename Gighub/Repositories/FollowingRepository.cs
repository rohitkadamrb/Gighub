using Gighub.Models;
using System.Linq;

namespace Gighub.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetAnyFollowings(string ArtistId, string userId)
        {
            return _context.Followings.SingleOrDefault(f => f.FolloweeId == ArtistId && f.FollowerId == userId);
        }
    }
}