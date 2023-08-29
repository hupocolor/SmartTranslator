using Hjc.ChatGPT.Utils;
using Hjc.FileDomain;
using Hjc.TranslatorDomain;
using Hjc.TranslatorDomain.TranslatorServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.ChatGPT
{
    /// <summary>
    /// ChatGPT翻译器
    /// </summary>
    public class ChatGPTTranslate : ITanslatorService
    {
       

        public async Task<EnglishParagraph> TanslateParagraphAsync(string paragraph, string token)
        {
            string json = await JsonHelper.GetJSONAsync(paragraph, token, Prompt.ToEnglishParagraph);
            if (string.IsNullOrEmpty(json)) throw new Exception("返回空值");
            EnglishParagraph englishParagraph = new EnglishParagraph()
            {
                Content = json
            };
            if (englishParagraph == null) throw new Exception("格式解析错误");
            if (string.IsNullOrEmpty(englishParagraph.Content)) throw new Exception("文本解析错误");
            return englishParagraph;
        }

        


        public async Task<List<EnglishWord>> TanslateWordAsync(string word, string token)
        {
            string json = await JsonHelper.GetJSONAsync(word, token, Prompt.ToEnglishWord);
            if (string.IsNullOrEmpty(json)) throw new Exception("返回空值");
            Englishs? list = JsonConvert.DeserializeObject<Englishs>(json);
            if (list == null) throw new Exception("格式不正确");
            return list.EnglishWords;
        }


        public async Task<List<ChineseWord>> TanslateCWordAsync(string word, string token)
        {
            string json = await JsonHelper.GetJSONAsync(word, token, Prompt.ToChineseWord);
            if (string.IsNullOrEmpty(json)) throw new Exception("返回空值");
            Chineses? list = JsonConvert.DeserializeObject<Chineses>(json);
            if (list == null) throw new Exception("格式不正确");
            return list.ChineseWords;
        }

        public async Task<ChineseParagraph> TanslateCParagraphAsync(string paragraph, string token)
        {
            string json = await JsonHelper.GetJSONAsync(paragraph, token, Prompt.ToChineseParagraph);
            if (string.IsNullOrEmpty(json)) throw new Exception("返回空值");
            ChineseParagraph chineseParagraph = new ChineseParagraph()
            {
                Content = json
            };
            if (chineseParagraph == null) throw new Exception("格式解析错误");
            if (string.IsNullOrEmpty(chineseParagraph.Content)) throw new Exception("文本解析错误");
            return chineseParagraph;
        }
    }
}
