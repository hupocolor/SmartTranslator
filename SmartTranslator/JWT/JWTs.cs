using Hjc.UserDomain;
using Microsoft.IdentityModel.Tokens;
using SmartTranslator.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartTranslator.JWT
{
    public class JWTs
    {
        internal static string BuildToken(MyUser user, IList<string> roles,JWTOptions options)
        {
            DateTime expires = DateTime.Now.AddSeconds(options.ExpireSeconds);
            byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
            var secKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(secKey,
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Version, user.JWTVersion.ToString()));
            if(user.ChatToken != null)
            claims.Add(new Claim("UserChatToken", user.ChatToken));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new JwtSecurityToken(expires: expires,
                signingCredentials: credentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        internal static long GetUserId(ClaimsPrincipal User)
        {
            try
            {
                return long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            }
            catch
            {
                throw MyException.create("登录验证失败");
            }
        }
        internal static string GetUserName(ClaimsPrincipal User)
        {
            try
            {
                return User.FindFirst(ClaimTypes.Name)!.Value;
            }
            catch
            {
                throw MyException.create("解析用户名错误");
            }
        }

        internal static List<string> GetUserRoles(ClaimsPrincipal User)
        {
            IEnumerable<Claim> roleClaims;
            try
            {
                roleClaims = User.FindAll(ClaimTypes.Role);
            }
            catch
            {
                throw MyException.create("权限验证失败");
            }
            List<string> roles;
            roles = roleClaims.Select(x => x.Value).ToList();
            return roles;
        }
        internal static string? GetChatToken(ClaimsPrincipal User)
        {
            try
            {
                return User.FindFirst("UserChatToken")!.Value;
            }
            catch
            {
                throw MyException.create("没有ChatGPT的Token");
            }
        }
    }
}
