using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.TranslatorDomain
{
    internal class EnglishWordConfig : IEntityTypeConfiguration<EnglishWord>
    {
        public void Configure(EntityTypeBuilder<EnglishWord> builder)
        {
            builder.ToTable("EnglishWords");
        }
    }
}
