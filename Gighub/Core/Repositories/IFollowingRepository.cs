using Gighub.Core.Models;

namespace Gighub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetAnyFollowings(string ArtistId, string userId);
        bool FollowingsAny(string dto, string userId);
        void Add(Following following);
        void Cancel(Following following);
    }
}