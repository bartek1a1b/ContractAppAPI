using ContractAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace ContractAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<ContractAppAPI.Models.Contract> Contracts { get; set; }
        public DbSet<ContractTypeOne> ContractTypeOnes { get; set; }
        public DbSet<ContractTypeTwo> ContractTypeTwos { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
