using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.UserDomain
{
    public class UserService : IUserService
    {
        UserManager<MyUser> userManager;

        public UserService(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            var user = await userManager.FindByNameAsync(username);
            if (user == null) return false;
            if (await userManager.CheckPasswordAsync(user, password)) return true;
            return false;
        }

        public async Task<bool> Resister(MyUser user)
        {
            if (await userManager.FindByNameAsync(user.UserName) != null) return false;
            if (await userManager.FindByEmailAsync(user.Email) != null) return false;
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, "User");
            return true;
        }


        
    }
}
