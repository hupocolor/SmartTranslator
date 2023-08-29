using Hjc.UserDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTranslator.Dto;
using SmartTranslator.JWT;
using SmartTranslator.Utils;

namespace SmartTranslator.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserDetailController:ControllerBase
    {
        UserManager<MyUser> _userManager;

        public UserDetailController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// 获取当前登录用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseResult<MyUserDetailsDto>>> GetLoginDetails()
        {
            long id = JWTs.GetUserId(User);
            var nowUser = await _userManager.FindByIdAsync(id.ToString());
            var res = ResponseResults<MyUserDetailsDto>.OkRes(
                    new MyUserDetailsDto()
                    {
                        PhoneNumber = nowUser.PhoneNumber,
                        Email= nowUser.Email,
                        UserName= nowUser.UserName,
                        ChatToken=nowUser.ChatToken
                    }
                );
            return res;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseResult<string>>> SetChatToken([FromBody]JWTOptions options ,string token)
        {
            if (string.IsNullOrEmpty(token)) throw MyException.create("token异常");
            long id = JWTs.GetUserId(User);
            var nowUser = await _userManager.FindByIdAsync(id.ToString());
            nowUser.ChatToken = token;
            nowUser.JWTVersion++;
            await _userManager.UpdateAsync(nowUser);
            var jwt = JWTs.BuildToken(nowUser,await _userManager.GetRolesAsync(nowUser),options);
            return ResponseResults<string>.OkRes(jwt);
        }
    }
}
