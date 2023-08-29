using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.TranslatorDomain
{
    internal class ChineseWordConfig : IEntityTypeConfiguration<ChineseWord>
    {
        public void Configure(EntityTypeBuilder<ChineseWord> builder)
        {
            builder.ToTable("ChineseWords");
        }
    }
}
