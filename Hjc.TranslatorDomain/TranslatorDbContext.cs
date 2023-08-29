using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.TranslatorDomain
{
    public class TranslatorDbContext : DbContext
    {
        public DbSet<ChineseWord> ChineseWords { get; set; }
        public DbSet<EnglishWord> EnglishWords { get; set; }
        public TranslatorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectStr = "Server=.\\SQLEXPRESS;DataBase=SmartTranslator;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectStr);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
