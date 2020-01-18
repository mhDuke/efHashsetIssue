using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.EntityConfig
{
    public class VehicleInitializedConfiguration : IEntityTypeConfiguration<VehicleInitialized>
    {
        public void Configure(EntityTypeBuilder<VehicleInitialized> b)
        {
            b.OwnsMany<Feature>(e => e.Features, a =>
            {
                a.WithOwner();
            });
        }
    }
}
