using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCodeDb.DbConfig
{
    public class MasterConfig : IEntityTypeConfiguration<Master>
    {
        public void Configure(EntityTypeBuilder<Master> builder)
        {
            builder .HasOne(o => o.Taxonomy)
             .WithMany(o => o.Masters)
             .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                 new Master() { Id = 1, Key = "FPTV", Value = "Test 1" });
        }
    }
}
