using Gighub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Gighub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }

    }
}