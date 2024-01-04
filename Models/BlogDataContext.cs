using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public IQueryable<MonthlySpecial> MonthlySpecials 
        {
            get
            {
                return new[]
                {
                    new MonthlySpecial
                    {
                        Key = "calm",
                        Name = "California Calm Package",
                        Type = "Day Spa Package",
                        Price = 250
                    },
                      new MonthlySpecial
                    {
                        Key = "desert",
                        Name = "Frome Desert to sea",
                        Type = "2 Day Salton Sea",
                        Price = 350
                    },
                        new MonthlySpecial
                    {
                        Key = "backpack",
                        Name = "backpack cali",
                        Type = "Big Sur Retreat",
                        Price = 620
                    },
                 }.AsQueryable();
            }
        }

        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
