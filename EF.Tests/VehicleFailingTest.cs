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
    public class VehicleFailingTest
    {
        [Fact]
        public void Ensure_Duplicate_Free_Features_When_Features_Collection_Instantiation_leftTo_EfCore()
        {
            var provider = new InMemorySqliteProvider();

            var vehicle = new Vehicle("vehicle1");

            vehicle.AddFeature(new Feature(1));
            vehicle.AddFeature(new Feature(2));

            vehicle.AddFeature(new Feature(1));
            vehicle.AddFeature(new Feature(2));

            Assert.Equal(2, vehicle.Features.Count());

            try
            {
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    ctx.Add(vehicle);
                    ctx.SaveChanges();
                }
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    var efVehicle = ctx.Vehicles.Include(e => e.Features).First();

                    Assert.True(efVehicle.Features.Count() == 2);

                    efVehicle.AddFeature(new Feature(1));
                    efVehicle.AddFeature(new Feature(2));

                    //fails: 
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
