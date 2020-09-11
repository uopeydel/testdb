using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCodeDb.DbConfig
{
    public class TaxonomyConfig : IEntityTypeConfiguration<Taxonomy>
    {
        public void Configure(EntityTypeBuilder<Taxonomy> builder)
        {
            builder
                .HasMany(o => o.Masters)
                .WithOne(o => o.Taxonomy)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                 new Taxonomy() { Id = 1, Key = "FPTV", Value = "Test 1" });
        }
    }
}
