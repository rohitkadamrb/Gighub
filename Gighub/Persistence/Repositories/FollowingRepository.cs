using Gighub.Core.Models;
using Gighub.Core.Repositories;
using System.Linq;

namespace Gighub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
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

        public bool FollowingsAny(string dto, string userId)
        {
            return _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Cancel(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}