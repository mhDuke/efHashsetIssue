using EF.Entities;
using EF.Tests.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EF.Tests
{
    public class VehicleHashSetInitializedSucceedingTest
    {
        [Fact]
        public void Ensure_Duplicate_Free_Features_After_Manual_HashSet_Instantiation()
        {
            var provider = new InMemorySqliteProvider();

            var vehicleInitialized = new VehicleInitialized("vehicle1");

            vehicleInitialized.AddFeature(new Feature(1));
            vehicleInitialized.AddFeature(new Feature(2));

            vehicleInitialized.AddFeature(new Feature(1));
            vehicleInitialized.AddFeature(new Feature(2));

            Assert.Equal(2, vehicleInitialized.Features.Count());

            try
            {
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    ctx.Add(vehicleInitialized);
                    ctx.SaveChanges();
                }
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    var efVehicle = ctx.VehiclesWithHashSetInitialized.Include(e => e.Features).First();

                    Assert.True(efVehicle.Features.Count() == 2);

                    efVehicle.AddFeature(new Feature(1));
                    efVehicle.AddFeature(new Feature(2));

                    //succeeds: 
                    Assert.Equal(2, efVehicle.Features.Count());
                }
            }
            finally
            {
                provider.DestroyDb();
            }

        }
    }
}
