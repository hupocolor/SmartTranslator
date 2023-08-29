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
    [Authorize(Roles ="Admin")]
    public class UserAndRoleController:ControllerBase
    {
        private UserManager<MyUser> _userManager;

        public UserAndRoleController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 根据关键词搜索用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="eMail"></param>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult<ResponseResult<List<MyUserDetailsDto>>> GetByDetails(string? name,string? eMail,string? phoneNum)
        {
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(name))
                users = users.Where(user => user.UserName.Contains(name));
            if(!string.IsNullOrEmpty(eMail))
                users = users.Where(user=>user.Email.Contains(eMail));
            if(!string.IsNullOrEmpty(phoneNum))
                users = users.Where(user =>user.PhoneNumber.Contains(phoneNum));
            var res = users.Select(user=>new MyUserDetailsDto() { 
                ChatToken=user.ChatToken,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber,
                UserName=user.UserName,
            }).ToList();
            return ResponseResults<List<MyUserDetailsDto>>.OkRes(res);
        }
    }
}
