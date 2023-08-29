using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.TranslatorDomain
{
    public class EnglishWord
    {
        [Key]
        public long EnglishId { get; set; }
        [MaxLength(10)]
        public string? Word { get; set; }
        [MaxLength(10)]
        public string? WordType { get; set; }
        [MaxLength(100)]
        public string? Explain { get; set; }
        public List<ChineseWord>? ChineseWords { get; set; }
    }
}
