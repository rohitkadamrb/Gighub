﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Gighub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }

        public string OrginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {

        }
        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
            {
                throw new ArgumentNullException("gig");
            }
            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;

        }
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OrginalVenue = originalVenue;
            return notification;
        }
        public static Notification GigCancelled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCancelled);
        }
    }
}