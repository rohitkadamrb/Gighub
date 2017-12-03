﻿using Gighub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Gighub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>().
                HasRequired(a => a.Gig).
                WithMany(g => g.Attendances).
                WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().
               HasMany(u => u.Followers).
               WithRequired(f => f.Followee).
               WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>().
               HasMany(u => u.Followees).
               WithRequired(f => f.Follower).
               WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>().
                HasRequired(n => n.User).
                WithMany(u => u.UserNotifications).
                WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}