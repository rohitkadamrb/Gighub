using Gighub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Gighub.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {

            HasRequired(n => n.Gig);
        }
    }
}