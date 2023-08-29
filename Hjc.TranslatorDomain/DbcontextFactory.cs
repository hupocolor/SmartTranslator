using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.TranslatorDomain
{
    internal class DbcontextFactory : IDesignTimeDbContextFactory<TranslatorDbContext>
    {
        public TranslatorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TranslatorDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;DataBase=SmartTranslator;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");

            return new TranslatorDbContext(optionsBuilder.Options);
        }
    }
}
