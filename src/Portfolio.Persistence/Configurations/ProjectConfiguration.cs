using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace Portfolio.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(e => e.Tools).HasConversion(
                v => JsonSerializer.Serialize(v, default),
                v => JsonSerializer.Deserialize<List<string>>(v, default));
        }
    }
}
