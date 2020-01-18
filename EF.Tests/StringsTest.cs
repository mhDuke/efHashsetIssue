using EF.Entities;
using EF.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EF.Tests
{
    public class StringsTest
    {
        [Fact]
        public void Can_EfCore_Initialize_The_Strings_IEnumerable_Properly()
        {
            var provider = new InMemorySqliteProvider();
            var strings = new Strings(1);

            strings.AddString("1");
            strings.AddString("2");

            strings.AddString("1");
            strings.AddString("2");

            Assert.Equal(2, strings.Stringies.Count());

            try
            {
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    ctx.Add(strings);
                    ctx.SaveChanges();
                }
                using (var ctx = new HashsetContext(provider.DbOptions))
                {
                    //throws: System.InvalidCastException : 
                    //Unable to cast object of type 'System.Collections.Generic.List`1[System.String]' to type 'System.Collections.Generic.HashSet`1[System.String]'.
                    var efStrings = ctx.Strings.First();
                }
            }
            finally
            {
                provider.DestroyDb();
            }
        }
    }
}
