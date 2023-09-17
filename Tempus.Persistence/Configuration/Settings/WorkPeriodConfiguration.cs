using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.Settings;

namespace Tempus.Persistence.Configuration.TimeManagement;
internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.
            HasKey(b => b.Id);

        builder.HasIndex(b=>b.Key);
    }
}
