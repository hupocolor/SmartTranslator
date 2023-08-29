using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hjc.TranslatorDomain
{
    public class ChineseWord
    {
        [Key]
        public long ChineseId { get; set; }
        [MaxLength(10)]
        public string? Word { get; set; }
        [MaxLength(10)]
        public string? WordType { get; set; }
        [MaxLength(100)]
        public string? Explain { get; set; }
        public List<EnglishWord>? EnglishWords { get; set; }

    }
}