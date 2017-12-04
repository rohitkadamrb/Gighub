﻿using Gighub.Core.Models;
using Gighub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gighub.Persistence.Repositories
{


    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetAllUpcomingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances.
                Where(a => a.AttendeeId == userId).
                Select(a => a.Gig).
                Include(g => g.Artist).
                Include(g => g.Genre).
                ToList();
        }
        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                    .Include(g => g.Attendances.Select(a => a.Attendee))
                  .SingleOrDefault(g => g.Id == gigId);
        }
        public Gig GetGigWithUserId(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id);
        }

        public IEnumerable<Gig> GetGigOfCurrentUser(string userId)
        {
            return _context.Gigs.
                  Where(g => g.ArtistId == userId &&
                  g.DateTime > DateTime.Now && !g.IsCancelled)
                  .Include(g => g.Genre).
                  ToList();
        }

        public Gig GetGigWithArtistAndGenre(int Id)
        {
            return _context.Gigs
                 .Include(g => g.Artist)
                 .Include(g => g.Genre)
                 .SingleOrDefault(g => g.Id == Id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public void Cancel(Gig gig)
        {
            _context.Gigs.Remove(gig);
        }
    }
}