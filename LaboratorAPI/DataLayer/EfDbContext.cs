using LaboratorAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LaboratorAPI.DataLayer
{
    public class EfDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Development.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Laborator");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
