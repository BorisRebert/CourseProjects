using DataBaseProject.DBModels;
using Microsoft.EntityFrameworkCore;

namespace DataBaseProject
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Browser> Browsers => Set<Browser>();
        public DbSet<User> Users => Set<User>();
        public DbSet<TestData> Tests => Set<TestData>();
        
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Config;Trusted_Connection=True;");
        }
    }
}