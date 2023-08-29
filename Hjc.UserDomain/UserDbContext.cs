using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.UserDomain
{
    public class UserDbContext : IdentityDbContext<MyUser, MyRole, long>
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}
