using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.TimeManagement;

namespace Tempus.Persistence.Configuration.TimeManagement;
internal class WorkPeriodConfiguration : IEntityTypeConfiguration<WorkPeriod>
{
    public void Configure(EntityTypeBuilder<WorkPeriod> builder)
    {
        builder.
            HasKey(b => b.Id);

        builder.HasIndex(b=>b.Date);
    }
}
