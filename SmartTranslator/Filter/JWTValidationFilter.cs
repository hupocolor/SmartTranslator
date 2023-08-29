using Hjc.UserDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using WebDemo.Utils;

namespace SmartTranslator.Filter
{
    public class JWTValidationFilter : IAsyncActionFilter
    //public class JWTValidationFilter
    {
        private readonly RedisUtil redisUtil;
        private readonly UserManager<MyUser> userManager;

        public JWTValidationFilter(RedisUtil redisUtil, UserManager<MyUser> userManager)
        {
            this.redisUtil = redisUtil;
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            //没有token就是未登录，跳过
            if (claimUserId == null)
            {
                //调用next才会往下执行，不调用就不会执行了
                await next();
                return;
            }
            long userId = long.Parse(claimUserId.Value);
            string cacheKey = $"JWT.UserInfo.{userId}";
            MyUser? user = await redisUtil.GetOrCreateAsync(cacheKey, async () =>
            {
                return await userManager.FindByIdAsync(userId.ToString());
            },3000);
            if(user == null)
            {
                var result = new ObjectResult($"UserId(userId) not found");
                result.StatusCode = 404;
                context.Result = result;
                return;
            }
            var claimVersion = context.HttpContext.User.FindFirst(ClaimTypes.Version);
            //jwt中保存的版本号
            long jwtVerOfReq = long.Parse(claimVersion!.Value);
            //由于内存缓存等导致的并发问题，
            //假如集群的A服务器中缓存保存的还是版本为5的数据，但客户端提交过来的可能已经是版本号为6的数据。因此只要是客户端提交的版本号>=服务器上取出来（可能是从Db，也可能是从缓存）的版本号，那么也是可以的
            if (jwtVerOfReq >= user.JWTVersion)
            {
                await next();
            }
            else
            {
                var result = new ObjectResult($"JWTVersion mismatch");
                result.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = result;
                return;
            }
        }
    }
}
