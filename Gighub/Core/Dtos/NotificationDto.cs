using System;
using Gighub.Core.Models;

namespace Gighub.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }

        public string OrginalVenue { get; set; }

        public GigDto Gig { get; set; }
    }
}