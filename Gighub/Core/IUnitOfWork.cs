using Gighub.Core.Repositories;

namespace Gighub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IApplicationUserRepository Users { get; }
        INotificationRepository Notifications { get; }

        void Complete();
    }
}