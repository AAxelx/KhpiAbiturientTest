using Microsoft.EntityFrameworkCore;
using QuestionManager.DAL.DataAccess.Implementations.EntityConfigurations;
using System.Threading.Tasks;

namespace QuestionManager.DAL.DataAccess.Implementations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8NE0IIK\\SQLEXPRESS; Database=NtuAbiturientTest; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
}
