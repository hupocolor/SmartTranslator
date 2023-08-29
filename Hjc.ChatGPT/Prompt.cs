namespace Hjc.ChatGPT
{
    public class Prompt
    {
        public static readonly string ToEnglishWord = "请你扮演一个翻译器，我将给你一个中文单词，请你进行翻译成英文。并使用JSON按照:{EnglishWords:[{Word:翻译出的英文单词,WordType:单词类型(中文),Explain:解释(中文)}...这里可能存在多个翻译结果]}的格式回答，其中EnglishWords是一个单词数组，若存在多个翻译结果则有多个单词，WordType是词汇类型例如名词，动词，Explain是对翻译出的单词的详细解释。现在翻译:";
        public static readonly string ToEnglishParagraph = "请你将以下中文翻译成英文:";
        public static readonly string ToChineseWord = "请你扮演一个翻译器，我将给你一个英文单词，请你进行翻译成中文。并使用JSON按照:{ChineseWords:[{Word:翻译出的中文单词,WordType:单词类型(英文),Explain:解释(英文)}...这里可能存在多个翻译结果]}的格式回答，其中ChineseWords是一个单词数组，若存在多个翻译结果则有多个单词，WordType是词汇类型例如名词，动词，Explain是对翻译出的单词的详细解释。现在翻译:";
        public static readonly string ToChineseParagraph = "请你将以下英文翻译成中文:";
    }
}