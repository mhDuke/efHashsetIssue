using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.EntityConfig
{
    public class StringsConfiguration : IEntityTypeConfiguration<Strings>
    {
        public void Configure(EntityTypeBuilder<Strings> b)
        {
            b.Property(e => e.Stringies).IsRequired()
                .HasMaxLength(200)
                .HasConversion(
                    strings => JsonConvert.SerializeObject(strings, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    json => JsonConvert.DeserializeObject<IEnumerable<string>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
