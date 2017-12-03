using System.Collections.Generic;
using Gighub.Core.Models;

namespace Gighub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetAllNotifications(string userId);
        IEnumerable<UserNotification> GetUserNotifications(string userId);
    }
}