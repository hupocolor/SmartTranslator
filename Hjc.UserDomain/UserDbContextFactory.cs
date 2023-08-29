using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.UserDomain
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;DataBase=UserDb;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
