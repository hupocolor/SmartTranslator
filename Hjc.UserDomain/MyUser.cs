using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hjc.UserDomain
{
    public class MyUser:IdentityUser<long>
    {
        [MaxLength(200)]
        public string? ChatToken { get; set; }
        public long JWTVersion { get; set; }
    }
}