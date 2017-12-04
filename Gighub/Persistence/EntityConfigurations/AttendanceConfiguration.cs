using Gighub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Gighub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {

            HasKey(a => new { a.GigId, a.AttendeeId });
        }
    }
}