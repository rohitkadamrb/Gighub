using Gighub.Core.Models;
using System.Collections.Generic;

namespace Gighub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetIfAnyAttendance(int id, string userId);

        bool AnyAttendance(int gigid, string userId);
        void Add(Attendance attendance);
        void Cancel(Attendance attendance);
    }
}