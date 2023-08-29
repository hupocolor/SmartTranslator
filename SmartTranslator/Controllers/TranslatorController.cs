using Hjc.TranslatorDomain;
using Hjc.TranslatorDomain.TranslatorServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTranslator.JWT;
using SmartTranslator.Utils;

namespace SmartTranslator.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class TranslatorController : ControllerBase
    {
        private readonly ITanslatorService _translatorService;

        public TranslatorController(ITanslatorService translatorService)
        {
            _translatorService = translatorService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseResult<List<ChineseWord>>>> TansToChineseWord(string englishWord) {
            string chatToken = JWTs.GetChatToken(User)!;
            
            return Ok(ResponseResult<List<ChineseWord>>.Ok(
                   await _translatorService.TanslateCWordAsync(englishWord,chatToken)
                ));;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseResult<List<EnglishWord>>>> TansToEnslishWord(string chineseWord)
        {
            string chatToken = JWTs.GetChatToken(User)!;
            
            return Ok(ResponseResult<List<EnglishWord>>.Ok(
                   await _translatorService.TanslateWordAsync(chineseWord, chatToken)
                ));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseResult<ChineseParagraph>>> TansToChinese(string paragraph)
        {
            string chatToken = JWTs.GetChatToken(User)!;
            
            return Ok(ResponseResult<ChineseParagraph>.Ok(
                   await _translatorService.TanslateCParagraphAsync(paragraph, chatToken)
                ));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseResult<EnglishParagraph>>> TansToEnglish(string paragraph)
        {
            string chatToken = JWTs.GetChatToken(User)!;
            
            return Ok(ResponseResult<EnglishParagraph>.Ok(
                   await _translatorService.TanslateParagraphAsync(paragraph, chatToken)
                ));
        }

        
    }
}