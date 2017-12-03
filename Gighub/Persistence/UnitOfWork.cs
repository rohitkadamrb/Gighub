﻿using Gighub.Core;
using Gighub.Core.Repositories;
using Gighub.Persistence.Repositories;

namespace Gighub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IApplicationUserRepository Users { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Followings = new FollowingRepository(context);
            Attendances = new AttendanceRepository(context);
            Users = new ApplicationUserRepository(context);
            Notifications = new NotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}