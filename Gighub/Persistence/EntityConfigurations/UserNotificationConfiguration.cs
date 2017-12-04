﻿using Gighub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Gighub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new { un.UserId, un.NotificationId });
            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}