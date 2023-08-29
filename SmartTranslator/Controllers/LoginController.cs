using Hjc.UserDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartTranslator.JWT;
using SmartTranslator.Utils;
using System.Security.Claims;

namespace SmartTranslator.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController:ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<MyUser> userManager;

        public LoginController(IUserService userService, UserManager<MyUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult<string>>> LoginUser(string username,string password ,[FromServices] IOptions<JWTOptions> jwtOption)
        {
            if (! await userService.Login(username, password)){ return BadRequest(ResponseResult<string>.Error("登录失败")); }
            var user = await userManager.FindByNameAsync(username);
            user.JWTVersion++;
            await userManager.UpdateAsync(user);
            return Ok(ResponseResult<string>.Ok(JWTs.BuildToken(user,await userManager.GetRolesAsync(user),jwtOption.Value)));

        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult<string>>> Register([FromBody] MyUser user)
        {
            if(user == null) { throw MyException.create("出现了空用户"); }
            var res = await userService.Resister(user);
            if (res) return Ok(ResponseResult<string>.Ok());
            return BadRequest(ResponseResult<string>.Error("注册失败"));
        }
        
    }
}
