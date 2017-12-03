using Gighub.Core.Models;
using Gighub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gighub.Persistence.Repositories
{


    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                 .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
              .ToList();
        }
        public Attendance GetIfAnyAttendance(int id, string userId)
        {
            return _context.Attendances.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);
        }

        public bool AnyAttendance(int gigid, string userId)
        {
            return _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigid);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Cancel(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}