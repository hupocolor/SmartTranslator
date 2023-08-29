
namespace Hjc.TranslatorDomain.TranslatorServices
{
    /// <summary>
    /// 翻译领域服务
    /// </summary>
    public interface ITanslatorService
    {
        public Task<List<EnglishWord>> TanslateWordAsync(string word,string token);
        public Task<EnglishParagraph> TanslateParagraphAsync(string paragraph,string token);
        public Task<List<ChineseWord>> TanslateCWordAsync(string word, string token);
        public Task<ChineseParagraph> TanslateCParagraphAsync(string paragraph, string token);

    }
}