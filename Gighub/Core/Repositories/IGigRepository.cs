using Gighub.Core.Models;
using System.Collections.Generic;

namespace Gighub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetAllUpcomingGigs();
        void Add(Gig gig);
        IEnumerable<Gig> GetGigOfCurrentUser(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithArtistAndGenre(int Id);
        Gig GetGigWithAttendees(int gigId);

        Gig GetGigWithUserId(int id, string userId);

        void Cancel(Gig gig);
    }
}