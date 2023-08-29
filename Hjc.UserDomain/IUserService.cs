using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.UserDomain
{
    public interface IUserService
    {
        public Task<bool> Login(string username, string password);
        public Task<bool> Resister(MyUser user);
    }
}
